using DataConsoleViewer.ScreenActions;
using DataConsoleViewer.ScreenActions.RangeSelection;
using DataConsoleViewer.ScreenActions.Selection;
using DataConsoleViewer.ScreenActions.Sorting;

namespace DataConsoleViewer.Entities;

static class PublicationActions{
    static public string[] instructions = {
        "sort by date",
        "sort by name",
        "find by word in name",
        "select publication of a month",
        "select publications of a period"
    };


    static public ConsoleKey[] keys = {
        ConsoleKey.D,
        ConsoleKey.N,
        ConsoleKey.F,
        ConsoleKey.O,
        ConsoleKey.R
    };

    static public MenuOption[] options = new MenuOption[instructions.Length];

    static void SetMenuOptions(){

        for (int i = 0; i < instructions.Length; i++) {
            options[i] = new MenuOption(keys[i], instructions[i]);
        }
    }


    static PublicationActions(){
        SetMenuOptions();
    }

     static public IScreenAction<Publication>[] GetActionArray(){
        return new IScreenAction<Publication>[]{
            new SortingAction<Publication>(PublicationOperations.CompareByDate, options[0]),
            new SortingAction<Publication>(PublicationOperations.CompareByName, options[1]),
            new SelectingAction<Publication, string>(PublicationOperations.FindByKeyWord, InputConverters.ParseStringInput, options[2], "Enter the key word"),
            new SelectingAction<Publication, DateOnly>(PublicationOperations.FindByMonth, InputConverters.ParseDateYYYYMM, options[3], "Enter the date in YYYYMM format"),
            new RangeSelectionAction<Publication, DateOnly>(options[4],InputConverters.ParseDateYYYYMMDD, "Enter the start of the period (format: YYYYMMDD)", "Enter the end of the period (format: YYYYMMDD)",  PublicationOperations.GetPublicationDate)
        };
    }
}