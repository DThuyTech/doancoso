namespace login.Logic
{
    public class Node
    {
        public string Attribute { get; set; }
        public int Value  { get; set; }
        public List<int> Label { get; set; } = new List<int>();
        public List<String> atributeChecked { get; set; }

        public List<Node> Children { get; set; }

        public Node()
        {
            Children = new List<Node>();
        }
        
    }
}
