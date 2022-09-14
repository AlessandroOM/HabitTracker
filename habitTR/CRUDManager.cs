using HabitTracker;
using habitTR.models;

namespace habitTR;

public class CRUDManager
{
    private readonly IConectDataBase _connection;

    private habitTRack _habit;

    private void ShowModel(bool showId)
    {
        Console.Clear();
        Console.WriteLine("#=============================[Consultar]=#"); 
        Console.WriteLine("#=  Habito     : " + _habit.Habit + " =#"); 
        Console.WriteLine("#=  Quantidade : " + _habit.Quantity + " =#"); 
        Console.WriteLine("#=  Data       : " + _habit.Date + " =#"); 

    }

    private int ShowListHabits(string caption)
    {
       var hts = _connection.Listhabit();
       Console.WriteLine($"#====================[{caption}]=#");
       Console.WriteLine("#=ID   Habit        Date    Qty =#"); 
       foreach (var item in hts)
       {
            _habit = item;
            Console.WriteLine($"#={_habit.Id}  {_habit.Habit} {_habit.Date} {_habit.Quantity} ==#");   
           
       }
       Console.WriteLine("#======[Informe o ID para desejado ou Zero para sair.]=#");
       int id = Convert.ToInt32( Console.ReadLine());
       return id;
    }

    private string GetStr(bool isEdit, string value)
    {
        string result = "NONE";
        if (isEdit)
        {
            Console.WriteLine($"#== Valor atual: {value} =#");
        }
        result = Console.ReadLine(); 
        if (result.Trim() == "" || result.Trim() == "0")
        {
                result = "NONE";
        }
        
        return result;
    }

    private bool GetInfo(bool isEdit)
    {
        string confirm = "N";
        string valueStr;
        bool bExit = false;
        while (!bExit)
        {
            Console.WriteLine("#== Informe o Hábito ou Vazio para sair=#"); 
            valueStr = GetStr(isEdit, _habit.Habit);
            if (valueStr.ToLower() != "none")
            {
                _habit.Habit = valueStr;
            } else
            {
                bExit = true;
            }

            if (!bExit)
            {
                Console.WriteLine("#== Informe a quantidade ou Zero para sair=#"); 
                 valueStr = GetStr(isEdit, Convert.ToString(_habit.Quantity));
                if (valueStr.ToLower() != "none")
                {
                    _habit.Quantity = Convert.ToInt32(valueStr);
                } else
                {
                    bExit = true;
                }
            }
            
            if (!bExit)
            {
                GetDate(isEdit);
                bExit = _habit.Habit.Length == 0;
            }

            if (!bExit)
            {
                ShowModel(isEdit ? true : false);
                Console.WriteLine("#== Informações corretas?(S/N) =#"); 
                confirm =  Console.ReadLine();
                bExit = confirm.ToLower() == "s";
               
            }
            
        }
        return bExit;
    }

    private bool GetDate(bool isEdit)
    {
       bool isValid = false;
       while (!isValid)
       {
            Console.WriteLine("#== Informe a data formato (DD/MM/YYYY) ou Vazio para sair=#"); 
             if (isEdit)
             {
                 Console.WriteLine($"#== Valor atual: {_habit.Date} =#");
             }
            _habit.Date = Console.ReadLine();
            if (_habit.Date.Length > 0)
            {
                try
                {
                    Convert.ToDateTime( _habit.Date);
                    isValid = true;
                }
                catch (System.Exception)
                {
                    
                    isValid = false;
                }   
            } else
            {
                isValid = true;
            }
            
       }
       return isValid;
    }

    public CRUDManager(IConectDataBase connection)
    {
        _connection = connection;
        _habit = new habitTRack();
    }

    public void Insert()
    {
        Console.Clear();
        Console.WriteLine("#=============================[Inclusão]=#"); 
        bool ok = false;
        while (!ok)
        {
            ok = GetInfo(false);
            if (ok)
            {
                _connection.InsertHabit(_habit);
                ok = true;
            }
            
        }
        
    }

    public void Update()
    {
        Console.Clear();
        Console.WriteLine("#=============================[Alterar]=#"); 
        int id = -1;
        bool ok = false;
             
         while (id != 0)
         {
            Console.Clear();
            id = ShowListHabits("Alterar");
            if (id > 0)
            {
                 _habit = _connection.findById(id);
                 ShowModel(true);
                 ok = GetInfo(true);
                
                 if (ok)
                 {
                      _connection.Update(_habit);
                 }
                   
            }

            }
    
    }

    public void Delete()
    {
        Console.Clear();
        Console.WriteLine("#=============================[Excluir]=#"); 
        int id = -1;
        string confirm = "N";
       
         while (id != 0)
         {
            Console.Clear();
            id = ShowListHabits("Excluir");
            if (id > 0)
            {
                 _habit = _connection.findById(id);
                 ShowModel(true);
                 Console.WriteLine("#== Confirma a exclusão?(S/N) =#"); 
                 confirm =  Console.ReadLine();
                 if (confirm.ToLower() == "s")
                 {
                   _connection.Delete(id);
                 }
                   
            }

            }

    }

    public void View()
    {
         int id = -1;
         while (id != 0)
         {
            Console.Clear();
            id = ShowListHabits("Consultar");
            
            if (id > 0)
            {
                 _habit = _connection.findById(id);
                 ShowModel(true);
                 Console.WriteLine("#== Pressione qualquer tecla para continuar =#");  
                 Console.ReadKey();

            }
         }
   
    }
}
