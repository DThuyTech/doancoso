using login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using login.Models;
using login.Data;

namespace login.Controllers
{
   
    public class HomeController : Controller
    {

      
        IList<Food> listFinal = new List<Food>();
        List<FoodContent> foodContents = new List<FoodContent>();

        rbDBContext _typeFoodDBContext;

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<rbDBContext> _userManager;



        public HomeController(ILogger<HomeController> logger, rbDBContext applicationDbContext/*UserManager<ApplicationDbContext> userManager*/)
        {
            //_userManager = userManager;
            _logger = logger;
            _typeFoodDBContext = applicationDbContext;
            listFinal = _typeFoodDBContext.Foods.ToList();

            _logger = logger;
            foodContents = _typeFoodDBContext.foodContents.ToList();
        }

        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}