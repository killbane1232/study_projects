
namespace Coand
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
        void Undo();
    }

    public class NoCommand : ICommand
    {
        public string Name { get;}

        public void Execute()
        {
            
        }

        public void Undo()
        {
            
        }
    }
}