using Lucene.Net.Support;

namespace login.Logic
{
    public class Logic
    {
        public List<NodeFs> _nodes { get; set; }
        public HashMap<String, String> cauhoi = new HashMap<string, string>();
        public NodeFs nEnd { get; set; }

        public Logic(List<NodeFs> nodes, HashMap<string, string> cauhoi)
        {
            _nodes = nodes;
            this.cauhoi = cauhoi;
        }

        public NodeFs duyet(NodeFs node)
        {
            if (node.ListNodenext.Count == 0)
            {
                return node;
            }
            else
            {
                for (int i = 0; i < node.ListNodenext.Count; i++)
                {
                    if (cauhoi[node.ListNodenext[i].thuoctinh.ToString()] == node.ListNodenext[i].dieukien.ToString())
                    {
                        return duyet(node.ListNodenext[i]);
                    }
                    else
                    {
                        node.ListNodenext.Remove(node.ListNodenext[i]);
                    }
                }
                return duyet(node);
            }
        }
    }
}
