using System;
using System.ComponentModel;
using SQLite;

namespace Core.Data.Model
{
    public class Vacation
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { set; get; }
        [NotNull]
        public string CreatedBy { set; get; }
        [Indexed]
        public int VacationTypeId { set; get; }
        [Indexed]
        public int VacationStatusId { set; get; }
        [NotNull]
        public DateTime Start { set; get; }
        [NotNull]
        public DateTime End { set; get; }
        [NotNull]
        public DateTime Created { set; get; }
        [NotNull]
        public bool IsSynchroniz { set; get; }
        [Indexed]
        public int SynchronizStatusId { set; get; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
