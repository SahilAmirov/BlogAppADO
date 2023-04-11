namespace BlogAppADO.Models.Dtos
{
    public class CommentDtos
    {
        public class AddComment
        {
            public string Message { get; set; }
            public int PostID { get; set; }
        }
    }
}
