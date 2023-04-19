using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
