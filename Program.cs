using DataConsoleViewer.CSVParser;
using DataConsoleViewer.Entities;
using DataConsoleViewer.Screen;
using DataConsoleViewer.ScreenActions;

class Program
{
    static void Main()
    {
        Publication[] array = ParserScreen<Publication>.Parse(new PublicationCSVParser());
        IScreenAction<Publication>[] actions = PublicationActions.GetActionArray();
        (new Screen<Publication>(array, actions)).Iterate();
    }
}
