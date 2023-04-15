using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class ValueAnswers
    {
        public static List<ValueAnswer> getAllEmotion()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value="EB",Description="Buon" },
                new ValueAnswer() {Value="EG",Description="Tuc gian" },
                new ValueAnswer() {Value="EV",Description="Vui" },
            };
        }
        public static List<ValueAnswer> getAllAge()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value="ATI",Description="Thieu nien" },
                new ValueAnswer() {Value="ATH",Description="Thanh nien" },

            };
        }

        public static List<ValueAnswer> getAllSex()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value="SF",Description="Nu" },
                new ValueAnswer() {Value="SM",Description="Nam" },

            };
        }
        public static List<ValueAnswer> getAllTaste()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value="TM",Description="Man" },
                new ValueAnswer() {Value="TN",Description="Ngot" },

            };
        }
        public static List<ValueAnswer> getAllColor()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value="CS",Description="Sang" },
                new ValueAnswer() {Value="CT",Description="Toi" },

            };
        }
    }
}
