using System.ComponentModel.DataAnnotations;

namespace STC
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int accountType { get; set; }

        public override string ToString()
        {
            return username;
        }
    }
}