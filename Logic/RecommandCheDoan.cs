using login.Models;
using login.Data;
using Lucene.Net.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace login.Logic
{
    public class RecommandCheDoan
    {
        public List<Food> foods { get; set; }
        public UserInfor Useruser { get; set; }
        public List<Diet> diets { get; set; }
        public List<Diet> ReturnDiets()
        {
            BMR bMR = new BMR(Useruser.sexs, Useruser.heights, Useruser.weigh, Useruser.ages, Useruser.Activ,Useruser.Mucdich) ;
            float caloriesneed = bMR.clorisneed();

            float calobuoisang = caloriesneed * 30 / 100;
            float calobuoitrua =caloriesneed* 50 / 100;
            float calobuoitoi = caloriesneed * 20 / 100;


            List<Diet> diets = new List<Diet>();
            Food food1 = new Food();
            int dem = 0;
            string buoi = "";
            float caloneedmax = 0;
            float caloneedmin = 0;
            for(int i = 0; i <= 2; i++)
            {
            if (i == 0)
               {
                    caloneedmax = calobuoisang * 110 / 100;
                    caloneedmin = calobuoisang * 80 / 100;
                }
                else if (i == 1)
                {
                    caloneedmax = calobuoitrua * 120 / 100;
                    caloneedmin = calobuoitrua * 90 / 100;
                }
                else
                {
                    caloneedmax = calobuoitoi * 110 / 100;
                    caloneedmin = calobuoitoi * 80 / 100;
                }
                foreach (Food food in foods)
                {
                    if (food.Calo > caloneedmin && food.Calo < caloneedmax)
                    {
                        Diet diet = new Diet();
                        diet.IdFood = food.Id;
                        diet.Buoi = 1;
                        diet.Day = 20;
                        diet.IdUser = Useruser.Id;

                        diets.Add(diet);
                        break;
                    } 

                }
            }
            return diets;
        }
    }
}
