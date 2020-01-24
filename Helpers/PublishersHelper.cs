using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using BookWorm.Models;

namespace BookWorm.Helpers
{
    public class PublishersHelper
    {
        public static List<Publisher> GetPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "select * from Publishers order by Publisher";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Publisher publisher = new Publisher();
                    publisher.Id = Convert.ToInt32(reader["ID"]);
                    publisher.Name = reader["Publisher"].ToString();
                    publisher.Country = reader["Country"].ToString();
                    publisher.Authors = new List<string>();
                    publishers.Add(publisher);
                }
            }
            foreach (Publisher publisher in publishers)
            {
                query = "select distinct a.Name as Name from Authors a join Books b on b.AuthorID=a.ID" +
                    " where b.PublisherID=" + publisher.Id.ToString();
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        publisher.Authors.Add(reader["Name"].ToString());
                    }
                }
            }
            connection.Close();
            return publishers;
        }

        public static void PostPublisher(Publisher publisher)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "if not exists(select * from Publishers where Publisher='" + publisher.Name + "')" +
                " insert into Publishers(Publisher,Country) values('" + publisher.Name + "','" + publisher.Country + "')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void EditPublisher(Publisher publisher)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "update Publishers set Publisher = '" + publisher.Name +
                "' , Country = '" + publisher.Country + "' where ID = " + publisher.Id.ToString();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void DeletePublisher(Publisher publisher)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "delete from Publishers where ID = " + publisher.Id.ToString();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}