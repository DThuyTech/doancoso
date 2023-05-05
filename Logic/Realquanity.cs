using login.Models;

namespace login.Logic
{
    public class Realquanity
    {
       public Realquanity() { }

       public int quantity(Food food)
        {
            ChangeDVT changeDVT = new ChangeDVT();
            int dvtnew = changeDVT.change(food.DVTid);
            int caloreal = food.Calo * 4 / dvtnew;

            return caloreal;
        }

        public List<float> CaloforBref(float calories)
        {
            float caloSmax;
            float caloTmax;
            float caloTrmax;

            caloSmax = calories +40;
           
            caloTmax = calories +50 ;
           
            caloTrmax = calories +20 ;
            List<float> listCalobref = new List<float>();
            listCalobref.Add(caloSmax);
            listCalobref.Add(caloTrmax);
            listCalobref.Add(caloTmax);
            return listCalobref;
          
        }

        // ham dien du mon an phan bo trong 1 tuan
        public List<DetailFoodNutri> updateDetailList(List<DetailFoodNutri> detailFoodNutrisRaw,List<DetailFoodNutri> detailFoodNutrisFull)
        {
            int countFood = detailFoodNutrisRaw.Count;
            int numberofDetail = countFood  *40/60;
            List<DetailFoodNutri> detailFoodNutrisSup = new List<DetailFoodNutri>();
            for(int i =0;i<numberofDetail-1;i++)
            {
                DetailFoodNutri detailFoodNutri = detailFoodNutrisFull[random(detailFoodNutrisFull.Count)];
                if(detailFoodNutrisRaw.FirstOrDefault(x=>x == detailFoodNutri) == null)
                {
                    detailFoodNutrisRaw.Add(detailFoodNutri);
                }
                else
                {
                    i--;
                }
            }
            return detailFoodNutrisRaw;


        }

        public int random( int max)
        {
            Random random = new Random();
            int idlist = random.Next(0, max-1);
            return idlist;
        }

    }
}
