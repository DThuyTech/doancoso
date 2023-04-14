using login.Models;

namespace login.Logic
{
    public class NodeFs
    {
        private int idNode { get; set; }
        public List<TypeFood> typeFoods { get; set; }
        public List<NodeFs> ListNodenext { get; set; } = new List<NodeFs>();
        public string nameNode;
        public string thuoctinh;
        public string dieukien;

        public NodeFs(int idNode, List<TypeFood> typeFoods, List<NodeFs> listNodenext, string nameNode, string thuoctinh, string dieukien)
        {
            this.idNode = idNode;
            this.typeFoods = typeFoods;
            this.ListNodenext = listNodenext;
            this.nameNode = nameNode;
            this.thuoctinh = thuoctinh;
            this.dieukien = dieukien;
        }
        public NodeFs(int idNode, List<TypeFood> typeFoods, String nameNode)
        {
            this.idNode = idNode;
            this.typeFoods = typeFoods;
            this.nameNode = nameNode;
        }
        public NodeFs()
        {

        }

        public NodeFs(int idNode, List<TypeFood> typeFoods, string thuoctinh, string dieukien)
        {
            this.idNode = idNode;
            this.thuoctinh = thuoctinh;
            this.dieukien = dieukien;
            this.typeFoods = typeFoods;
        }

        public void AddNodeNext(NodeFs node)
        {
            this.ListNodenext.Add(node);
        }
        public void Addlist(NodeFs node)
        {
            this.ListNodenext.Add(node);
        }
    }
}
