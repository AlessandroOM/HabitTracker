using  habitTR;
using  habitTR.database;



internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        conectSqLite nc;
        nc = new conectSqLite("Data Source=data.sqlite");
        nc.Open();
        //nc.CreateTable();
        CRUDManager Manager;
        Manager = new CRUDManager(nc);

        int option = 0;
        //Console.Clear();
        while (option != 5)
        {
            Console.WriteLine("#=============================[Opções]=#"); 
            Console.WriteLine("#=[1- Incluir  ]=======================#"); 
            Console.WriteLine("#=[2- Alterar  ]=======================#"); 
            Console.WriteLine("#=[3- Apagar   ]=======================#"); 
            Console.WriteLine("#=[4- Consultar]=======================#"); 
            Console.WriteLine("#=[5- Sair     ]=======================#"); 
            option = Convert.ToInt32(Console.ReadLine()); 
            switch (option)
            {
                case 1 :
                       Manager.Insert();
                       break;

                case 2 : 
                       Manager.Update(); 
                       break; 

                case 3 :
                       Manager.Delete();
                       break;              

                case 4 :
                       Manager.View();
                       break;       
                
                
            }
        }
        nc.Dispose();
        Console.WriteLine("bye-bye, World!");
    }
}