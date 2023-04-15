using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using login.Models;
using login.Data;
namespace login.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {


        IList<Food> listFinal = new List<Food>();
        List<FoodContent> foodContents = new List<FoodContent>();
        List<FoodContent> foodContents1 = new List<FoodContent>();  

        rbDBContext _typeFoodDBContext;

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<rbDBContext> _userManager;
        public ManagerController(IList<Food> listFinal, List<FoodContent> foodContents, rbDBContext typeFoodDBContext, ILogger<HomeController> logger, UserManager<rbDBContext> userManager)
        {
            this.listFinal = listFinal;
            this.foodContents = foodContents;
            _typeFoodDBContext = typeFoodDBContext;
            _logger = logger;
            _userManager = userManager;
            foodContents = _typeFoodDBContext.foodContents.ToList();
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateFood(Food food)
        {
            List<Food> foods = _typeFoodDBContext.Foods.ToList();
            if (foods.FirstOrDefault(p => p.Name == food.Name) != null)
            {
                ViewBag.data = "Đã có món này trong database";
            }
            else
            {
                Food food1 = food;
                _typeFoodDBContext.Foods.Add(food1);
                _typeFoodDBContext.SaveChanges();
                ViewBag.data = "Đã them thanh cong";
                return View();
            }
            return View();
        }

        public IActionResult CreateFood()
        {

            return View();
        }

        [HttpPost]
        public IActionResult EditFood(Food food)
        {

            Food food1 = new Food();
            food1 = _typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == food.Id);
            _typeFoodDBContext.Foods.Add(food1);
            _typeFoodDBContext.SaveChanges();
            return View("Admin", "Home");
        }
        public IActionResult EditFood(int? id)
        {
            var food = _typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == id);

            return View(food);
        }
    }
}
