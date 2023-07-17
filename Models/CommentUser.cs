namespace login.Models
{
    public class CommentUser
    {
        public string? Id { get; set; }
        public string idUser { get; set; }
        public string idFood { get; set; }
        public string content { get; set; }
        //gdfgdgfdgfdf
        public CommentUser()
        {
        }

        public CommentUser(string idUser, string idFood, string content)
        {
            this.idUser = idUser;
            this.idFood = idFood;
            this.content = content;
        }
    }
}
