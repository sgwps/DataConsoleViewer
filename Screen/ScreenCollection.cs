using System.Text.Json;

namespace DataConsoleViewer.Screen;

/// <summary>
/// Инкапсулирует массив данных и вывода его части на экран
/// </summary>
class ScreenCollection<T> {
    /// <summary>
    /// Непосредственно массив данных
    /// </summary>
    public T[] Data {get; init;}

    /// <summary>
    /// Колличество объектов, выводимых на экран за раз
    /// </summary>
    /// <value></value>
    int ScreenLength {
        get; init;
    }

    int _first = 0;

    /// <summary>
    /// Индекс первого элемента, выводимого на экран
    /// </summary>
    /// <value></value>
    public int First{
        get { return _first; }
        set
        {
            if (!(value < 0 || value >= Data.Length))
                _first = value;
        }
    }

    /// <summary>
    /// Индекс последнего элемента, выводимого на экран
    /// </summary>
    /// <value></value>
    int ScreenEnd
    {
        get { return Math.Min(First + ScreenLength, Data.Length); }
    }

    /// <value>true, если не достаточно элементов, чтобы полностью заполнить экран</value>
    bool EndFlag
    {
        get { return First + ScreenLength >= Data.Length; }
    }


    public void PrintData(){
        for (int i = First; i < ScreenEnd; ++i) {
            Console.WriteLine($"{i + 1}. {Data[i]}");
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


    /// <summary>
    /// Сохранение массива данных в JSON файл
    /// </summary>
    /// <param name="filename"></param>
    public void SaveToJSON(string filename) {
        using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, Data);
            }
    }

}