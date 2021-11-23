#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <mcheck.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <sys/wait.h>
#include <netinet/in.h>
#include <unistd.h>
void server(int pid)
{
	printf("child %d\nparent %d",pid,getpid());
        //kill(pid,SIGSTOP);
        char server_message[256] = "You have reached the server!";
        printf("Server executing\n");
 	    // create the server socket
 	    int server_socket;
 	    server_socket = socket(AF_INET, SOCK_STREAM, 0);
 	    // define the server address
 	    struct sockaddr_in server_address;
 	    server_address.sin_family = AF_INET;
 	    server_address.sin_port = htons(8005); 
 	    server_address.sin_addr.s_addr = inet_addr("127.0.0.1");
 	    // bind the socket to our specified IP and port
	    //printf("started listening\n");
		bind(server_socket, (struct sockaddr*) &server_address, sizeof(server_address));
 	    // second agrument is a backlog - how many connections can be waiting for this socket simultaneously
 	    
		kill(pid,SIGCONT);
 	    listen(server_socket, 5);

	    accept(server_socket, NULL, NULL);
	    // send the message
	    send(server_socket, server_message, sizeof(server_message), 0);
    	// close the socket
	    printf("data sent");
	    close(server_socket);
		exit(0);
}
void client()
{
        kill(getpid(),SIGSTOP);
    	//int parrentPID = getppid();
        int network_socket;
        int status;
	    network_socket = socket(AF_INET, SOCK_STREAM, 0);
	    // specify an address for the socket
	    struct sockaddr_in server_address;
	    server_address.sin_family = AF_INET;
	    server_address.sin_port = htons(8005);
	    server_address.sin_addr.s_addr = inet_addr("127.0.0.1");
		/*waitpid(getpid(), &status,WUNTRACED);
		while (!WIFSTOPPED(status))
		{
			waitpid(getpid(), &status,WUNTRACED);
			printf("status %d\n",status);
			// code 
		}*/
		

		status = connect(network_socket, (struct sockaddr *) &server_address, sizeof(server_address));

	    if(-1==status)
	    {
		    // check for error with the connection
		    printf("Server not avaivable\n\n");
	    }

	    // receive data from the server
	    char server_response[256];
	    recv(network_socket, &server_response, sizeof(server_response), 0);
	    // print out the server's response
	    printf("The server sent the data: %s\n", server_response);
	    // and then close the socket
	    close(network_socket);
		exit(0);
}
int main()
{
    pid_t pid;
    int status;
    pid = fork();
    switch(pid)
    {
    case -1:
        printf("fork error");
        exit(1);
        break;
    case 0:
        client();
        break;
    default:
        server(pid);
        break;
    }
    return 0;
}
