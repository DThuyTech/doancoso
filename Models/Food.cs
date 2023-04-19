using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Calo { get; set; }

        public string Bref { get; set; }
        [ForeignKey("TypeFood")]
        public int IdType { get; set; }
        public TypeFood TypeFood { get; set; }
        public ICollection<FoodContent> contents { get; set; }
        public ICollection<Diet> diets { get; set; }
    }
}
