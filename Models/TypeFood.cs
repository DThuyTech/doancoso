using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class TypeFood
    {
       
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<Food> foods { get; set; }
        
    }
}
