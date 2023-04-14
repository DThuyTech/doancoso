using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class Recipes
    {
        [Key]
        public int Id { get; set; }
        public int IdFood { get; set; }
        public string Recipe { get; set; }
    }
}
