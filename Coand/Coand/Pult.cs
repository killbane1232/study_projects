using System.Windows.Input;
using System;
namespace Coand
{
    public class Pult
    {
        ICommand[] onCommand;
        ICommand[] offCommand;
        ICommand lastCommand;
        public Pult()
        {
            var room1 = new Light("room1");
            var room2 = new Light("room2");
            var room3 = new Light("room3");
            var room1On = new LightOnCommand(room1);
            var room1Off = new LightOffCommand(room1);
            var room2On = new LightOnCommand(room2);
            var room2Off = new LightOffCommand(room2);
            var room3On = new LightOnCommand(room3);
            var room3Off = new LightOffCommand(room3);
            onCommand = new ICommand[]
            {
                room1On,room2On,room3On
            };
            offCommand = new ICommand[]
            {
                room1Off,room2Off,room3Off
            };
        }
        public void OnOnButtonClick(int id)
        {
            onCommand[id].Execute();
            lastCommand = onCommand[id];
            Console.WriteLine(lastCommand.Name+":Execute");
        }
        public void OnOffButtonClick(int id)
        {
            offCommand[id].Execute();
            lastCommand = offCommand[id];
            Console.WriteLine(lastCommand.Name+":Execute");
        }
        public void OnUndoButtonClick()
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