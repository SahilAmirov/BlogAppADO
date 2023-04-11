namespace BlogAppADO.Models.Dtos
{
    public class PostDtos
    {
        public class UpdatePost
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Slug { get; set; }
            public string Details { get; set; }
            public string PhotoLink { get; set; }
        }

        public class AddPost
        {
            public string Title { get; set; }
            public string Slug { get; set; }
            public string Details { get; set; }
            public string PhotoLink { get; set; }
            public int UserID { get; set; }
            public int CategoryID { get; set; }
        }
    }
}
