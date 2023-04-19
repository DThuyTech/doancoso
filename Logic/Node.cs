namespace login.Logic
{
    public class Node
    {
        public string Attribute { get; set; }
        public int Value { get; set; }
        public int Label { get; set; }
        public List<Node> Children { get; set; }

        public Node()
        {
            Children = new List<Node>();
        }
    }
}
