using System.Runtime.InteropServices;

namespace login.Logic
{
    public class BMR
    {
        public int sex { get; set; }
        public float heigh { get; set; }
        public float weigh { get; set; }
        public int age { get; set; }
        
        public int hoatdong { get; set; }

        public int mucdich { get; set; }

        public BMR(int sex, float heigh, float weigh, int age, int hoatdong,int mucdich)
        {
            this.sex = sex;
            this.heigh = heigh;
            this.weigh = weigh;
            this.age = age;
            this.hoatdong = hoatdong;
            this.mucdich = mucdich;
        }

        public float clorisneed()
        {
            if (sex == 1)
            {
                return (float)((float)weigh * 2.204623f * 10 + 6.25 * (float)heigh / 2.54f - 5 * (float)age + 5);
            }
            return (float)((float)weigh * 2.204623f * 10 + 6.25 * (float)heigh / 2.54f - 5 * (float)age + -161);
        }

        public float neesuiwwithReach()
        {
            if (mucdich == 0)
            {
                return clorisneed();
            }
            else if(mucdich == 1)
            {
                return clorisneed()- 300;
            }
            else
            {
                return clorisneed() + 200;
            }
        }

        public float TDEE()
        {
            if (hoatdong == 0)
            {
                return neesuiwwithReach() * 1.2f;
            }
            else if (hoatdong == 1)
            {
                return neesuiwwithReach() * 1.3f;

            }
            else if (hoatdong == 2)
            {
                return neesuiwwithReach() * 1.5f;
            }
            else 
            {
                return neesuiwwithReach() * 1.7f;
            }

          
        }
    }
}
