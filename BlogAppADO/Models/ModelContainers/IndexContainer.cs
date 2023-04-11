using static BlogAppADO.Models.EntityModels;

namespace BlogAppADO.Models.ModelContainers
{
    public class IndexContainer
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<InPosts> IndexPosts { get; set; } = new List<InPosts>();

        
    }
}

public class InPosts
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string PhotoLink { get; set; }
    public DateTime PublishDate { get; set; }
    public string CategoryName { get; set; }
    public string UserName { get; set; }
}