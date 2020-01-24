using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookWorm.Models;
using BookWorm.Helpers;
using PagedList;

namespace BookWorm.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        [HttpGet]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : sortOrder == "title" ? "title_desc" : "title";
            ViewBag.AuthorSortParam = sortOrder == "author" ? "author_desc" : "author";
            ViewBag.PublisherSortParam = sortOrder == "publisher" ? "publisher_desc" : "publisher";
            ViewBag.YearSortParam = sortOrder == "year" ? "year_desc" : "year";
            ViewBag.GenreSortParam = sortOrder == "genre" ? "genre_desc" : "genre";
            ViewBag.LanguageSortParam = sortOrder == "language" ? "language_desc" : "language";
            ViewBag.ReadSortParam = sortOrder == "read" ? "read_desc" : "read";
            List<Book> books = BooksHelper.GetBooks();
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.ToLower().Contains(searchString.ToLower().Trim()) ||
                b.Author.ToLower().Contains(searchString.ToLower().Trim()) ||
                b.Publisher.ToLower().Contains(searchString.ToLower().Trim()) ||
                b.Genre.ToLower().Contains(searchString.ToLower().Trim()) ||
                b.Language.ToLower().Contains(searchString.ToLower().Trim()) ||
                b.Year.ToString().ToLower().Contains(searchString.ToLower().Trim())).ToList();
            }
            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title).ToList();
                    break;
                case "title":
                    books = books.OrderBy(b => b.Title).ToList();
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author).ToList();
                    break;
                case "author":
                    books = books.OrderBy(b => b.Author).ToList();
                    break;
                case "publisher_desc":
                    books = books.OrderByDescending(b => b.Publisher).ToList();
                    break;
                case "publisher":
                    books = books.OrderBy(b => b.Publisher).ToList();
                    break;
                case "year_desc":
                    books = books.OrderByDescending(b => b.Year).ToList();
                    break;
                case "year":
                    books = books.OrderBy(b => b.Year).ToList();
                    break;
                case "genre_desc":
                    books = books.OrderByDescending(b => b.Genre).ToList();
                    break;
                case "genre":
                    books = books.OrderBy(b => b.Genre).ToList();
                    break;
                case "lanugage_desc":
                    books = books.OrderByDescending(b => b.Language).ToList();
                    break;
                case "language":
                    books = books.OrderBy(b => b.Language).ToList();
                    break;
                case "read_desc":
                    books = books.OrderByDescending(b => b.Read).ToList();
                    break;
                case "read":
                    books = books.OrderBy(b => b.Read).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Book book = BooksHelper.GetBooks().Where(b => b.Id == id).First();
            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            BooksHelper.PostBook(book);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Book book = BooksHelper.GetBooks().Where(b => b.Id == id).First();
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(int id, Book book)
        {
            BooksHelper.EditBook(book);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Book book = BooksHelper.GetBooks().Where(b => b.Id == id).First();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, Book book)
        {
            BooksHelper.DeleteBook(book);
            return RedirectToAction("Index");
        }
    }
}