using habitTR.models;
using HabitTracker;
using Microsoft.Data.Sqlite;

namespace habitTR.database
{
    public class conectSqLite : IConectDataBase
    {
        private readonly string _conectionStr; 
        private SqliteConnection _connection;
        public conectSqLite(string conectionStr)
        {
            _conectionStr = conectionStr;
             _connection = new SqliteConnection(_conectionStr);
        }

        private void WriteMensErrorInit()
        {
            Console.WriteLine("#==[ERROR]===============#");
            Console.WriteLine("#=                      =#");
        }

        private void WriteMensErrorEnd()
        {
            Console.WriteLine("#=                      =#");
            Console.WriteLine("#========================#");
        }
        

        public void Open()
        {
           
            try
            {
               _connection.Open();  
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
    
        }

        public Boolean CreateTable()
        {
            string StrCmd = "CREATE TABLE HabitTable (";
            StrCmd = StrCmd + "ID integer primary key autoincrement,";
            StrCmd = StrCmd +  "HABIT TEXT NOT NULL ,";
            StrCmd = StrCmd +  "DATE	TEXT NOT NULL,";
            StrCmd = StrCmd +  "QUANTITY	INTEGER)";
//Id integer primary key autoincrement
            var cmd = _connection.CreateCommand();
            cmd.CommandText = StrCmd;
            try
            {
               cmd.ExecuteNonQuery(); 
            }
            catch (System.Exception ex)
            {
                 Console.WriteLine(ex.Message);
            }
            
            return true;
        }

        public habitTRack findById(int id)
        {
            habitTRack result = new habitTRack();
            string StrCmd = "SELECT ID, HABIT, DATE, QUANTITY";
            StrCmd = StrCmd + $" FROM HabitTable A WHERE A.ID={id}";
           
            var cmd = _connection.CreateCommand();
            cmd.CommandText = StrCmd;
                     
           
            try
            {
                var reader = cmd.ExecuteReader();
                reader.Read();
                if  (reader.HasRows)
                {
                    result.Id = reader.GetInt32(0);
                    result.Habit = reader.GetString(1);
                    result.Date = reader.GetString(2);
                    result.Quantity = reader.GetInt32(3);
                }    
            }
            catch (System.Exception ex)
            {
                 WriteMensErrorInit() ;
                 Console.WriteLine(ex.Message);
                 WriteMensErrorEnd();
                
            }

            return result;

        }

        public void Update(habitTRack habit)
        {
           var sqlDelete = _connection.CreateCommand();
           sqlDelete.CommandText = @"
                UPDATE HabitTable 
                SET HABIT = $habit, 
                    DATE = $date,
                    QUANTITY =$qt
                WHERE id = $id
            ";

            // Bind the parameters to the query.
           sqlDelete.Parameters.AddWithValue("$habit", habit.Habit);
           sqlDelete.Parameters.AddWithValue("$date", habit.Date);
           sqlDelete.Parameters.AddWithValue("$qt", habit.Quantity);
           sqlDelete.Parameters.AddWithValue("$id", habit.Id);
          
           try
           {
               sqlDelete.ExecuteNonQuery(); 
           }
           catch (System.Exception ex)
           {
                 WriteMensErrorInit() ;
                 Console.WriteLine(ex.Message);
                 WriteMensErrorEnd();
           }
        }

        public void Delete(int idhabit)
        {
           var sqlDelete = _connection.CreateCommand();
           sqlDelete.CommandText = @"
                DELETE from HabitTable 
                WHERE id = $id
            ";

            // Bind the parameters to the query.
           sqlDelete.Parameters.AddWithValue("$id", idhabit);
          
           try
           {
               sqlDelete.ExecuteNonQuery(); 
           }
           catch (System.Exception ex)
           {
                 WriteMensErrorInit() ;
                 Console.WriteLine(ex.Message);
                 WriteMensErrorEnd();
           }
        }

        public List<habitTRack> Listhabit()
        {
            List<habitTRack> Habits = new List<habitTRack>();
            string StrCmd = "SELECT ID, HABIT, DATE, QUANTITY";
            StrCmd = StrCmd + " FROM HabitTable";
           
            var cmd = _connection.CreateCommand();
            cmd.CommandText = StrCmd;
            try
            {
                var reader = cmd.ExecuteReader();
                //reader.Read();
               
                while (reader.Read())
                {
                    habitTRack ht = new habitTRack();
                    //Console.WriteLine( reader.GetInt32(2));
                    ht.Id = reader.GetInt32(0);
                    ht.Habit = reader.GetString(1);
                    ht.Date = reader.GetString(2);
                    ht.Quantity = reader.GetInt32(3);
                    Habits.Add(ht);
                    //Console.ReadKey();
                    ht = null;
                }
              
            }
            catch (System.Exception ex)
            {
                 WriteMensErrorInit() ;
                 Console.WriteLine(ex.Message);
                 WriteMensErrorEnd();
                
            }


            return Habits;
        }

        public void InsertHabit(habitTRack habit)
        {
           string StrCmd = "INSERT INTO HabitTable (HABIT, DATE, QUANTITY )" ;
           StrCmd = StrCmd +  $" VALUES ('";
           StrCmd = StrCmd +  $"{habit.Habit}"+"','";
           StrCmd = StrCmd +  $"{habit.Date}"+"','";
           StrCmd = StrCmd +  $"{habit.Quantity}"+"')";
           var cmd = _connection.CreateCommand();
           cmd.CommandText = StrCmd;
           try
           {
               cmd.ExecuteNonQuery(); 
           }
           catch (System.Exception ex)
           {
                 WriteMensErrorInit() ;
                 Console.WriteLine(ex.Message);
                 WriteMensErrorEnd();
           }

        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
               
            }
        }
    }
}