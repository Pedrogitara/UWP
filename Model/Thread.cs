using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace STC
{
    public class Thread
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string date { get; set; }
        public List<Post> posts { get; set; }
        public User user { get; set; }
    }
}