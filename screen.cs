using System.Text.Json;

class Screen<T>{
    public T[] FindResults(Func<T, bool> finder){
        return _array.Where<T>(finder).ToArray();
    }

    public void Finder(Validator<T> findingParam){
        Console.Clear();
        Console.Write(findingParam.Output);
        try{
            findingParam.GetInput(Console.ReadLine());
        } catch (ArgumentException e){
            Console.WriteLine("Invalid input");
             Console.Write("Enter a to try again, enter any key to exit finder");
            if (Console.ReadKey().Key == ConsoleKey.A) Finder(findingParam);
        }

        (new Screen<T>(_array.Where<T>(findingParam.Compare).ToArray<T>())).Iterate(); 

        

    }

    /// <summary>
    /// Cписок объектов, с которыми работает программа
    /// <summary/> 
    T[] _array;


    /// <summary>
    /// Возможные варианты сортировки
    /// <summary/> 
    SortingParameter<T>[] _sortingParams = new SortingParameter<T>[]{};

    Validator<T> [] _validadots = new Validator<T>[]{};




    /// <param name="arr">Список объектов для работы в консоли</param>
    public Screen(T[] arr){
        _array = arr;
    }


    /// <param name="arr">Список объектов для работы в консоли</param>
    /// <param name="sortingParams">Возможные варианты сортировки</param>
    public Screen(T[] arr, SortingParameter<T>[] sortingParams){
        _array = arr;
        _sortingParams = sortingParams;
    }


    public Screen(T[] arr, Validator<T>[] validators){
        _array = arr;
        _validadots = validators;
    }

    /// <summary>
    /// Первый объект, выводящийся на экран
    /// <summary/> 
    int _first = 0;
    int First{
        get{
            return _first;
        }
        set{
            if (!(value < 0 || value >= _array.Length)) _first = value;
        }
    }


    /// <summary>
    /// Количество объектов, выводящихся на экран
    /// <summary/>
    int _len { get{ return Console.WindowHeight - 5 - _sortingParams.Length - 3;}}


    /// <summary>
    /// Индекс, до которого объекты выводятся на экран
    /// <summary/>
    int ScreenEnd{
        get{
            return Math.Min(First + _len, _array.Length);
        }
    }


    /// <summary>
    /// Флаг, показывающий, есть ли объекты ниже последнего выведенного
    /// <summary/>
    bool EndFlag{
        get{
            return First + _len >= _array.Length;
        }
    }


    /// <summary>
    /// Вывод объектов на экран
    /// <summary/>
    void Print(){
        for (int i = _first; i < ScreenEnd; i++){
            Console.WriteLine(_array[i]);
        }
        if (EndFlag){
            Console.WriteLine("You reached the end of the file");
        }

    }


    /// <summary>
    /// Пролистывание объектов
    /// <summary/>
    void Scroll(int count){
        First += count;
    }

    /// <summary>
    /// Вывод отсортировнных объектов на экран
    /// <summary/>
    void PrintSorted(Comparison<T> comp){
        T[] arrayNew = new T[ScreenEnd - _first];
        for (int i = First; i < ScreenEnd; i++){
            arrayNew[i - First] = _array[i];
        }
        Array.Sort<T>(arrayNew, comp);
        foreach (T i in arrayNew){
            Console.WriteLine(i);
        }
        if (EndFlag){
            Console.WriteLine("You reached the end of the file");
        }

    }


    /// <summary>
    /// Выгрузка в JSON
    /// <summary/>
    void Upload(int begin, int end){
        Console.Clear();
        Console.Write("Enter filename: ");
        try{
        using (FileStream fs = new FileStream(Console.ReadLine(), FileMode.Create)){
                JsonSerializer.Serialize(fs, _array[begin..end]);
        }
        Console.WriteLine("File written");
        } catch (IOException){
            Console.WriteLine("Eccor occured. File not writter");
            Console.Write("Enter a to try again, enter any key to exit file writer");
            if (Console.ReadKey().Key == ConsoleKey.A) Upload(begin, end);
        }
    }
    
    static string menu = 
    @"
    Enter s to scroll;
    Enter t to scroll up;
    Enter u to upload all data;
    Enter a to upload all data on the screen;
    Enter e to exit;
    ";


    /// <summary>
    /// Пользовательское меню в консоли
    /// <summary/>
    string Menu{
        get{
            if (_sortingParams .Length != 0){
                string[] sorting = Array.ConvertAll<SortingParameter<T>, string>(_sortingParams, x => x.ScreenOutput);
                return menu + String.Join("\n    ", sorting);
            }
            else 
                return menu;
        }
    }
    
    /// <summary>
    /// Цикл работы с программой
    /// <summary/>
    public void Iterate(){
        while (true){
            Console.Clear();
            Print();
            Console.WriteLine(Menu);
            ConsoleKey key = Console.ReadKey().Key;
            Console.Clear();
            switch (key){
                case(ConsoleKey.S) : Scroll(1); continue; 
                case(ConsoleKey.T) : Scroll(-1); continue;

                case (ConsoleKey.U) : Upload(_first, ScreenEnd); continue;
                case (ConsoleKey.A) : Upload(0, _array.Length); continue;
                case (ConsoleKey.N) : Finder(_validadots[0]); continue;

                case (ConsoleKey.E) : return; 
                default:
                SortingParameter<T>? sortingParameter = _sortingParams.FirstOrDefault<SortingParameter<T>>(x => x.Key == key);
                if (sortingParameter != null){
                    PrintSorted(sortingParameter.Comparer);
                }
                else {
                    Console.Clear();
                    Console.WriteLine("Invalid input");
                }
                break;
            }
            Console.Write("Press any key to continue: ");
            key = Console.ReadKey().Key;
        }
        
    }


}