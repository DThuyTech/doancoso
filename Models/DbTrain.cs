using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login.Models
{
    public class DbTrain
    {
        [Key]
        public int Id { get; set; }
        public int Gender { get; set; }
        
        public int Age { get; set; }
        public int Activ { get; set; }
        public int Color { get; set; }
        public int KeyEmotion { get; set; }
        public int KeyTaste { get; set; }

        [ForeignKey("Nutribution")]
        public int NutributionId { get; set; }


    }
}
