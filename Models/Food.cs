using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace login.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calo { get; set; }

        public string Bref { get; set; }
        [ForeignKey("TypeFood")]
        public int IdType { get; set; }

        public int chatbeo { get; set; }
        public int chatxo { get; set; }
        public int chatdam { get; set; }


        [ForeignKey("Loaimon")]
        public int LoaimonId { get; set; }
        //public Loaimon Loaimon { get; set; }

        [ForeignKey("DVT")]
        //public DVT DVT { get; set; }
        public int DVTid { get; set; }



        
       
        public TypeFood TypeFood { get; set; }
        public ICollection<FoodContent> contents { get; set; }
        public ICollection<DetailFoodNutri> detailFoodNutris { get; set; }
        public ICollection<Diet> diets { get; set; }
    }
}
