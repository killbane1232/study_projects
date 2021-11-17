using System;

namespace Coand
{
    public class Light
    {
        public string Name;

        public Light(string room)
        {
            Name = room;
        }
        public void On()
        {
            Console.WriteLine(Name + " light is on!");
        }
        public void Off()
        {
            Console.WriteLine(Name + " light has been turned off!");
        }
    }
    public class LightOnCommand : ICommand
    {
        private Light _light;

        public string Name
        {
            get { return _light.Name; }
        }
        public LightOnCommand(Light light)
        {
            this._light = light;
        }
        public void Execute()
        {
            _light.On();
        }

        public void Undo()
        {
            _light.Off();
        }
    }

    public class LightOffCommand : ICommand
    {
        private Light _light;

        public string Name
        {
            get { return _light.Name; }
        }
        public LightOffCommand(Light light)
        {
            this._light = light;
        }
        public void Execute()
        {
            _light.Off();
        }

        public void Undo()
        {
            _light.On();
        }
    }
}