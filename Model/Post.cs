using System.ComponentModel.DataAnnotations;

namespace STC
{
    public class Post
    {
        [Key]
        public int id { get; set; }
        public string content { get; set; }
        public string date { get; set; }
        public User user { get; set; }
    }
}