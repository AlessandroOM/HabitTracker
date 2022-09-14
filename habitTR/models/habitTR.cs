namespace habitTR.models;

public class habitTRack
{
   private int _Id; 
   private string _habit;
   private string _date;
   private int _quantity;

   public habitTRack(int id, string habit, string date, int quantity)
   {
    _Id = id;
    _habit = habit;
    _date = date;
    _quantity = quantity;
   }

   public habitTRack()
   {
    _Id = 0;
    _habit = "";
    _date = DateTime.Now.ToString();
    _quantity = 0;
   }

    public int Id { get => _Id; set => _Id = value; }
    public string Habit { get => _habit; set => _habit = value; }
    public string Date { get => _date; set => _date = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}
