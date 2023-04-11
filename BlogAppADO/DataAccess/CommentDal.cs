using System.Data.SqlClient;
using static BlogAppADO.Models.Dtos.PostDtos;
using static BlogAppADO.Models.EntityModels;

namespace BlogAppADO.DataAccess
{
    public class CommentDal
    {
        public string connectionString = ConnectionString.ConnectionValue;

        public List<Comment> GetComments(int postID)
        {
            var commentList = new List<Comment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, CommentMsg, IsLiked, AddedAt, UserID, PostID FROM ADO_Comments WHERE PostID = @postID ORDER BY AddedAt DESC", connection);
                command.Parameters.AddWithValue("@postID", postID);
                var reader = command.ExecuteReader();


                while (reader.Read())
                {
                    var commentItem = new Comment();
                    commentItem.ID = reader.GetInt32(0);
                    commentItem.CommentMsg = reader.GetString(1);
                    commentItem.IsLiked = reader.GetBoolean(2);
                    commentItem.AddedAt = reader.GetDateTime(3);
                    commentItem.UserID = reader.GetInt32(4);
                    commentItem.PostID = reader.GetInt32(5);

                    commentList.Add(commentItem);
                }
            }
            return commentList;
        }

        public bool AddComment(Comment comment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "INSERT INTO ADO_Comments (CommentMsg, IsLiked, AddedAt, UserID, PostID) VALUES (@commentMsg, @isLiked, @addedAt, @userID, @postID)",
                            connection);

                    command.Parameters.AddWithValue("@commentMsg", comment.CommentMsg);
                    command.Parameters.AddWithValue("@isLiked", comment.IsLiked);
                    command.Parameters.AddWithValue("@addedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@userID", comment.UserID);
                    command.Parameters.AddWithValue("@postID", comment.PostID);

                    command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public bool DeleteComment(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM ADO_Comments WHERE ID = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
