using Microsoft.AspNetCore.Mvc.Rendering;

namespace login.Viewmodels
{
    public class ContentFoodViewmodel
    {
        public string NameTypes { get; set; }

        public List<SelectListItem> ListidtypeFood { get; set; }
    }
}
