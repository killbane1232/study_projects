namespace Coand
{
    public class main
    {
        public static void Main()
        {
            var pult = new Pult();
            pult.OnOnButtonClick(0);
            pult.OnOffButtonClick(0);
            pult.OnOnButtonClick(1);
            pult.OnOnButtonClick(0);
            pult.OnOnButtonClick(2);
            pult.OnUndoButtonClick();
            pult.OnOnButtonClick(2);
            pult.OnOffButtonClick(2);
        }
    }
}