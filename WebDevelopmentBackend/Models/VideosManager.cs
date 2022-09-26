using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebDevelopmentBackend.Models
{
    public class VideosManager
    {
        private string connStr;

        public VideosManager()
        {
            connStr = "Data Source=.;Initial Catalog=FirstServerSideAppDB;Trusted_Connection=True;";
        }

        public VideosManager(string connectionString)
        {
            connStr = connectionString;
        }

        public VideoModel GetVideoByID(int ID)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(connStr);

                command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM [Videos] WHERE ID = @ID";
                command.Parameters.AddWithValue("@ID", ID);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(reader);

                if(dt != null && dt.Rows.Count > 0)
                {
                    VideoModel video = new VideoModel();

                    video.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                    video.Title = dt.Rows[0]["ID"].ToString();
                    video.Description = dt.Rows[0]["Description"].ToString();
                    video.Source = dt.Rows[0]["Source"].ToString();
                    video.UploadedAt = dt.Rows[0]["UploadedAt"].ToString();

                    return video;
                }

                throw new Exception("No record found");

            } catch (Exception ex)
            {
                throw ex;
            } finally
            {
                connection.Close();
            }
        }

        public List<VideoModel> GetVideosByUser(string UploaderEmail)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = new SqlConnection(connStr);

                command = connection.CreateCommand();

                command.CommandText = "SELECT * FROM [Videos] WHERE UploadedBy = '@UploadedBy'";
                command.Parameters.AddWithValue("@UploadedBy", UploaderEmail);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(reader);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<VideoModel> videos = new List<VideoModel>();

                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        VideoModel video = new VideoModel();

                        video.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        video.Title = dt.Rows[i]["ID"].ToString();
                        video.Description = dt.Rows[i]["Description"].ToString();
                        video.Source = dt.Rows[i]["Source"].ToString();
                        video.UploadedAt = dt.Rows[i]["UploadedAt"].ToString();


                        videos.Add(video);
                    }

                    return videos;
                }

                throw new Exception("No record found");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}