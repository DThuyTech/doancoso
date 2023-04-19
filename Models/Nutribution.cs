using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class Nutribution
    {
        [Key]
        public int iD { get; set; }

        public string name { get; set; }
        public string description { get; set; }


        public ICollection<DbTrain> dbTrains { get; set; }
        public ICollection<DetailFoodNutri> detailFoodNutris { get; set; }
    }
}
