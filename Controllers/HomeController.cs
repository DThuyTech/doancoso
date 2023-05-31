using login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using login.Data;
using login.Logic;
using login.Viewmodels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Google.Cloud.Firestore;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore.V1;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace login.Controllers
{

    public class HomeController : Controller
    {

        //connect to firebase 

        private string serviceAccountKeyPath;


        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "FmqH3aXHqn9M9SdgtXFT1uaTxqqeB0HJL761iKHb",
            BasePath = "https://foodcommentuser-default-rtdb.firebaseio.com",
        };
        IFirebaseClient client;

        IList<Food> listFinal = new List<Food>();
        List<FoodContent> foodContents = new List<FoodContent>();

        rbDBContext _typeFoodDBContext;

        private readonly ILogger<HomeController> _logger;
       

        public HomeController(rbDBContext typeFoodDBContext, ILogger<HomeController> logger)
        {
            _typeFoodDBContext = typeFoodDBContext;
            listFinal = _typeFoodDBContext.Foods.ToList();
            foodContents = _typeFoodDBContext.foodContents.ToList() ;
            serviceAccountKeyPath = "C:\\Users\\ASUS\\source\\Repos3\\Food\\foodrecommands-firebase-adminsdk-jtgwr-af70336f68.json";
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
        


        public IActionResult check(int id)
        {
            return View("Index");
        }

        //action show thong tin cua khach hang
        [Authorize]
        [HttpGet]
        public IActionResult accountInfor(int? id)
        {
            int day=90;
            if (id != null)
            {
                day = (int)id;
            }
            else
            {
                day = 90;
            }
            int dayRe;
            Dictionary<int, List<Diet>> Likstfoodfor7day = new Dictionary<int, List<Diet>>();
            string idUser = User.Identity.Name;
            List<Diet> diets1user = new List<Diet>();
          
      
            ViewBag.day = day;          
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
               
            }
            else
            {
                user = _typeFoodDBContext.userinfors.FirstOrDefault(p => p.Id == User.Identity.Name);
                ViewBag.Data = user;
                ViewBag.BMI = chisoIbm(user.heights, user.weigh);
               
            }
            //    InforUser infor = new InforUser(_typeFoodDBContext.inforUsers.FirstOrDefault(p=>p.username == user.IdentityUser.UserName));
            //ViewBag.Infor = infor;

            if (_typeFoodDBContext.diets.Where(x => x.IdUser == idUser).Count() >= 42)
            {

                ViewBag.monphu = getmnonphu(idUser);


                diets1user = _typeFoodDBContext.diets.OrderByDescending(s => s.IdDiet).ToList();

                diets1user = diets1user.Where(x => x.IdUser == idUser).ToList();

                dayRe = 1;


                if (diets1user.Count != 0)
                {
                    for (int i = diets1user.Count - 1; i >= diets1user.Count - 21; i -= 3)
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
                ViewBag.Datacalo = calobrefUserneed(idUser);
            }
            ViewBag.dvts = _typeFoodDBContext.dVTs.ToList();


            return View();



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
        public List<Dictionary< int,Food>> getmnonphu(string username)
        {
            List<Diet> diets = _typeFoodDBContext.diets.Where(s => s.IdUser == username).ToList();
            List<Dictionary<int, Food>> listFoodphu = new List<Dictionary<int, Food>>();
            for( int  i = 0; i < 7; i++)
            {
                List<int> listrandom = new List<int>();
                Dictionary<int, Food> listfoofoneday = new Dictionary<int, Food>();
                for(int j = 0; j < 3; j++)
                {
                    Random random = new Random();
                    int randomid = random.Next(0, diets.Count - 1);
                    Food food = _typeFoodDBContext.Foods.FirstOrDefault(x => x.Id == diets[randomid].IdFood);

                    if( listrandom.Where(x=>x == randomid).Count() >0)
                    {
                        j--;
                    }
                    else
                    {
                        if(food.LoaimonId == 1)
                        {
                            j--;
                        }
                        else
                        {
                            listfoofoneday[j] = food;
                        }
                    }
                   
                
                }
                listFoodphu.Add(listfoofoneday);
                
            }
            return listFoodphu;
        }
        public void monphu(int idnutrimonchinh)
        {
            List<Dictionary<int, Food>> listmonphu = new List<Dictionary<int, Food>>();
            List<DetailFoodNutri> monphuraw = _typeFoodDBContext.detailFoodNutris.Where(x => x.NutributionId != idnutrimonchinh).ToList();
            List<Food> foodphus = new    List<Food>();
            foreach (DetailFoodNutri item in monphuraw)
            {
                Food food = _typeFoodDBContext.Foods.FirstOrDefault(p=>p.Id ==item.FoodId);

                if(food.LoaimonId != 1)
                {
                    foodphus.Add(food);
                }
            }

            for(int i = 0; i < 7; i++)
            {
                List<Food> foodforoneday = new List<Food>();
                List<int> listrandom = new List<int>();
                for (int j = 0; j < 3; j++)
                {
                    Random random = new Random();

                    int randomid = random.Next(0, foodphus.Count - 1);
                    if (listrandom.Where(x => x == randomid).Count() != 0)
                    {

                        j--;
                    }
                    else
                    {
                        listrandom.Add(randomid);
                        Food food = foodphus[randomid];

                        foodforoneday.Add(food);
                    }
                }
                Dictionary<int, Food> foodphu = new Dictionary<int, Food>();
                foreach (Food item in foodforoneday)
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

        }

        public List<Dictionary<int,float>> UpdateCaloneed (List<Dictionary<int,float>> calorow,float caloforoneday)
        {
            List<Dictionary<int, float>> caloupdate = new List<Dictionary<int, float>>();
            Dictionary<int, float> caloneedBref = new Dictionary<int, float>();
            caloneedBref[1] = (caloforoneday * 40 /(float) 100) * 70/(float)100;
            caloneedBref[2] = (caloforoneday * 40 / (float)100) * 30 / (float)100;

            caloupdate.Add(caloneedBref);

            caloneedBref[1] = (caloforoneday * 30 / (float)100) * 80 / (float)100;
            caloneedBref[2] = (caloforoneday * 30 / (float)100) * 20 / (float)100;

            caloupdate.Add(caloneedBref);

            caloneedBref[1] = (caloforoneday * 30 / (float)100) * 75 / (float)100;
            caloneedBref[2] = (caloforoneday * 30 / (float)100) * 25 / (float)100;

            caloupdate.Add(caloneedBref);
            return caloupdate;

        }
        [HttpPost]
        public IActionResult RecommandFoodforweek(ValueAnswerViewModel viewModel)
        {
            string idUser = User.Identity.Name;
            UserInfor userInfor = _typeFoodDBContext.userinfors.FirstOrDefault(p => p.Id == idUser);
            BMR bMRUser = new BMR(userInfor.sexs, userInfor.heights, userInfor.weigh, userInfor.ages, userInfor.Mucdich,userInfor.Mucdich);
            List<Dictionary<int, float>> caloneed = new List<Dictionary<int, float>>();
            float caloneedForoneday = bMRUser.TDEE();
            caloneed = UpdateCaloneed(caloneed,caloneedForoneday);



           List<Diet> diet = new List<Diet>();
            diet = _typeFoodDBContext.diets.Where(x => x.IdUser == User.Identity.Name).ToList();
            foreach(Diet item in diet)
            {
                _typeFoodDBContext.diets.Remove(item);
            }
            _typeFoodDBContext.SaveChanges();

            //updatemuc tieu
            String username = User.Identity.Name;
            UserInfor user = _typeFoodDBContext.userinfors.FirstOrDefault(x => x.Id == username);
            user.Mucdich = int.Parse(viewModel.valueAnswerReach);
            user.Activ = int.Parse(viewModel.valueAnswerActiv);
            //chinh sau hoat dong cua user


            if (int.Parse(viewModel.valueAnswerActiv) <= 1)
            {
                viewModel.valueAnswerActiv = "1";
            }
            else
            {
                viewModel.valueAnswerActiv = "2";
            }

            
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


            // ham tinh toan luong doa dan
            Realquanity realquanity1 = new Realquanity();
            detailFoodNutris = realquanity1.updateDetailList(detailFoodNutris, _typeFoodDBContext.detailFoodNutris.ToList());
            for (int i = 0; i < 7; i++)
            {
                foodforOneday = new List<Food>();
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
                        Food food = _typeFoodDBContext.Foods.FirstOrDefault(p => p.Id == detailFoodNutris[randomid].FoodId);
                        Realquanity realquanity = new Realquanity();
                        int rel = realquanity.quantity(food);
                        float rel2 = realquanity.CaloforBref(caloneed[j][1])[j];

                        if (realquanity.quantity(food) < realquanity.CaloforBref(caloneed[j][1])[j]&&food.LoaimonId==1)
                        {
                            foodforOneday.Add(food);
                        }
                        else
                        {
                            j--;
                        }
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
            monphu(nodeFinal.Label[0]);
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
                

                    listFoodfinal[i].Add(_typeFoodDBContext.Foods.FirstOrDefault(x => x.Id == sd[0].IdFood));
                    listFoodfinal[i].Add(_typeFoodDBContext.Foods.FirstOrDefault(x => x.Id == sd[1].IdFood));
                    listFoodfinal[i].Add(_typeFoodDBContext.Foods.FirstOrDefault(x => x.Id == sd[2].IdFood));
               

               

            }
            return listFoodfinal;
        }


        //lay calo tung buoi 1 nguoiwf
        public List<float> calobrefUserneed(string username)
        {
            UserInfor userInfor = _typeFoodDBContext.userinfors.FirstOrDefault(x => x.Id == username);
            BMR bMR = new BMR(userInfor.sexs, (float)userInfor.heights,(float)userInfor.weigh, userInfor.ages,userInfor.Activ,userInfor.Mucdich);
            float calonneed = bMR.TDEE();
            List<float> calotungbuoi = new List<float>();
            calotungbuoi.Add(calonneed * 30 / 100);
            calotungbuoi.Add(calonneed * 40 / 100);
            calotungbuoi.Add(calonneed * 30 / 100);

            return calotungbuoi;
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

            ViewBag.Datacalo = calobrefUserneed(User.Identity.Name);
            ViewBag.dvts = _typeFoodDBContext.dVTs.ToList();



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
            var answerDataReach = ValueAnswers.getAllReach();
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
            model.valueAnswerSelectdListReach = new List<SelectListItem>();

            foreach (var valueAnswer in answerDataReach)
            {
                model.valueAnswerSelectdListReach.Add(new SelectListItem { Text = valueAnswer.Description, Value = valueAnswer.Value.ToString() });
            }


            return View(model);

          



        }


        public void SaveCmt(CommentUser comment)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                var data = comment;
                PushResponse response = client.Push("cmtUser/", data);
                data.Id = response.Result.name;
                SetResponse setResponse = client.Set("cmtUser/" + data.Id, data);


                if (setResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ModelState.AddModelError(string.Empty, "Added Succesfully");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong!!");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

           

        


        }
      
        public  IActionResult SendCmttodata(string cmtData,int idFood)
        {
            int id = idFood;
            string asd = cmtData;

            CommentUser comment = new CommentUser();
            comment.idUser = User.Identity.Name;
            comment.idFood = idFood.ToString(); 
            comment.content = cmtData;
             SaveCmt(comment);
            
            return RedirectToAction("Food",new {id = idFood});

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


       

        [Route("Home/Food/{id:int}")]
        public async Task<IActionResult> DetailAsync(int? id)
        {




            // uplaod firebase
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("cmtUser");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<CommentUser>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<CommentUser>(((JProperty)item).Value.ToString()));
                }
            }

            ViewBag.cmtUser = list;
          
            //Dictionary<string, object> newBookData = new Dictionary<string, object>
            //    {
            //        { "Title", "Sample Book" },
            //        { "Author", "John Doe" },
            //        { "ISBN", "1234567890" }
            //    };
            //await newBookRef.SetAsync(newBookData);


            List<DetailFoodNutri> detailFoodNutris = _typeFoodDBContext.detailFoodNutris.Where(x => x.FoodId == id).ToList();
                List<string> nutriText = new List<string>();
                foreach(DetailFoodNutri item in detailFoodNutris)
                {
                    nutriText.Add(_typeFoodDBContext.nutributions.FirstOrDefault(x => x.iD == item.NutributionId).name);
                }
                if (nutriText.Count > 0)
                {
                    ViewBag.ListofnutriFood = nutriText;
                }
                else
                {
                    nutriText.Add("Chua cap nhat");
                    ViewBag.ListofnutriFood = nutriText;
                }
                //FoodContent foodContent = new FoodContent();
                string[] text;
                text = null;
                String imgeFood = " /Food/ImageFood/"+id.ToString()+".jpg ";



            //lat noi dung nguyen liau
            text = new string[1000];
    
            string link = "../Food/wwwroot/Food/InFood/"+id.ToString()+".txt";
           if(System.IO.File.Exists(link)!=null) {
                text = System.IO.File.ReadAllLines(link);
                ViewBag.InFood = text;
            }
            else
            {
                text = System.IO.File.ReadAllLines("../Food/wwwroot/Food/InFood/0.txt");
                ViewBag.InFood = text;
            }


            text = new string[1000];
             link = "../Food/wwwroot/Food/RecipeFood/" + id.ToString() + ".txt";
            if (System.IO.File.Exists(link) != null)
            {
                text = System.IO.File.ReadAllLines(link);
                ViewBag.RecipeFood = text;
            }
            else
            {
                text = System.IO.File.ReadAllLines("../Food/wwwroot/Food/InFood/0.txt");
                ViewBag.RecipeFood = text;
            }

            text = new string[1000];
            link = "../Food/wwwroot/Food/ContentFood/" + id.ToString() + ".txt";
            if (System.IO.File.Exists(link) != null)
            {
                text = System.IO.File.ReadAllLines(link);
                ViewBag.ContentFood = text;
            }
            else
            {
                text = System.IO.File.ReadAllLines("../Food/wwwroot/Food/InFood/0.txt");
                ViewBag.ContentFood = text;
            }

            //if (foodContents.FirstOrDefault(p => p.FoodId == id) == null)
            //{
            //    text = new string[5];
            //    text[0].Insert(0, "xin loi vi chuc toi chua co cong thuc mon nay");
            //    text[1].Insert(0, "quy khach co the chon lai mon khach neu that su can cong thuc");
            //}
            //else
            //{

            //    foodContent = foodContents.FirstOrDefault(p => p.FoodId == id);
            //    text = new string[1000];
            //    string link = "../Webrecommandfood3/wwwroot/Food/recipefood/recipe39.txt";
            //    //C: \Users\ASUS\source\repos\Webrecommandfood3\wwwroot\Food\recipefood\recipe21.txt
            //    //C: \Users\ASUS\source\repos\Webrecommandfood3\wwwroot\Food\recipefood\recipe39.txt
            //    text = System.IO.File.ReadAllLines(foodContent.Recipes);



            ViewBag.ImgLink = imgeFood;
                return View(listFinal.FirstOrDefault(p => p.Id == id));
            
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       

    }
}