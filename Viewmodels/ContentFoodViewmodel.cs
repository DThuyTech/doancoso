using Microsoft.AspNetCore.Mvc.Rendering;

namespace login.Viewmodels
{
    public class ContentFoodViewmodel
    {
        public string NameTypes { get; set; }
        public string TenLoai { get; set; }

        public string dvt { get; set; }

        public List<SelectListItem> ListidtypeFood { get; set; }

        public List<SelectListItem> ListidLoaimon { get; set; }

        public List<SelectListItem> ListIddvt { get; set; }
    }
}
