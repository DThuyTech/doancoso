using login.Models;
using Lucene.Net.Support;

namespace login.Logic
{
    public class Train
    {
        NodeFs node;
        List<TypeFood> typeFoods = new List<TypeFood>();
        List<NodeFs> nodeFs = new List<NodeFs>();
        HashMap<String, String> mCauhoi;
        Logic logic;
        public List<Food> logicFunc(List<TypeFood> typeFoodList, List<string> listAnswer, List<Food> foods)
        {
            nodeFs = new List<NodeFs>();
            node = new NodeFs(0, typeFoodList, "Age");
            nodeFs.Add(node);
            //node tang 1 thieu tnien
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in typeFoodList)
            {
                if (item.Id == 2 || item.Id == 6 || item.Id == 9 || item.Id == 5)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(1, typeFoodList, "Age", "ATI");
            nodeFs.Add(node);


            //tang 1 thanh nien
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in typeFoodList)
            {
                if (item.Id != 2 && item.Id != 6)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(2, typeFoods, "Age", "ATH");
            nodeFs.Add(node);


            nodeFs[0].AddNodeNext(nodeFs[1]);
            nodeFs[0].AddNodeNext(nodeFs[2]);



            //cay thieu nien tuc gian
            //node 3 thieu nien tuc gian
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[1].typeFoods)
            {
                if (item.Id == 2)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(3, typeFoods, "Emotion", "EG");
            nodeFs.Add(node);
            nodeFs[1].AddNodeNext(node);

            //node 4 tre con vui ve
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[1].typeFoods)
            {
                typeFoods.Add(item);
            }
            node = new NodeFs(4, typeFoods, "Emotion", "EV");
            nodeFs.Add(node);
            nodeFs[1].AddNodeNext(node);


            //node 5 tre con buon ba
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[1].typeFoods)
            {
                if (item.Id == 9 || item.Id == 5 || item.Id == 2)
                    typeFoods.Add(item);
            }
            node = new NodeFs(5, typeFoods, "Emotion", "EB");
            nodeFs.Add(node);
            nodeFs[1].AddNodeNext(node);


            //node 6 thanh nien vui ve 
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[2].typeFoods)
            {
                if (item.Id != 9 && item.Id != 6)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(6, typeFoods, "Emotion", "EV");
            nodeFs.Add(node);
            nodeFs[2].AddNodeNext(node);

            //node 7 thanh nien buon 
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[2].typeFoods)
            {
                if (item.Id == 5 || item.Id == 8 || item.Id == 3 || item.Id == 4)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(7, typeFoods, "Emotion", "EB");
            nodeFs.Add(node);
            nodeFs[2].AddNodeNext(node);

            //node 8 thanh nien tuc gian 
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[2].typeFoods)
            {
                if (item.Id == 1 || item.Id == 5 || item.Id == 9 || item.Id == 4 || item.Id == 8)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(8, typeFoods, "Emotion", "EG");
            nodeFs.Add(node);
            nodeFs[2].AddNodeNext(node);

            //node 9 thanh nien tuc gian nam
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[8].typeFoods)
            {
                if (item.Id == 1 || item.Id == 5)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(9, typeFoods, "Emotion", "EG");
            nodeFs.Add(node);
            nodeFs[8].AddNodeNext(node);

            //node 10 thanh nien tuc gian nu
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[8].typeFoods)
            {
                if (item.Id == 5 || item.Id == 8 || item.Id == 9)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(10, typeFoods, "Emotion", "EG");
            nodeFs.Add(node);
            nodeFs[8].AddNodeNext(node);


            //node 11 thanh nien vui ve mau sang
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[6].typeFoods)
            {
                if (item.Id != 8 && item.Id != 7)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(11, typeFoods, "Color", "CS");
            nodeFs.Add(node);
            nodeFs[6].AddNodeNext(node);


            //node 12 thanh nien vui ve mau toi
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[6].typeFoods)
            {
                if (item.Id == 5 || item.Id == 3 || item.Id == 7)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(12, typeFoods, "Color", "CT");
            nodeFs.Add(node);
            nodeFs[6].AddNodeNext(node);

            //node 13 thanh nien buon nam
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[7].typeFoods)
            {
                if (item.Id == 8 || item.Id == 4 || item.Id == 3)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(13, typeFoods, "Sex", "SM");
            nodeFs.Add(node);
            nodeFs[7].AddNodeNext(node);


            //node 14 thanh nien buon  nu
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[7].typeFoods)
            {
                if (item.Id == 5 || item.Id == 3)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(14, typeFoods, "Sex", "SF");
            nodeFs.Add(node);
            nodeFs[7].AddNodeNext(node);

            //node 15 thanh nien buon nam man
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[13].typeFoods)
            {
                if (item.Id == 4 || item.Id == 3)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(15, typeFoods, "Taste", "TM");
            nodeFs.Add(node);
            nodeFs[13].AddNodeNext(node);

            //node 16 thanh nien buon nam ngot
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[13].typeFoods)
            {
                if (item.Id == 8 || item.Id == 4)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(16, typeFoods, "Taste", "TN");
            nodeFs.Add(node);
            nodeFs[13].AddNodeNext(node);



            //node 17 thanh nien buon nam man sang
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[15].typeFoods)
            {
                if (item.Id == 3)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(17, typeFoods, "Color", "CS");
            nodeFs.Add(node);
            nodeFs[15].AddNodeNext(node);


            //node 18 thanh nien buon nam man toi
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[15].typeFoods)
            {
                if (item.Id == 4)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(18, typeFoods, "Color", "CT");
            nodeFs.Add(node);
            nodeFs[15].AddNodeNext(node);


            //node 19 thanh nien tuc gian nu man
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[10].typeFoods)
            {
                if (item.Id == 8 || item.Id == 6)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(19, typeFoods, "Taste", "TM");
            nodeFs.Add(node);
            nodeFs[10].AddNodeNext(node);



            //node 20 thanh nien tuc gian nu ngot
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[10].typeFoods)
            {
                if (item.Id == 5 || item.Id == 8)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(20, typeFoods, "Taste", "TN");
            nodeFs.Add(node);
            nodeFs[10].AddNodeNext(node);

            //node 21 thanh nien tuc gian nu ngot sang
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[20].typeFoods)
            {
                if (item.Id == 5 || item.Id == 8)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(21, typeFoods, "Color", "CS");
            nodeFs.Add(node);
            nodeFs[20].AddNodeNext(node);


            //node 22 thanh nien tuc gian nu ngot toi
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[20].typeFoods)
            {
                if (item.Id == 5)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(22, typeFoods, "Color", "CT");
            nodeFs.Add(node);
            nodeFs[20].AddNodeNext(node);


            //node 23 thanh nien buon  nu sang
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[14].typeFoods)
            {
                if (item.Id == 3)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(23, typeFoods, "Color", "CS");
            nodeFs.Add(node);
            nodeFs[14].AddNodeNext(node);


            //node 24 thanh nien buon  nu toi
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[14].typeFoods)
            {
                if (item.Id == 5)
                {
                    typeFoods.Add(item);
                }
            }
            node = new NodeFs(24, typeFoods, "Color", "CT");
            nodeFs.Add(node);
            nodeFs[14].AddNodeNext(node);


            //node 25 tre con buon ba ngot
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[5].typeFoods)
            {
                if (item.Id == 2 || item.Id == 5)
                    typeFoods.Add(item);
            }
            node = new NodeFs(25, typeFoods, "Taste", "TN");
            nodeFs.Add(node);
            nodeFs[14].AddNodeNext(node);



            //node 26 tre con buon ba man
            typeFoods = new List<TypeFood>();
            foreach (TypeFood item in nodeFs[5].typeFoods)
            {
                if (item.Id == 9 || item.Id == 5)
                    typeFoods.Add(item);
            }
            node = new NodeFs(26, typeFoods, "Taste", "TM");
            nodeFs.Add(node);
            nodeFs[5].AddNodeNext(node);








            // xuly du lieu
            mCauhoi = new HashMap<string, string>();
            List<string> key = new List<string>() { "Age", "Emotion", "Color", "Taste", "Sex" };
            for (int i = 0; i < key.Count(); i++)
            {
                mCauhoi.Add(key[i], listAnswer[i]);

            }

            Logic logic = new Logic(nodeFs, mCauhoi);
            NodeFs nEnd = logic.duyet(nodeFs[0]);

            List<TypeFood> nEdnlisttype = new List<TypeFood>();
            nEdnlisttype = randomType(nEnd);
            return getListFood(nEdnlisttype, foods);

        }



        //public NodeFs duyet(NodeFs node, HashMap<String, String> mcauhoi)
        //{
        //    if (node.ListNodenext.Count == 0)
        //    {
        //        return node;
        //    }
        //    else
        //    {
        //        for(int i=0;i<node.ListNodenext.Count;i++)
        //        {

        //        }
        //    }
        //}
        public List<TypeFood> randomType(NodeFs nodeFs)
        {
            List<TypeFood> typeFoods = new List<TypeFood>();
            TypeFood typeFood;

            int min = 0;
            int max = nodeFs.typeFoods.Count - 1;
            Random random = new Random();
            int randomId = random.Next(min, max);
            typeFoods.Add(nodeFs.typeFoods[randomId]);
            randomId = random.Next(min, max);
            typeFoods.Add(nodeFs.typeFoods[randomId]);
            return typeFoods;

        }

        public List<Food> getListFood(List<TypeFood> typeFoods, List<Food> mlistraw)
        {
            List<Food> foods = new List<Food>();
            Food food;
            List<Food> mListfoodraw = new List<Food>();
            food = new Food();
            int id_typefood;
            for (int i = 0; i < typeFoods.Count; i++)
            {
                id_typefood = typeFoods[i].Id;
                mListfoodraw = new List<Food>();
                foreach (Food food1 in mlistraw)
                {
                    if (food1.TypeFood == typeFoods[i])
                    {
                        mListfoodraw.Add(food1);
                    }
                }
            }
            Random random = new Random();
            int randomFood = random.Next(0, mListfoodraw.Count - 1);

            for (int i = 0; i < 3; i++)
            {
                randomFood = random.Next(0, mListfoodraw.Count - 1);
                foods.Add(mListfoodraw[randomFood]);
            }
            return foods;
        }
    }
}
