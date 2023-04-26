using Microsoft.AspNetCore.Mvc.Rendering;

namespace login.Viewmodels
{
    public class ValueAnswerViewModel
    {
        public string valueAnswerTas { get; set; }
        public string valueAnswerEmo { get; set; }
        public string valueAnswerCol { get; set; }
        public string valueAnswerAge { get; set; }
        public string valueAnswerSex { get; set; }

        public string valueAnswerActiv {get; set; }


        public List<SelectListItem> valuaAnswerSelectdListTaste { get; set; }
        public List<SelectListItem> valuaAnswerSelectdListAge { get; set; }

        public List<SelectListItem> valuaAnswerSelectdListSex { get; set; }

        public List<SelectListItem> valuaAnswerSelectdListColor { get; set; }

        public List<SelectListItem> valuaAnswerSelectdListEmotion { get; set; }

        public List<SelectListItem> valueAnswerSelectdListActiv { get; set; }
    }
}
