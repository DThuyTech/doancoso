using login.Models;
using login.Logic;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Bson;

namespace login.Logic
{
    public class Train2
    {
        public List<DbTrain> data { get; set; }

        public List<String> Attribute = new List<string>() { "Gender", "Age", "Activ", "Color", "KeyTaste", "KeyEmotion" };

        public Node rootEnd = new Node();
        public Train2(List<DbTrain> data)
        {
            this.data = data;
        }


        

        public List<Dictionary<string, int>> AddData()
        {
            List<Dictionary<string, int>> Datatrain = new List<Dictionary<string, int>>();
            List<DbTrain> newdata = new List<DbTrain>();
            newdata = data.Where(x => x.KeyEmotion == 2).ToList();
          
            foreach (DbTrain item in data)
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

           
            

        public Node BuidDecisionTree(List<Dictionary<string, int>> data)
        {


            
            Node root = new Node();
            List<string> attribute = new List<string>();




            //add list same food in data 

            Dictionary<string, int> foodfirst = data[0];
            int check;

            if (data.Count > 1)
            {

                check = 0;
                foreach (Dictionary<string, int> item in data)
                {
                    foreach (string nameCol in item.Keys)
                    {

                        if (nameCol != "NutributionId")
                        {
                            if (foodfirst[nameCol] != item[nameCol])
                            {
                                check++;
                            }
                        }
                    }
                    if (check > 0)
                    {
                        break;
                    }
                }
                if (check == 0)
                {
                    
                        foreach (Dictionary<string, int> item in data)
                        {
                            root.Label.Add(item["NutributionId"]);
                        }
                        return root;
                    
                }
            }

           

            foreach (string atributeonr in Attribute)
            {
                if (data.Select(x => x[atributeonr]).Distinct().Count() !=1)
                {
                    attribute.Add(atributeonr);
                }
            }
            if (data.Select(x => x["NutributionId"]).Distinct().Count() == 1)
            {
                root.Label.Add(data[0]["NutributionId"]);
                return root;

            }

            if (attribute.Count == 0)
            {
                root.Label.Add(data.GroupBy(x => x["NutributionId"]).OrderByDescending(x => x.Count()).First().Key);
                return root;
            }

            string bestAttribute = "";
            double bestInformationGain = 0;
            foreach (string attributes in attribute)
            {
                double informationGainn = informationGain(data, attributes);
                if (informationGainn >= bestInformationGain)
                {
                    bestAttribute = attributes;
                    bestInformationGain = informationGainn;
                }
            }

            root.Attribute = bestAttribute;

            if(Attribute.FirstOrDefault(p=>p == bestAttribute)== null)
            {
                string erro = "sdsd";
            }
           // attribute.Remove(bestAttribute);

            Dictionary<int, List<Dictionary<string, int>>> splitData = SplitData(data, bestAttribute);

            foreach(int value in splitData.Keys)
            {
                Node child = BuidDecisionTree(splitData[value]);
                child.Value = value;
                root.Children.Add(child);
            }

            return root;
        




    }

         Dictionary<int, List<Dictionary<string, int>>> SplitData(List<Dictionary<string, int>> data, string Atriibutes)
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
            
        public double informationGain(List<Dictionary<string,int>> data,string Attributes) {
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
        public double Entropy(List<Dictionary<string, int>> data) {
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
