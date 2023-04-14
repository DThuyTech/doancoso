namespace login.Logic
{
    public class IBM
    {
        public float cs { get; set; }
        public string kq { get; set; }

        public IBM(float cs)
        {
            this.cs = cs;
        }

        public string ketqua()
        {
            if (this.cs < 18.5)
                this.kq = "Gầy";
            if (18.5 < this.cs && this.cs < 24.9)
                this.kq = "Bình thường";
            if (this.cs >= 25 && this.cs < 30)
                this.kq = "Tiền béo phì";
            this.kq = "Bình thường";
            if (this.cs >= 30 && this.cs < 34.9)
                this.kq = "Béo phì độ I";
            if (this.cs >= 35 && this.cs < 39.9)
                this.kq = "Béo phì độ II";
            if (this.cs >= 40)
                this.kq = "Béo phì độ III";
            return this.kq;
        }
    }
}

