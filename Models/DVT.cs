
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class DVT
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Food> foods { get; set; }
    }
}
