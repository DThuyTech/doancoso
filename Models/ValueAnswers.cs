﻿using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class ValueAnswers
    {
        public static List<ValueAnswer> getAllEmotion()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value=1,Description="Tieu cuc" },
                new ValueAnswer() {Value=2,Description="Tich cuc" },
                new ValueAnswer() {Value=3,Description="Buon buc" },
            };
        }
        public static List<ValueAnswer> getAllAge()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value=1,Description="Thieu nien" },
                new ValueAnswer() {Value=2,Description="Thanh nien" },
                new ValueAnswer() {Value=3,Description="Trung nien" },

            };
        }

        public static List<ValueAnswer> getAllSex()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value=0,Description="Nu" },
                new ValueAnswer() {Value=1,Description="Nam" },

            };
        }
        public static List<ValueAnswer> getAllTaste()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value=1,Description="Man" },
                new ValueAnswer() {Value=2,Description="Ngot" },
                 new ValueAnswer() {Value=3,Description="Chua" },
                  new ValueAnswer() {Value=4,Description="Cay" },

            };
        }
        public static List<ValueAnswer> getAllColor()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value=1,Description="Sang" },
                new ValueAnswer() {Value=2,Description="Toi" },

            };
        }
        public static List<ValueAnswer> getAllActiv()
        {
            return new List<ValueAnswer>
            {
                new ValueAnswer() {Value=0,Description="Khong hoat dong manh" },
                new ValueAnswer() {Value=1,Description="Co nhung khong nhieu" },
                new ValueAnswer() {Value=2,Description="Tren 2 lan 1 tuan" },
                new ValueAnswer() {Value=3,Description="Tren 4 lan 1 tuan" },

            };
        }


        public  static List<ValueAnswer> getAllReach()
        {
            return new List<ValueAnswer> {
            new ValueAnswer() { Value=0, Description="Giu dang"},
            new ValueAnswer() { Value = 1, Description = "Giam can" },
            new ValueAnswer() { Value = 2, Description = "Tang can" }
            };
        }
    }
}
