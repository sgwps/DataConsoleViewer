
namespace DataConsoleViewer.Entities;

/// <summary>
/// Статические действия, для совершения операций при работе с данными типа Publication
/// </summary>
static class PublicationOperations{
    public static int CompareByDate(Publication a, Publication b) {
        return a.Date.CompareTo(b.Date);
    }


    public static int CompareByName(Publication a, Publication b) {
        return a.Name.CompareTo(b.Name);
    }


    public static bool FindByKeyWord(Publication publication, string value) {
        return publication.Name.Split(" ").Contains(value);
    }

    public static bool FindByMonth(Publication publication, DateOnly date) {
        return publication.Date.Month == date.Month & publication.Date.Year == date.Year;
    }


    public static DateOnly GetPublicationDate(Publication publication) {
        return publication.Date;
    }


}