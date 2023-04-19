namespace login.Logic
{
    public class BMR
    {
        public int sex { get; set; }
        public float heigh { get; set; }
        public float weigh { get; set; }
        public int age { get; set; }

        public int hoatdong { get; set; }

        public BMR(int sex, float heigh, float weigh, int age, int hoatdong)
        {
            this.sex = sex;
            this.heigh = heigh;
            this.weigh = weigh;
            this.age = age;
            this.hoatdong = hoatdong;
        }

        public float clorisneed()
        {
            if (sex == 1)
            {
                return (float)((float)weigh * 2.204623f * 10 + 6.25 * (float)heigh / 2.54f - 5 * (float)age + 5);
            }
            return (float)((float)weigh * 2.204623f * 10 + 6.25 * (float)heigh / 2.54f - 5 * (float)age + -161);
        }


        public float TDEE()
        {
            if (hoatdong == 1)
            {
                return clorisneed() * 1.2f;
            }
            else if (hoatdong == 2)
            {
                return clorisneed() * 1.375f;

            }
            else if (hoatdong == 3)
            {
                return clorisneed() * 1.55f;
            }
            else if (hoatdong ==4)
            {
                return clorisneed() * 1.725f;
            }

            else 
            {
                return clorisneed() * 1.9f;
            }
        }
    }
}
