using System;
namespace Coand
{
    public class Pult
    {
        ICommand lastCommand;
        public void Executer(ICommand command)
        {
            command.Execute();
            lastCommand = command;
            Console.WriteLine(lastCommand.Name+":Executed");
        }
        public void UndoLast()
        {
            if(lastCommand!=null)
            {
                lastCommand.Undo();
                Console.WriteLine(lastCommand.Name+":Undo");
                lastCommand=null;
            }
        }
    }
}