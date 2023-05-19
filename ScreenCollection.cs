using System.Text.Json;

namespace DataConsoleViewer.Screen;

class ScreenCollection<T> {
    public T[] Data {get; init;}

    int ScreenLength {
        get; init;
    }

    int _first = 0;
    public int First{
        get { return _first; }
        set
        {
            if (!(value < 0 || value >= Data.Length))
                _first = value;
        }
    }

    int ScreenEnd
    {
        get { return Math.Min(First + ScreenLength, Data.Length); }
    }

    bool EndFlag
    {
        get { return First + ScreenLength >= Data.Length; }
    }


    public void PrintData(){
        for (int i = First; i < ScreenEnd; ++i) {
            Console.WriteLine($"{i + 1}. {i}");
        }
        if (EndFlag)
        {
            Console.WriteLine("You reached the end of the data range");
        }
    }

    public ScreenCollection(T[] array, int screenLength) {
        Data = array;
        ScreenLength = screenLength;
    }


    public void SaveToJSON(string filename) {
        using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, Data);
            }
    }

}