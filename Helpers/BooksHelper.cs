using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using BookWorm.Models;
using System.Configuration;

namespace BookWorm.Helpers
{
    public class BooksHelper
    {
        public static List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "select b.ID as ID, b.Title as Title, a.Name as Author, p.Publisher as Publisher," +
                "b.Year as Year, g.Genre as Genre, b.Language as Language, b.IsRead as IsRead, b.Description as Description " +
                "from Books b join Authors a on a.ID=b.AuthorID " +
                "join Publishers p on b.PublisherID=p.ID " +
                "join Genres g on g.ID=b.GenreID";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = Convert.ToInt32(reader["ID"]);
                    book.Title = reader["Title"].ToString();
                    book.Author = reader["Author"].ToString();
                    book.Publisher = reader["Publisher"].ToString();
                    book.Year = Convert.ToInt32(reader["Year"]);
                    book.Genre = reader["Genre"].ToString();
                    book.Language = reader["Language"].ToString();
                    book.Description = reader["Description"].ToString();
                    if (Convert.ToInt32(reader["IsRead"]) == 1) book.Read = true;
                    else book.Read = false;
                    books.Add(book);
                }
            }
            connection.Close();
            return books;
        }

        public static void PostBook(Book book)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "insert into Books(Title,AuthorID,PublisherID,Year,GenreID,Language,IsRead,Description) values (";
            query += "'" + book.Title + "',";
            query += "(select ID from Authors where Name='" + book.Author + "'),";
            query += "(select ID from Publishers where Publisher='" + book.Publisher + "'),";
            query += book.Year.ToString() + ",";
            query += "(select ID from Genres where Genre='" + book.Genre + "'),";
            query += "'" + book.Language + "',";
            query += book.Read ? "1," : "0,";
            query += "'" + book.Description + "')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void EditBook(Book book)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "update Books set ";
            query += "Title = '" + book.Title + "',";
            query += "AuthorID = (select ID from Authors where Name = '" + book.Author + "'),";
            query += "PublisherID = (select ID from Publishers where Publisher = '" + book.Publisher + "'),";
            query += "Year = " + book.Year.ToString() + ",";
            query += "GenreID = (select ID from Genres where Genre = '" + book.Genre + "'),";
            query += "Language = '" + book.Language + "',";
            query += book.Read ? "IsRead = 1," : "IsRead = 0,";
            query += "Description = '" + book.Description + "' ";
            query += "where ID = " + book.Id.ToString();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void DeleteBook(Book book)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "delete from Books where ID = " + book.Id.ToString();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static List<string> GetGenres()
        {
            List<string> genres = new List<string>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString);
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
            string query = "select * from Genres order by ID";
            SqlCommand command = new SqlCommand(query, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    genres.Add(reader["Genre"].ToString());
                }
            }
            connection.Close();
            return genres;
        }
    }
}