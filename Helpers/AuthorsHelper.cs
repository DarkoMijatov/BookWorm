using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using BookWorm.Models;

namespace BookWorm.Helpers
{
    public class AuthorsHelper
    {
        public static List<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "select * from Authors order by Name";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Author author = new Author();
                    author.Id = Convert.ToInt32(reader["ID"]);
                    author.Name = reader["Name"].ToString();
                    author.Country = reader["Country"].ToString();
                    author.Books = new List<string>();
                    authors.Add(author);
                }
            }
            foreach (Author author in authors)
            {
                query = "select Title from Books where AuthorID=" + author.Id.ToString();
                command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        author.Books.Add(reader["Title"].ToString());
                    }
                }
            }
            connection.Close();
            return authors;
        }

        public static void PostAuthor(Author author)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "if not exists(select * from Authors where Name='" + author.Name +
                "') insert into Authors(Name,Country) values('" + author.Name + "','" + author.Country + "')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void EditAuthor(Author author)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "update Authors set Name = '" + author.Name +
                "', Country = '" + author.Country + "' where ID = " + author.Id.ToString();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void DeleteAuthor(Author author)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "delete from Authors where ID = " + author.Id.ToString();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}