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
        public DbSet<Loaimon> loaimons { get; set; }
        public DbSet<DVT> dVTs { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<TypeFood> Types { get; set; }
        public DbSet<FoodContent> foodContents { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Nutribution> nutributions { get; set; }
        public DbSet<DetailFoodNutri> detailFoodNutris { get; set; }
        public DbSet<UserInfor> userinfors { get; set; }
        public DbSet<Diet> diets { get; set; }
        public DbSet<DbTrain> trains { get; set; }
    }
}
