namespace BlogAppADO.Models
{
    public class EntityModels
    {
        public class Post
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Slug { get; set; }
            public string Details { get; set; }
            public string PhotoLink { get; set; }
            public DateTime PublishDate { get; set; }
            public DateTime UpdatedOn { get; set; }
            public int UserID { get; set; }
            public int CategoryID { get; set; }
        }

        public class User
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string? Bio { get; set; }
            public string? PhotoLink { get; set; }
            public DateTime? JoinedAt { get; set; }
        }

        public class Comment
        {
            public int ID { get; set; }
            public string CommentMsg { get; set; }
            public bool IsLiked { get; set; }
            public DateTime AddedAt { get; set; }
            public int UserID { get; set; }
            public int PostID { get; set; }
        }

        public class Category
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string PhotoLink { get; set; }
        }
    }
}
