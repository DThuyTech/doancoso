namespace login.Logic
{
    public class BMR
    {
        public int sex { get; set; }
        public int heigh { get; set; }
        public int weigh { get; set; }
        public int age { get; set; }

        public BMR(int sex, int heigh, int weigh, int age)
        {
            this.sex = sex;
            this.heigh = heigh;
            this.weigh = weigh;
            this.age = age;
        }

        public float clorisneed()
        {
            if (sex == 1)
            {
                return (float)((float)heigh * 2.204623f * 10 + 6.25 * (float)weigh / 2.54f - 5 * (float)age + 5);
            }
            return (float)((float)weigh * 2.204623f * 10 + 6.25 * (float)heigh / 2.54f - 5 * (float)age + -161);
        }
    }
}
