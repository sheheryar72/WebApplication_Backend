using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebDevelopmentBackend.Models
{
    public class UsersManager
    {
        private string connStr;

        public UsersManager()
        {
            connStr = "Data Source=.;Initial Catalog=FirstServerSideAppDB;Trusted_Connection=True;";
        }

        public UsersManager(string connectionString)
        {
            connStr = connectionString;
        }

        public string AddUserInDB(User user)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(connStr);

                command = connection.CreateCommand();

                command.CommandText = "INSERT INTO [Users] (Email, Password, Name, Gender, DOB, CreatedAt) VALUES ('@Email', '@Password', '@Name', '@Gender', '@DOB', GETDATE())";

                connection.Open();

                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@DOB", user.DOB);

                int rowsAffected = command.ExecuteNonQuery();

                if(rowsAffected > 0)
                {
                    return user.Email;
                }

                throw new Exception("Some error occurred while inserting user");

            } catch (Exception ex)
            {
                throw ex;
            } finally
            {
                command = null;
                connection.Close();
            }
        }

        public bool AuthenticateUser(string Email, string Password)
        {
            if(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                throw new ArgumentNullException();
            }

            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(connStr);

                command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM [Users] WHERE [Email] = '@Email' AND [Password] = '@Password'";

                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Password", Password);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(reader);

                if(dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }

                return false;
            } catch (Exception ex)
            {
                throw ex;
            } finally
            {
                command = null;

                if(connection != null)
                {
                    connection.Close();
                }
            }
        }

    }
}