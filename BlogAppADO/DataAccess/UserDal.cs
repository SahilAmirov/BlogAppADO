using static BlogAppADO.Models.EntityModels;
using System.Data.SqlClient;
using static BlogAppADO.Models.Dtos.PostDtos;

namespace BlogAppADO.DataAccess
{
    public class UserDal
    {
        public string connectionString = ConnectionString.ConnectionValue;

        public List<User> GetAll()
        {
            var userList = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, Name, Email, Password, Bio, PhotoLink, JoinedAt FROM ADO_Users ORDER BY JoinedAt DESC", connection);
                var reader = command.ExecuteReader();


                while (reader.Read())
                {
                    var userItem = new User();
                    userItem.ID = reader.GetInt32(0);
                    userItem.Name = reader.GetString(1);
                    userItem.Email = reader.GetString(2);
                    userItem.Password = reader.GetString(3);
                    userItem.Bio = reader.GetString(4);
                    userItem.PhotoLink = reader.GetString(5);
                    userItem.JoinedAt = reader.GetDateTime(6);

                    userList.Add(userItem);
                }
            }
            return userList;
        }

        public User GetUserWithId(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, Name, Email, Password, Bio, PhotoLink, JoinedAt FROM ADO_Users WHERE ID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();
                reader.Read();
                var userItem = new User();
                userItem.ID = reader.GetInt32(0);
                userItem.Name = reader.GetString(1);
                userItem.Email = reader.GetString(2);
                userItem.Password = reader.GetString(3);
                userItem.Bio = reader.GetString(4);
                userItem.PhotoLink = reader.GetString(5);
                userItem.JoinedAt = reader.GetDateTime(6);

                return userItem;
            }
        }

        public User GetUserWithEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT ID, Name, Email, Password, Bio, PhotoLink, JoinedAt FROM ADO_Users WHERE Email = @email", connection);
                command.Parameters.AddWithValue("@email", email);
                var reader = command.ExecuteReader();
                reader.Read();
                var userItem = new User();
                userItem.ID = reader.GetInt32(0);
                userItem.Name = reader.GetString(1);
                userItem.Email = reader.GetString(2);
                userItem.Password = reader.GetString(3);
                userItem.Bio = reader.GetString(4);
                userItem.PhotoLink = reader.GetString(5);
                userItem.JoinedAt = reader.GetDateTime(6);

                return userItem;
            }
        }

        public bool AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "INSERT INTO ADO_Users (Name, Email, Password, Bio, PhotoLink, JoinedAt) VALUES (@name, @email, @password, @bio, @photolink, @joinedAt)",
                            connection);
                    //"INSERT INTO ADO_Users (Name, Email, Password, Bio, PhotoLink, JoinedAt) VALUES (@name, @email, @password, @bio, @photolink, @joinedAt)"

                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@bio", user.Bio);
                    command.Parameters.AddWithValue("@photolink", user.PhotoLink);
                    command.Parameters.AddWithValue("@joinedAt", DateTime.Now);

                    command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public bool UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand(
                            "UPDATE ADO_Users SET Name = @name, Email = @email, Password = @password, Bio = @bio, PhotoLink = @photoLink WHERE ID = @id",
                            connection);

                    command.Parameters.AddWithValue("@id", user.ID);
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@bio", user.Bio);
                    command.Parameters.AddWithValue("@photoLink", user.PhotoLink);
                    command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public bool DeleteUserWithId(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM ADO_Users WHERE ID = @id", connection);
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

        public User Login(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT ID, Name, Email, Password, Bio, PhotoLink, JoinedAt FROM ADO_Users WHERE Email = @email and Password = @password", connection);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    var userItem = new User();
                    userItem.ID = reader.GetInt32(0);
                    userItem.Name = reader.GetString(1);
                    userItem.Email = reader.GetString(2);
                    //userItem.Password = reader.GetString(3);
                    //userItem.Bio = reader.GetString(4);
                    //userItem.PhotoLink = reader.GetString(5);
                    //userItem.JoinedAt = reader.GetDateTime(6);

                    return userItem;
                }
            }
            catch (Exception)
            {

                return null;
            }
            
        }
    }
}
