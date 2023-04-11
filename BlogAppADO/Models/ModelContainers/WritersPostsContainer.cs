namespace BlogAppADO.Models.ModelContainers
{
    public class WritersPostsContainer
    {
        public List<WritersPostsPosts> WritersPostsPosts { get; set; } = new List<WritersPostsPosts>();
        public WritersPostsUser WritersPostsUser { get; set; } = new WritersPostsUser();
        public WritersPostsCategory WritersPostsCategory { get; set;} = new WritersPostsCategory();
    }
}

public class WritersPostsPosts
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Details { get; set; }
    public string PhotoLink { get; set; }
    public DateTime PublishDate { get; set; }
    public string CategoryName { get; set; }
    public string UserName { get; set; }
}

public class WritersPostsUser
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string PhotoLink { get; set; }
}

public class WritersPostsCategory
{
    public int ID { get; set; }
    public string Name { get; set; }
}