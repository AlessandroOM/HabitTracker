using habitTR.models;

namespace HabitTracker;

public interface IConectDataBase
{
    public void Open();
    public Boolean CreateTable();


     public habitTRack findById(int id);
     public void InsertHabit(habitTRack habit);
     public void Update(habitTRack habit);
     public void Delete(int idhabit);

     public List<habitTRack> Listhabit();

    public void Dispose();
}
