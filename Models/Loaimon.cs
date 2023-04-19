using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class Loaimon
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Food> Foods { get; set; }
    }
}
