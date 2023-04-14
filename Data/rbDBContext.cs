using login.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace login.Data
{
    public class rbDBContext : IdentityDbContext
    {
        public rbDBContext(DbContextOptions<rbDBContext> options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<TypeFood> Types { get; set; }
        public DbSet<FoodContent> foodContents { get; set; }
        public DbSet<Recipes> Recipes { get; set; }

        public DbSet<UserInfor> userinfors { get; set; }
    }
}
