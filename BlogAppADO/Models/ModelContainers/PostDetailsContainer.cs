namespace BlogAppADO.Models.ModelContainers
{
    public class PostDetailsContainer
    {
        public PostDetailUser User { get; set; } = new PostDetailUser();
        public PostDetailPost Post { get; set; } = new PostDetailPost();
        public PostDetailsCategory Category { get; set; } = new PostDetailsCategory();
        public List<PostDetailComment> Comments { get; set; } = new List<PostDetailComment>();
    }
}

public class PostDetailUser
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string PhotoLink { get; set; }
}


public class PostDetailPost
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Details { get; set; }
    public string PhotoLink { get; set; }
    public DateTime PublishDate { get; set; }
}

public class PostDetailComment
{
    public int ID { get; set; }
    public string CommentMsg { get; set; }
    //public bool IsLiked { get; set; }
    public DateTime AddedAt { get; set; }
    public string UserName { get; set; }
    public string UserPhotoLink { get; set; }
}

public class PostDetailsCategory
{
    public int ID { get; set; }
    public string Name { get; set; }
}