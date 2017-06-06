using System;
using SQLite;

namespace Core.Data.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        [NotNull]
        public String Login { get; set; }
        [NotNull]
        public String Password { get; set; }
    }
}
