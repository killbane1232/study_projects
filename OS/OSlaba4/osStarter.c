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
    printf("Server executing\n");
 	int server_socket;
 	server_socket = socket(AF_INET, SOCK_STREAM, 0);
 	struct sockaddr_in server_address;
 	server_address.sin_family = AF_INET;
 	server_address.sin_port = htons(8005); 
 	server_address.sin_addr.s_addr = inet_addr("127.0.0.1");
	bind(server_socket, (struct sockaddr*) &server_address, sizeof(server_address));
	int status;
 	listen(server_socket, 5);
	kill(pid,SIGCONT);
	printf("continued\n");
	int inner = accept(server_socket, NULL, NULL);
	printf("Accepted\n");
    char server_message[256] = "You have reached the server!";
	send(inner, server_message, sizeof(server_message), 0);
	printf("data sent\n");
	char server_response[256];
	recv(inner, &server_response, sizeof(server_response), 0);
	printf("The clnt sent the data: %s\n", server_response);
	close(server_socket);
}
void client()
{
    int network_socket;
    int status;
	network_socket = socket(AF_INET, SOCK_STREAM, 0);
	struct sockaddr_in server_address;
	server_address.sin_family = AF_INET;
	server_address.sin_port = htons(8005);
	server_address.sin_addr.s_addr = inet_addr("127.0.0.1");
	status = connect(network_socket, (struct sockaddr *) &server_address, sizeof(server_address));
	while(-1==status)
	{
		sleep(1);
	    printf("Server not avaivable\n\n");
		status = connect(network_socket, (struct sockaddr *) &server_address, sizeof(server_address));
	}
	char server_response[256];
	recv(network_socket, &server_response, sizeof(server_response), 0);
	printf("The server sent the data: %s\n", server_response);
    char server_message[256] = "You have reached the clnt!";
	send(network_socket, server_message, sizeof(server_message), 0);
	printf("data sent\n");
	close(network_socket);
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
	exit(0);
    return 0;
}
