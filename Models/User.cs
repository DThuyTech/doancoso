using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace login.Models
{
    public class User : IdentityUser
    {
        public float height { get; set; }
        public float weight { get; set; }
        public int age { get; set; }
        public int sex { get; set; }
        public User() { }
        public IdentityUser? IdentityUser { get; set; }
        public ICollection<Diet> diets { get; set; }

    }
}
