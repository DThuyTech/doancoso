namespace login.Logic
{
    public class ChangeDVT
    {
        public ChangeDVT()
        {
        }

        public int change(int Value)
        {
            if (Value == 4) return 4;
            if(Value == 2) return 4;
            if (Value == 5) return 2;
            if( Value== 6) return 1;
            if(Value==7) return 1;
             return 1;
        }
    }
}
