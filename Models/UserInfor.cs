using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace login.Models
{
    public class UserInfor
    {
        [Key]
        public string Id { get; set; }
        public int heights { get; set; }


        public int ages { get; set; }

        public int sexs { get; set; }

        public int weigh { get; set; }

        public string role { get; set; }

        public int Activ { get; set; }

        
        public int Mucdich { get; set; }
    }
}
