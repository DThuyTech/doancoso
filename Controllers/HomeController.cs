using login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Lucene.Net.Search;
using System.Security;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using login.Data;
using login.Logic;
using login.Viewmodels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Common;
using Newtonsoft.Json.Bson;
using Lucene.Net.Support;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace login.Controllers
{
    
    public class HomeController : Controller
    {

        IList<Food> listFinal = new List<Food>();
        List<FoodContent> foodContents = new List<FoodContent>();

        rbDBContext _typeFoodDBContext;

        private readonly ILogger<HomeController> _logger;
       

        public HomeController(rbDBContext typeFoodDBContext, ILogger<HomeController> logger)
        {
            _typeFoodDBContext = typeFoodDBContext;
            listFinal = _typeFoodDBContext.Foods.ToList();
            foodContents = _typeFoodDBContext.foodContents.ToList() ;
          
            _logger = logger;
          
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

        //action show thong tin cua khach hang
        [Authorize]
        [HttpGet]
        public IActionResult accountInfor(int? day)
        {
            if(day == null) {
                day = 90;
            }
            int dayRe;
            Dictionary<int, List<Diet>> Likstfoodfor7day = new Dictionary<int, List<Diet>>();
            string idUser = User.Identity.Name;
            List<Diet> diets1user = new List<Diet>();
            if (_typeFoodDBContext.diets.Where(x => x.IdUser == idUser).Count() > 21)
            {



                diets1user = _typeFoodDBContext.diets.OrderByDescending(s => s.IdDiet).ToList();

                diets1user = diets1user.Where(x => x.IdUser == idUser).ToList();

                dayRe = 1;
                day = 1;

                if (diets1user.Count != 0)
                {
                    for (int i = diets1user.Count - 1; i >= diets1user.Count - 1 - 21; i -= 3)
                    {

                        List<Diet> dietsoneDay = new List<Diet>();
                        dietsoneDay.Add(diets1user[i]);
                        dietsoneDay.Add(diets1user[i - 1]);
                        dietsoneDay.Add(diets1user[i - 2]);
                        Likstfoodfor7day.Add((int)dayRe, dietsoneDay);
                        dayRe++;
                    }
                }
                ViewBag.foodFinal = foodRe7day(Likstfoodfor7day);
            }
            
            ViewBag.day = day;
            //User user = new User("khach hang", _typeFoodDBContext.users.FirstOrDefault(p => p.UserName == User.Identity.Name));
            UserInfor user;

            ViewBag.id = User.Identity.Name;

            if (_typeFoodDBContext.userinfors.FirstOrDefault(p => p.Id == User.Identity.Name) is null)
            {
                user = new UserInfor();
                user.Id = User.Identity.Name;
                user.role = "Guest";
                _typeFoodDBContext.userinfors.Add(user);
                _typeFoodDBContext.SaveChanges();
                ViewBag.BMI = chisoIbm(user.heights, user.weigh);
                return View();
            }
            else
            {
                user = _typeFoodDBContext.userinfors.FirstOrDefault(p => p.Id == User.Identity.Name);
                ViewBag.Data = user;
                ViewBag.BMI = chisoIbm(user.heights, user.weigh);
                return View();
            }
            //    InforUser infor = new InforUser(_typeFoodDBContext.inforUsers.FirstOrDefault(p=>p.username == user.IdentityUser.UserName));
            //ViewBag.Infor = infor;






        }

        public Node cctchID3(Node node, Dictionary<string, int> answer)
        {
            if (node.Attribute == null)
            {
                return node;
                
            }
            else
            {
                int valueAns = answer[node.Attribute];
                Node nodechil = node.Children.FirstOrDefault(x => x.Value == valueAns);
                return cctchID3(nodechil, answer);
            }
        }
        public Node RecommandNodeFood(Dictionary<string,int> answer)
        {

            List<DbTrain> dbTrain = new List<DbTrain>();
            dbTrain = _typeFoodDBContext.trains.ToList();
            Train2 train = new Train2(dbTrain);
            Node root = new Node();
            train.data = dbTrain;
            root = train.BuidDecisionTree(train.AddData());
           

            train.data = dbTrain;


            return cctchID3(root, answer);
            
        }


        [HttpPost]
        public IActionResult RecommandFoodforweek(ValueAnswerViewModel viewModel)
        {

           List<Diet> diet = new List<Diet>();
            diet = _typeFoodDBContext.diets.Where(x => x.IdUser == User.Identity.Name).ToList();
            foreach(Diet item in diet)
            {
                _typeFoodDBContext.diets.Remove(item);
            }
            _typeFoodDBContext.SaveChanges();
            Dictionary<string, int> answrer = new Dictionary<string, int>()
                {
                    {"Gender",int.Parse(viewModel.valueAnswerSex)},
                    {"Age",int.Parse(viewModel.valueAnswerAge) },
                    {"Activ",int.Parse(viewModel.valueAnswerActiv) },
                    {"Color",int.Parse(viewModel.valueAnswerCol) },
                    {"KeyTaste",int.Parse(viewModel.valueAnswerTas) },
                    {"KeyEmotion",int.Parse(viewModel.valueAnswerEmo) }
                };
            Node nodeFinal = RecommandNodeFood(answrer);

            Diet dietOneday = new Diet();
            List<Diet> diets = new List<Diet>();


            List<Food> foodforOneday = new List<Food>();

            List<DetailFoodNutri> detailFoodNutris = new List<DetailFoodNutri>();
            detailFoodNutris = _typeFoodDBContext.detailFoodNutris.Where(x => x.NutributionId == nodeFinal.Label[0]).ToList();
            

            

            for (int i = 0; i < 7; i++)
            {
                List<int> listrandom = new List<int>();
                for (int j = 0; j < 3; j++)
                {
                   
                    Random random = new Random();
                    
                    int randomid = random.Next(0, detailFoodNutris.Count - 1);
                    if (listrandom.Where(x=>x==randomid).Count() !=0)
                    {

                        j--;
                    }
                    else
                    {
                        listrandom.Add(randomid);
                        foodforOneday.Add(_typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == detailFoodNutris[randomid].FoodId));
                    }


                }
                foreach (Food item in foodforOneday)
                {

                    Diet bref1 = new Diet();
                    bref1.IdUser = User.Identity.Name;
                    bref1.IdFood = item.Id;
                    bref1.Day = DateTime.Now.Day + i;
                    //bref1.Buoi = item.Bref;

                    _typeFoodDBContext.diets.Add(bref1);
                    _typeFoodDBContext.SaveChanges();

                }
            }

            return Redirect("accountInfor");
            

        }
        //get list food forrecommand 
        public Dictionary<int,List<Food>> foodRe7day(Dictionary<int,List<Diet>> listdietuser)
        {
            List<Food> foodlist = _typeFoodDBContext.Foods.ToList(); 
            Dictionary<int, List<Food>> listFoodfinal = new Dictionary<int, List<Food>>();
            for(int i = 1;i <= 7; i++)
            {
                listFoodfinal[i] = new List<Food>();
                List<Diet> sd = listdietuser[i];
                

                    listFoodfinal[i].Add(_typeFoodDBContext.Foods.FirstOrDefault(x => x.Id == sd[0].IdFood ));
                    listFoodfinal[i].Add(_typeFoodDBContext.Foods.FirstOrDefault(x => x.Id == sd[1].IdFood));
                    listFoodfinal[i].Add(_typeFoodDBContext.Foods.FirstOrDefault(x => x.Id == sd[2].IdFood));
               

               

            }
            return listFoodfinal;
        }
        [Authorize]
        [HttpPost]
        public IActionResult accountInfor(string height, string weight, string age, string sex,int? day)
        {
            if (day == null)
            {
                day = 90;
            }
            int dayRe = 0;
            Dictionary<int, List<Diet>> Likstfoodfor7day = new Dictionary<int, List<Diet>>();
            string idUser = User.Identity.Name;
            List<Diet> diets1user = new List<Diet>();
            if (_typeFoodDBContext.diets.Where(x=>x.IdUser == idUser).Count() > 21)
            {

              

                diets1user = _typeFoodDBContext.diets.OrderByDescending(s => s.Day).ToList();
              
                diets1user = diets1user.Where(x => x.IdUser == idUser).ToList();

                dayRe = 1;
                day = 1;
                if (diets1user.Count != 0)
                {
                    for (int i = diets1user.Count - 1; i >= diets1user.Count - 1 - 21; i -= 3)
                    {
                        
                        List<Diet> dietsoneDay = new List<Diet>();
                        dietsoneDay.Add(diets1user[i]);
                        dietsoneDay.Add(diets1user[i - 1]);
                        dietsoneDay.Add(diets1user[i - 2]);
                        Likstfoodfor7day.Add((int)dayRe, dietsoneDay);
                        dayRe++;
                    }
                }
                ViewBag.foodFinal = foodRe7day(Likstfoodfor7day);
            }
            
            ViewBag.day = day;
            int asdasd = Likstfoodfor7day.Count;

            var userinfor = _typeFoodDBContext.userinfors.FirstOrDefault(p => p.Id == User.Identity.Name);
            userinfor.weigh = int.Parse(weight);
            userinfor.ages = int.Parse(age);
            userinfor.heights = int.Parse(height);
            userinfor.sexs = int.Parse(sex);
            _typeFoodDBContext.SaveChanges();



            ViewBag.Data = userinfor;



           
        
            ViewBag.BMI = chisoIbm(userinfor.heights, userinfor.weigh);



            //recommand food
           
          


            return View("accountInfor", "Home");
        }
        

        public float chisoIbm(int heigh, int weight)
        {
            float csIbm;
            float heighM = (float)heigh * 0.01f;
            return csIbm = (float)weight / ((float)heighM * (float)heighM);
        }




        public IActionResult Contact()
        {
            return View();
        }

        //[Authorize]
        public IActionResult Recipes()
        {
            List<FoodContent> foodContent = new List<FoodContent>();
            FoodViewModel foodViewModel = new FoodViewModel();
            Random random = new Random();
            foodViewModel.foods = new List<Food>();
            List<Food> foods = listFinal.ToList();
            int randomID;
            for (int i = 0; i < 28; i++)
            {
                randomID = random.Next(0, foods.Count - 1);
                foodViewModel.foods.Add(foods[randomID]);
            }
            foodContent = _typeFoodDBContext.foodContents.ToList();
            ViewBag.foodContent = foodContent;

            return View(foodViewModel.foods);
        }
        [HttpPost]
        //[Authorize]
        public IActionResult Recipes(String search)
        {
            List<Food> listfoddlaod = new List<Food>();

            listfoddlaod = listFinal.Where(s => s.Name.Contains(search)).ToList();
            ViewBag.datasanm = listFinal[0].Name;
            ViewBag.foodContent = _typeFoodDBContext.foodContents.ToList();
            return View(listfoddlaod);
        }
        public IActionResult List()
        {
            return View();
        }



        public IActionResult Recommand()
        {

            //List<string>[] arrayListinfo;
            //List<string> gioitinh = new List<string> { "Nam", "Nu" };
            //List<string> dotuoi = new List<string> { "Tren 16", "Duoi 16" };
            //List<string> mausac = new List<string> { "Sang", "Toi" };
            //List<string> camxuc = new List<string> { "Buon", "Vui","Tuc gian" };



            //arrayListinfo =  new List<string>[] { gioitinh,dotuoi,mausac,camxuc };
            //var getList = new List<string>[] { };


            var answerDataTaste = ValueAnswers.getAllTaste();
            var answerDataEmotion = ValueAnswers.getAllEmotion();
            var answerDataColor = ValueAnswers.getAllColor();
            var answerDataAge = ValueAnswers.getAllAge();
            var answerDataSex = ValueAnswers.getAllSex();
            var answerDataActiv = ValueAnswers.getAllActiv();
            var model = new ValueAnswerViewModel();
            model.valuaAnswerSelectdListTaste = new List<SelectListItem>();

            foreach (var valueAnswer in answerDataTaste)
            {
                model.valuaAnswerSelectdListTaste.Add(new SelectListItem { Text = valueAnswer.Description, Value = valueAnswer.Value.ToString() });
            }
            model.valuaAnswerSelectdListSex = new List<SelectListItem>();

            foreach (var valueAnswer in answerDataSex)
            {
                model.valuaAnswerSelectdListSex.Add(new SelectListItem { Text = valueAnswer.Description, Value = valueAnswer.Value.ToString() });
            }

            model.valuaAnswerSelectdListAge = new List<SelectListItem>();

            foreach (var valueAnswer in answerDataAge)
            {
                model.valuaAnswerSelectdListAge.Add(new SelectListItem { Text = valueAnswer.Description, Value = valueAnswer.Value.ToString() });
            }
            model.valuaAnswerSelectdListEmotion = new List<SelectListItem>();

            foreach (var valueAnswer in answerDataEmotion)
            {
                model.valuaAnswerSelectdListEmotion.Add(new SelectListItem { Text = valueAnswer.Description, Value = valueAnswer.Value.ToString() });
            }
            model.valuaAnswerSelectdListColor = new List<SelectListItem>();

            foreach (var valueAnswer in answerDataColor)
            {
                model.valuaAnswerSelectdListColor.Add(new SelectListItem { Text = valueAnswer.Description, Value = valueAnswer.Value.ToString() });
            }




            model.valueAnswerSelectdListActiv = new List<SelectListItem>();

            foreach (var valueAnswer in answerDataActiv)
            {
                model.valueAnswerSelectdListActiv.Add(new SelectListItem { Text = valueAnswer.Description, Value = valueAnswer.Value.ToString() });
            }


            return View(model);

          



        }

        [HttpPost]
        public IActionResult Recommand(ValueAnswerViewModel model)
        {
            List<Food> foods = new List<Food>();
            List<TypeFood> typeFoods = new List<TypeFood>();
            foods = _typeFoodDBContext.Foods.ToList<Food>();
            typeFoods = _typeFoodDBContext.Types.ToList<TypeFood>();
            List<String> answer = new List<String>();
            //string age = model.valueAnswerAge.ToString();
            answer.Add(model.valueAnswerAge.ToString());
            answer.Add(model.valueAnswerEmo.ToString());
            answer.Add(model.valueAnswerCol.ToString());
            answer.Add(model.valueAnswerTas.ToString());
            answer.Add(model.valueAnswerSex.ToString());

            //answer.Add("ATI");
            //answer.Add("EB");
            //answer.Add("")
            //answer.Add("TM");
            //answer.Add("SF");

            Train train = new Train();
            List<Food> finalre = train.logicFunc(typeFoods, answer, foods);



            TempData["idfood1"] = finalre[0].Id;
            TempData["idfood2"] = finalre[1].Id;
            TempData["idfood3"] = finalre[2].Id;

            return RedirectToAction("Detail");
        }




        [Route("Food/{id:int}")]
        public IActionResult Detail(int? id)
        {


            if (id == 9999)
            {
                FoodViewModel modelfoods = new FoodViewModel();
                modelfoods.foods = new List<Food>();
                modelfoods.foods.Add(_typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == (int)TempData["idfood1"]));
                modelfoods.foods.Add(_typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == (int)TempData["idfood2"]));
                modelfoods.foods.Add(_typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == (int)TempData["idfood3"]));
                FoodContent foodContent = foodContents.FirstOrDefault(p => p.FoodId == modelfoods.foods[0].Id);

                string[] text = System.IO.File.ReadAllLines(foodContent.Recipes);
                ViewBag.data = text;
                ViewBag.foodcontent = foodContent;

                return View(modelfoods.foods[0]);
            }
            else
            // C: \Users\ASUS\source\repos\WebrecommandFood\WebrecommandFood\wwwroot\Food\recipefood\recipe1.txt
            {
                FoodContent foodContent = new FoodContent();
                string[] text;
                text = null;
                if (foodContents.FirstOrDefault(p => p.FoodId == id) == null)
                {
                    text = new string[5];
                    text[0].Insert(0, "xin loi vi chuc toi chua co cong thuc mon nay");
                    text[1].Insert(0, "quy khach co the chon lai mon khach neu that su can cong thuc");
                }
                else
                {

                    foodContent = foodContents.FirstOrDefault(p => p.FoodId == id);
                    text = new string[1000];
                    string link = "../Webrecommandfood3/wwwroot/Food/recipefood/recipe39.txt";
                    //C: \Users\ASUS\source\repos\Webrecommandfood3\wwwroot\Food\recipefood\recipe21.txt
                    //C: \Users\ASUS\source\repos\Webrecommandfood3\wwwroot\Food\recipefood\recipe39.txt
                    text = System.IO.File.ReadAllLines(foodContent.Recipes);

                }


                ViewBag.Data = text;
                ViewBag.foodcontent = foodContent;
                return View(listFinal.FirstOrDefault(p => p.Id == id));
            }
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       

    }
}