using MessagePack;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
using System.ComponentModel.DataAnnotations.Schema;

namespace login.Models
{
    public class Diet
    {
        [Key]
        public int IdDiet { get; set; }
        public int Buoi { get; set; }
        public int Day { get; set; }

        [ForeignKey("UserInfor")]

        public string IdUser { get; set; }

        [ForeignKey("Foods")]
        public int IdFood { get; set; }

        //public DateOnly date { get; set; }
        ////public void set()
        ////{
        ////    date =  new DateOnly();
        ////}
    }
}
