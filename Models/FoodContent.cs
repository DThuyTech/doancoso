using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login.Models
{
    public class FoodContent
    {
        [Key]

        public int Id { get; set; }
        public string Recipes { get; set; }
        public string ReContent { get; set; }

        public string Img { get; set; }
        [ForeignKey("Food")]
        public int FoodId { get; set; }
    }
}
