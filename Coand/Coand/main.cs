namespace Coand
{
    public class main
    {
        public static void Main()
        {
            var pult = new Pult();
            var room1 = new Light("room1");
            var room2 = new Light("room2");
            var room3 = new Light("room3");
            var room1On = new LightOnCommand(room1);
            var room1Off = new LightOffCommand(room1);
            var room2On = new LightOnCommand(room2);
            var room2Off = new LightOffCommand(room2);
            var room3On = new LightOnCommand(room3);
            var room3Off = new LightOffCommand(room3);
            pult.Executer(room1On);
            pult.Executer(room1Off);
            pult.Executer(room2On);
            pult.Executer(room1On);
            pult.Executer(room3On);
            pult.UndoLast();
            pult.Executer(room3On);
            pult.Executer(room3Off);
        }
    }
}