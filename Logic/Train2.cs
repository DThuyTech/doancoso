using login.Models;
using login.Logic;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace login.Logic
{
    public class Train2
    {
        public List<DbTrain> data { get; set; }
        public List<String> Attribute = new List<string>() { "Gender", "Age", "Activ", "Color", "KeyEmotion", "KeyTaste" };

        public Train2(List<DbTrain> data)
        {
            this.data = data;
        }

        public List<Dictionary<string, int>> AddData()
        {
            List<Dictionary<string, int>> Datatrain = new List<Dictionary<string, int>>();
            List<DbTrain> newdata = new List<DbTrain>();
            newdata = data.Where(x => x.KeyTaste == 2).ToList();
          
            foreach (DbTrain item in newdata)
            {
                Dictionary<string, int> itemData = new Dictionary<string, int>();
                itemData["Gender"] = item.Gender;
                itemData["Age"] = item.Age;
                itemData["Activ"] = item.Activ;
                itemData["Color"] = item.Color;
                itemData["KeyEmotion"] = item.KeyEmotion;
                itemData["KeyTaste"] = item.KeyTaste;
                itemData["NutributionId"] = item.NutributionId;

                Datatrain.Add(itemData);
            }

            return Datatrain;
        }
        public Node TrainFinish(List<Dictionary<string, int>> data, List<string> Attributes)
        {
            return BuidDecisionTree(data, Attributes);
        }
         static  Node BuidDecisionTree(List<Dictionary<string, int>> data,List<string> Attributes)
        {
            Node root = new Node();
            if (data.Select(p => p["NutributionId"]).Distinct().Count() == 1)
            {
                root.Label = data[0]["NutributionId"];
                return root;
            }

            if (Attributes.Count == 0)
            {
                root.Label = data.GroupBy(p => p["NutributionId"]).OrderByDescending(p=>p.Count()).First().Key;
                return root;
            }

            string MaxAtriibute = "";
            double MaxinformationGain = 0;
            foreach(string atributetest in Attributes)
            {
                double informationGaintest = informationGain(data,atributetest);
                if(informationGaintest > MaxinformationGain)
                {
                    MaxinformationGain = informationGaintest;
                    MaxAtriibute= atributetest;
                }
            }
            
            root.Attribute = MaxAtriibute;

            Attributes.Remove(MaxAtriibute);
            if(MaxAtriibute == ""){
                string nno = "asdasd";
            }
            Dictionary<int, List<Dictionary<string, int>>> splitdat = SplitData(data, MaxAtriibute);
            foreach (int value in splitdat.Keys)
            {   
                Node child = BuidDecisionTree(splitdat[value], Attributes);
                child.Value = value;
                root.Children.Add(child); 
            }
            return root;

        }

        static Dictionary<int, List<Dictionary<string, int>>> SplitData(List<Dictionary<string, int>> data, string Atriibutes)
        {
            Dictionary<int, List<Dictionary<string, int>>> splitdata = new Dictionary<int, List<Dictionary<string, int>>>();
            foreach(Dictionary<string, int> item in data)
            {
                int value = item[Atriibutes];

                if(splitdata.ContainsKey(value))
                {
                    splitdata[value].Add(item);
                }
                else
                {
                    splitdata[value] = new List<Dictionary<string, int>>() { item};
                }
            }
            return splitdata;
        }
            
        static double informationGain(List<Dictionary<string,int>> data,string Attributes) {
            double originalEntropy = Entropy(data);

            Dictionary<int,List<Dictionary<string,int>>> splitData = SplitData(data, Attributes);


            double WeightEntropy = 0;
            foreach(int value in splitData.Keys)
            {
                double subsetEntro = Entropy(splitData[value]);
                double subsetWeigh = (double)splitData[value].Count / data.Count;
                WeightEntropy += subsetEntro * subsetWeigh;
            }
            double informationGain = originalEntropy - WeightEntropy;

            return informationGain;
        }
        static double Entropy(List<Dictionary<string, int>> data) {
            Dictionary<int, int> classCounts = new Dictionary<int, int>();
            foreach(Dictionary<string, int > item in data)
            {
                int classification = item["NutributionId"];
                if (classCounts.ContainsKey(classification))
                {
                    classCounts[classification]++;
                }
                else
                {
                    classCounts[classification] = 1;
                }
            }

            double entropy = 0;
            foreach (int count in classCounts.Values)
            {
                double probali = (double)count/ data.Count;
                entropy -= probali*Math.Log(probali,2);
            }

            return entropy;
        }
    }
}
