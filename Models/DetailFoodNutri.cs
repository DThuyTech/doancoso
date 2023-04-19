using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace login.Models
{
    
    public class DetailFoodNutri
    {

        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        [ForeignKey("Nutribution")]
        public int NutributionId { get; set; }


        [AllowNull]
        public float Quantity { get; set; }
    }
}
