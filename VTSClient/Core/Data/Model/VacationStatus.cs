using SQLite;

namespace Core.Data.Model
{
    public class VacationStatus
    {
        [PrimaryKey, AutoIncrement]
        public int Id { set; get; }
        public string Name { set; get; }
    }
}
