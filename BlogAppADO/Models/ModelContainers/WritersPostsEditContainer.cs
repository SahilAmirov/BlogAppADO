namespace BlogAppADO.Models.ModelContainers
{
    public class WritersPostsEditContainer
    {
        public List<WritersPostsEditPosts> WritersPostsEditPosts { get; set; } = new List<WritersPostsEditPosts>();
        public WritersPostsEditUser WritersPostsEditUser { get; set; } = new WritersPostsEditUser();
        public WritersPostsEditCategory WritersPostsEditCategory { get; set;} = new WritersPostsEditCategory();
    }
}

public class WritersPostsEditPosts
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

public class WritersPostsEditUser
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string PhotoLink { get; set; }
}

public class WritersPostsEditCategory
{
    public int ID { get; set; }
    public string Name { get; set; }
}