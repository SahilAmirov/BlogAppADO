using System.Data.SqlClient;
using System.Reflection;
using static BlogAppADO.Models.Dtos.PostDtos;
using static BlogAppADO.Models.EntityModels;

namespace BlogAppADO.DataAccess
{
    public class PostDal
    {
        public string connectionString = ConnectionString.ConnectionValue;

        public List<Post> GetAll()
        {
            var postList = new List<Post>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, Title, Slug,Details, PhotoLink, PublishDate, UpdatedOn, UserID, CategoryID FROM ADO_Posts ORDER BY PublishDate DESC", connection);
                var reader = command.ExecuteReader();


                while (reader.Read())
                {
                    var postItem = new Post();
                    postItem.ID = reader.GetInt32(0);
                    postItem.Title = reader.GetString(1);
                    postItem.Slug = reader.GetString(2);
                    postItem.Details = reader.GetString(3);
                    postItem.PhotoLink = reader.GetString(4);
                    postItem.PublishDate = reader.GetDateTime(5);
                    postItem.UpdatedOn = reader.GetDateTime(6);
                    postItem.UserID = reader.GetInt32(7);
                    postItem.CategoryID = reader.GetInt32(8);

                    postList.Add(postItem);
                }
            }
            return postList;
        }

        public Post GetPostWithId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, Title, Slug,Details, PhotoLink, PublishDate, UpdatedOn, UserID, CategoryID FROM ADO_Posts WHERE ID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();
                reader.Read();
                var postItem = new Post();
                postItem.ID = reader.GetInt32(0);
                postItem.Title = reader.GetString(1);
                postItem.Slug = reader.GetString(2);
                postItem.Details = reader.GetString(3);
                postItem.PhotoLink = reader.GetString(4);
                postItem.PublishDate = reader.GetDateTime(5);
                postItem.UpdatedOn = reader.GetDateTime(6);
                postItem.UserID = reader.GetInt32(7);
                postItem.CategoryID = reader.GetInt32(8);

                return postItem;
            }
        }

        public List<Post> GetPostWithUserId(int userID)
        {
            var postList = new List<Post>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, Title, Slug,Details, PhotoLink, PublishDate, UpdatedOn, UserID, CategoryID FROM ADO_Posts WHERE UserID = @userID", connection);
                command.Parameters.AddWithValue("@userID", userID);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var postItem = new Post();
                    postItem.ID = reader.GetInt32(0);
                    postItem.Title = reader.GetString(1);
                    postItem.Slug = reader.GetString(2);
                    postItem.Details = reader.GetString(3);
                    postItem.PhotoLink = reader.GetString(4);
                    postItem.PublishDate = reader.GetDateTime(5);
                    postItem.UpdatedOn = reader.GetDateTime(6);
                    postItem.UserID = reader.GetInt32(7);
                    postItem.CategoryID = reader.GetInt32(8);

                    postList.Add(postItem);
                }
            }
            return postList;
        }

        public Post GetPostWithSlug(string slug)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, Title, Slug,Details, PhotoLink, PublishDate, UpdatedOn, UserID, CategoryID FROM ADO_Posts WHERE Slug = @slug", connection);
                command.Parameters.AddWithValue("@slug", slug);
                var reader = command.ExecuteReader();
                reader.Read();
                var postItem = new Post();
                postItem.ID = reader.GetInt32(0);
                postItem.Title = reader.GetString(1);
                postItem.Slug = reader.GetString(2);
                postItem.Details = reader.GetString(3);
                postItem.PhotoLink = reader.GetString(4);
                postItem.PublishDate = reader.GetDateTime(5);
                postItem.UpdatedOn = reader.GetDateTime(6);
                postItem.UserID = reader.GetInt32(7);
                postItem.CategoryID = reader.GetInt32(8);

                return postItem;
            }
        }

        public List<Post> GetPostsWithCategoryId(int categoryID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var postList = new List<Post>();

                connection.Open();
                var command = new SqlCommand("SELECT ID, Title, Slug,Details, PhotoLink, PublishDate, UpdatedOn, UserID, CategoryID FROM ADO_Posts WHERE CategoryID = @categoryID", connection);
                command.Parameters.AddWithValue("@categoryID", categoryID);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var postItem = new Post();
                    postItem.ID = reader.GetInt32(0);
                    postItem.Title = reader.GetString(1);
                    postItem.Slug = reader.GetString(2);
                    postItem.Details = reader.GetString(3);
                    postItem.PhotoLink = reader.GetString(4);
                    postItem.PublishDate = reader.GetDateTime(5);
                    postItem.UpdatedOn = reader.GetDateTime(6);
                    postItem.UserID = reader.GetInt32(7);
                    postItem.CategoryID = reader.GetInt32(8);

                    postList.Add(postItem);
                }
                return postList;
            }
        }

        public bool UpdatePost(UpdatePost post)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "UPDATE ADO_Posts SET Title = @title, Slug = @slug, Details = @details, PhotoLink = @photoLink, UpdatedOn = @updatedOn WHERE ID = @id",
                            connection);

                    command.Parameters.AddWithValue("@id", post.ID);
                    command.Parameters.AddWithValue("@title", post.Title);
                    command.Parameters.AddWithValue("@slug", post.Slug);
                    command.Parameters.AddWithValue("@details", post.Details);
                    command.Parameters.AddWithValue("@photoLink", post.PhotoLink);
                    command.Parameters.AddWithValue("@updatedOn", DateTime.Now);
                    command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool AddPost(AddPost post)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "INSERT INTO ADO_Posts (Title, Slug, Details, PhotoLink, PublishDate, UpdatedOn, UserID, CategoryID) VALUES (@title, @photoLink, @details, @slug, @createdOn, @updatedOn, @userID, @categoryID)",
                            connection);

                    command.Parameters.AddWithValue("@title", post.Title);
                    command.Parameters.AddWithValue("@slug", post.Slug);
                    command.Parameters.AddWithValue("@details", post.Details);
                    command.Parameters.AddWithValue("@photoLink", post.PhotoLink);
                    command.Parameters.AddWithValue("@createdOn", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedOn", DateTime.Now);
                    command.Parameters.AddWithValue("@userID", post.UserID);
                    command.Parameters.AddWithValue("@categoryID", post.CategoryID);

                    command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public bool DeletePostWithId(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM ADO_Posts WHERE ID = @id", connection);
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