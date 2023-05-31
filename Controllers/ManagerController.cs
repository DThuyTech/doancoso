using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using login.Models;
using login.Data;
using Microsoft.EntityFrameworkCore;
using login.Viewmodels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lucene.Net.Search;
using System;
using login.Logic;
using Google.Cloud.Firestore;
using MessagePack;

namespace login.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        IList<Food> listFinal = new List<Food>();
        List<FoodContent> foodContents = new List<FoodContent>();
        List<FoodContent> foodContents1 = new List<FoodContent>();

        rbDBContext _typeFoodDBContext;

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<rbDBContext> _userManager;
        public ManagerController(rbDBContext typeFoodDBContext, ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _environment = environment;

            _typeFoodDBContext = typeFoodDBContext;
            _logger = logger;

            listFinal = _typeFoodDBContext.Foods.ToList();
            foodContents = _typeFoodDBContext.foodContents.ToList();
        }


        public IActionResult ListLoaimon()
        {
            List<Loaimon> model = _typeFoodDBContext.loaimons.ToList();
            ViewBag.data = _typeFoodDBContext.Foods.ToList();

            return View(model);
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
                return View("ListFood");
            }
            return View("EditFood");
        }


        [HttpPost]
        public IActionResult CreateNutri(Nutribution nutribution)
        {
            List<Nutribution> nutributions = _typeFoodDBContext.nutributions.ToList();
            if (nutributions.FirstOrDefault(p => p.name == nutribution.name) != null)
            {
                ViewBag.data = "Đã có dinh duong này trong database";
            }
            else
            {
                Nutribution food1 = nutribution;
                _typeFoodDBContext.nutributions.Add(food1);
                _typeFoodDBContext.SaveChanges();
                ViewBag.data = "Đã them thanh cong";
                return RedirectToAction("ListNutriFood");
            }
            return View("CreateNutri");
        }
        public IActionResult ListDVt()
        {
            List<DVT> model = _typeFoodDBContext.dVTs.ToList();
            List<Food> foods = _typeFoodDBContext.Foods.ToList();
            ViewBag.foods = foods;
            return View(model);
        }
        public IActionResult ListUser()
        {

            List<UserInfor> userInfors = _typeFoodDBContext.userinfors.ToList();

            ViewBag.userInfor = userInfors;


            return View(userInfors);
        }

        [HttpPost]
        public IActionResult ListFood(string? search, int? idpage)
        {
            int page = 0;
            if (idpage == null)
            {
                page = 0;
            }
            else
            {
                page = idpage.Value;
            }
            List<Food> listfoddlaod = listFinal.ToList();
            int count = listfoddlaod.Count() / 50 + 1;
            ViewBag.pagenumber = count;
            if (search != null)
            {
                listfoddlaod = listFinal.Where(s => s.Name.Contains(search)).ToList();
                ViewBag.datasanm = listFinal[0].Name;
                ViewBag.foodContent = _typeFoodDBContext.foodContents.ToList();

            }

            List<Food> foodshow = new List<Food>();

            for (int i = 0; i < 50; i++)
            {
                foodshow.Add(listfoddlaod[i + page * 50]);

            }


            return View(foodshow);
        }
        public IActionResult ListFood()
        {
            List<Food> model = _typeFoodDBContext.Foods.ToList();
            int count = model.Count / 50 + 1;
            List<Food> modelfianl = new List<Food>();
            for (int i = 0; i < 50; i++)
            {
                modelfianl.Add(model[i + 0 * 50]);

            }
            ViewBag.pagenumber = count;
            return View(modelfianl);
        }

        public ContentFoodViewmodel GetContentFoodViewmodel()
        {
            ContentFoodViewmodel contentFoodViewmodel = new ContentFoodViewmodel();
            contentFoodViewmodel.ListidtypeFood = new List<SelectListItem>();
            contentFoodViewmodel.ListidLoaimon = new List<SelectListItem>();
            contentFoodViewmodel.ListIddvt = new List<SelectListItem>();
            List<TypeFood> typeFoods = _typeFoodDBContext.Types.ToList();
            List<Loaimon> loaimon = _typeFoodDBContext.loaimons.ToList();
            List<DVT> dVTs = _typeFoodDBContext.dVTs.ToList();
            foreach (TypeFood item in typeFoods)
            {
                contentFoodViewmodel.ListidtypeFood.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }

            foreach (Loaimon item in loaimon)
            {
                contentFoodViewmodel.ListidLoaimon.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }


            foreach (DVT item in dVTs)
            {
                contentFoodViewmodel.ListIddvt.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return contentFoodViewmodel;
        }
        public IActionResult CreateFood()
        {


            ViewBag.modelType = GetContentFoodViewmodel();
            return View();
        }


        public IActionResult CreateNutri()
        {
            return View();
        }
        public IActionResult CreateLoaimon()
        {


            return View();
        }


        public IActionResult ListNutriFood()
        {

            ViewBag.foods = _typeFoodDBContext.Foods.ToList();
            List<Nutribution> nutri = _typeFoodDBContext.nutributions.ToList();
            return View(nutri);
        }

        [HttpPost]
        public IActionResult ListDBtrain(int? idpage)
        {

            int page = 0;
            if (idpage == null)
            {
                page = 0;
            }
            else
            {
                page = idpage.Value;
            }
            List<DbTrain> listfoddlaod = _typeFoodDBContext.trains.ToList();
            int count = listfoddlaod.Count() / 50 + 1;
            ViewBag.pagenumber = count;



            List<DbTrain> foodshow = new List<DbTrain>();

            for (int i = 0; i < 50; i++)
            {
                foodshow.Add(listfoddlaod[i + page * 50]);

            }


            return View(foodshow);
        }

        public IActionResult ListDBtrain()
        {
            List<DbTrain> model = _typeFoodDBContext.trains.ToList();
            int count = model.Count / 50 + 1;
            List<DbTrain> modelfianl = new List<DbTrain>();
            for (int i = 0; i < 50; i++)
            {
                modelfianl.Add(model[i + 0 * 50]);

            }
            ViewBag.pagenumber = count;
            return View(modelfianl);
        }
        [HttpPost]
        public IActionResult CreateLoaimon(Loaimon loaimon)
        {
            ViewBag.datas = _typeFoodDBContext.Foods.ToList();
            List<Loaimon> loaimons = _typeFoodDBContext.loaimons.ToList();
            if (loaimons.FirstOrDefault(p => p.Name == loaimon.Name) != null)
            {
                ViewBag.data = "Đã có loại này trong database";
            }
            else
            {
                Loaimon loaimon1 = loaimon;
                _typeFoodDBContext.loaimons.Add(loaimon1);
                _typeFoodDBContext.SaveChanges();
                ViewBag.data = "Đã them thanh cong";
                return RedirectToAction("ListLoaimon");
            }
            return View("CreateLoaimon");
        }

        [HttpPost]
        public IActionResult EditNutri(Nutribution nutribution)
        {
           if(_typeFoodDBContext.nutributions.Where(p=>p.name == nutribution.name).Count() > 0)
            {
                ViewBag.erro = "Dinh dưỡng này đã có trong cơ sở dữ liệu";
                return View("EditNutri",nutribution);
               
            }
            else
            {
                Nutribution nutribution1 = _typeFoodDBContext.nutributions.FirstOrDefault(p => p.iD == nutribution.iD);
                nutribution1.name = nutribution.name;
                _typeFoodDBContext.SaveChanges();
                return RedirectToAction("ListNutriFood");
            }
        }
        public IActionResult EditNutri(int id)
        {
            Nutribution nutribution = _typeFoodDBContext.nutributions.FirstOrDefault(p=>p.iD == id);
            return View(nutribution);
        }
        [HttpPost]
        public IActionResult Editloaimon(Loaimon loaimon)
        {
            if (_typeFoodDBContext.loaimons.FirstOrDefault(p => p.Name == loaimon.Name) == null)
            {
                Loaimon loaimon1 = _typeFoodDBContext.loaimons.FirstOrDefault(p => p.Id == loaimon.Id);
                loaimon1.Name = loaimon.Name;
                _typeFoodDBContext.SaveChanges();

                return RedirectToAction("ListLoaimon");
            }
            else
            {
                return View("Editloaimon",loaimon);
            }
        }
        public IActionResult Editloaimon(int id)
        {
            Loaimon loaimon = _typeFoodDBContext.loaimons.FirstOrDefault(p => p.Id == id);
            return View(loaimon);
        }
        [HttpPost]
        public IActionResult EditFood(Food food, IFormFile? imageFile, String? content ,String? recipe,String? infood)
        {

            Food food1 = new Food();
        
            food1 = _typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == food.Id);

            food1.Name = food.Name;
            food1.Calo=food.Calo;
            food1.Bref=food.Bref;
            food1.TypeFood= food.TypeFood;

            food1.chatbeo = food.chatbeo;
            food1.chatxo = food.chatxo;
            food1.chatdam = food.chatdam;
            food1.DVTid = food.DVTid;
            food1.LoaimonId = food.LoaimonId;
            _typeFoodDBContext.Foods.Update(food1);
            _typeFoodDBContext.SaveChanges();
            if (imageFile != null && imageFile.Length > 0)
            {
                // Generate a unique file name for the uploaded image
                string fileName = food.Id.ToString()+".jpg";

                // Specify the directory where you want to save the image
                string imagePath = Path.Combine("C:\\Users\\ASUS\\source\\Repos3\\Food\\wwwroot\\Food\\ImageFood\\", fileName);

                // Save the image file to the specified directory
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                // Optionally, you can store the imagePath in your database or perform any additional operations.

                
            }
            if (content != null)
            {
                string filenametxt = "C:\\Users\\ASUS\\source\\Repos3\\Food\\wwwroot\\Food\\ContentFood\\" + food.Id.ToString() + ".txt";
                System.IO.File.WriteAllText(filenametxt, content);
            }
            if (recipe != null)
            {
                string filenametxt = "C:\\Users\\ASUS\\source\\Repos3\\Food\\wwwroot\\Food\\RecipeFood\\" + food.Id.ToString() + ".txt";
                System.IO.File.WriteAllText(filenametxt, recipe);
               
            }
            if (infood != null)
            {
                string filenametxt = "C:\\Users\\ASUS\\source\\Repos3\\Food\\wwwroot\\Food\\InFood\\" + food.Id.ToString() + ".txt";
                System.IO.File.WriteAllText(filenametxt, infood);

            }
            return RedirectToAction("ListFood");
        }
        public IActionResult EditFood(int? id)
        {
            var food = _typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == id);

            ViewBag.modelType = GetContentFoodViewmodel();
            List<SelectListItem> modelNutri = new List<SelectListItem>();
            List<DetailFoodNutri> modelNutridt = _typeFoodDBContext.detailFoodNutris.Where(p=>p.FoodId == id).ToList();
            foreach (DetailFoodNutri item in modelNutridt)
            {
                modelNutri.Add(new SelectListItem { Text = _typeFoodDBContext.nutributions.FirstOrDefault(p=>p.iD == item.NutributionId).name, Value = item.Id.ToString() });
            }
            string recipefile = "C:\\Users\\ASUS\\source\\Repos3\\Food\\wwwroot\\Food\\RecipeFood\\" + id + ".txt";
            string inFood = "C:\\Users\\ASUS\\source\\Repos3\\Food\\wwwroot\\Food\\InFood\\" + id + ".txt";
            string content = "C:\\Users\\ASUS\\source\\Repos3\\Food\\wwwroot\\Food\\ContentFood\\" + id + ".txt";
            if (System.IO.File.Exists(recipefile))
            {
                ViewBag.modelRecipe = System.IO.File.ReadAllText(recipefile);
            }
            else
            {
                ViewBag.modelRecipe = "Chua co";
            }

            if (System.IO.File.Exists(content))
            {
                ViewBag.modelContent = System.IO.File.ReadAllText(content);
            }
            else
            {
                ViewBag.modelContent = "Chua co";
            }

            if (System.IO.File.Exists(inFood))
            {
                ViewBag.modelInfood = System.IO.File.ReadAllText(inFood);
            }
            else
            {
                ViewBag.modelInfood = "Chua co";
            }
            
            ViewBag.modelNutri = modelNutri;
            return View("EditFood",food);
        }
        public IActionResult DeleteDVT(int id)
        {
            _typeFoodDBContext.dVTs.Remove(_typeFoodDBContext.dVTs.FirstOrDefault(p => p.Id == id));
            _typeFoodDBContext.SaveChanges();
            return RedirectToAction("ListDVt");
        }
        public IActionResult Delete(int? id)
        {
            if (_typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == id) != null)
            {
                _typeFoodDBContext.Foods.Remove(_typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == id));
                _typeFoodDBContext.SaveChanges();
            }
            else
            {
                return View("Admin", "Manager");
            }
            return View("Admin", "Manager");
        }
        public IActionResult Deletenutri(int? id)
        {
            if (_typeFoodDBContext.nutributions.FirstOrDefault(p => p.iD == id) != null)
            {
                _typeFoodDBContext.nutributions.Remove(_typeFoodDBContext.nutributions.FirstOrDefault(p => p.iD == id));
                _typeFoodDBContext.SaveChanges();
            }
            else
            {
                return RedirectToAction("ListNutriFood");
            }
            return RedirectToAction("ListNutriFood");
        }
        public IActionResult DeleteLM(int? id)
        {
            if (_typeFoodDBContext.loaimons.FirstOrDefault(p => p.Id == id) != null)
            {
                _typeFoodDBContext.loaimons.Remove(_typeFoodDBContext.loaimons.FirstOrDefault(p => p.Id == id));
                _typeFoodDBContext.SaveChanges();
            }
            else
            {
                return View("Admin","ListLoaimon");
            }
            return RedirectToAction("ListLoaimon");
        }
        [HttpPost]
        public IActionResult EditDVT(DVT dVT)
        {
            if (_typeFoodDBContext.dVTs.Where(p => p.Name == dVT.Name).Count() == 0)
            {
                DVT dVT1 = _typeFoodDBContext.dVTs.FirstOrDefault(p => p.Id == dVT.Id);
                dVT1.Name = dVT.Name;
                _typeFoodDBContext.SaveChanges();

                return RedirectToAction("ListDVt");
            }
            else
            {
                return View("EditDVT", dVT);
            }
        }
        public IActionResult EditDVT(int id)
        {
            DVT dVT = _typeFoodDBContext.dVTs.FirstOrDefault(p => p.Id == id);
            return View(dVT);
        }
        public IActionResult EditContent(int? id)
        {
            FoodContent foodcontent = _typeFoodDBContext.foodContents.FirstOrDefault(p => p.FoodId == id);
            return View(foodcontent);
        }
        [HttpPost]
        public IActionResult EditContent(IFormFile userfileimg, IFormFile userfilerecipe, FoodContent foodContent)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Admin(string? search)
        {

            List<Food> listfoddlaod = new List<Food>();
            if (search == null)
            {
                return View(listFinal);
            }
            else
            {
                listfoddlaod = listFinal.Where(s => s.Name.Contains(search)).ToList();
                ViewBag.datasanm = listFinal[0].Name;
                ViewBag.foodContent = _typeFoodDBContext.foodContents.ToList();
                return View(listfoddlaod);
            }

        }
        public IActionResult Admin()
        {
            List<Food> model = _typeFoodDBContext.Foods.ToList();
            return View(model);
        }
    }
}
