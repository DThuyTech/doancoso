using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login.Controllers
{
    public class ManagerController : Controller
    {
        [Authorize (Roles ="Admin")] 
        public IActionResult Index()
        {
            return View();
        }
    }
}
