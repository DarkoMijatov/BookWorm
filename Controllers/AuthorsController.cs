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
    public class AuthorsController : Controller
    {
        // GET: Authors
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : sortOrder == "name" ? "name_desc" : "name";
            ViewBag.CountrySortParam = sortOrder == "country" ? "country_desc" : "country";
            ViewBag.BooksSortParam = sortOrder == "books" ? "books_desc" : "books";
            List<Author> authors = AuthorsHelper.GetAuthors();
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
                authors = authors.Where(a => a.Name.ToLower().Contains(searchString.ToLower().Trim()) ||
                a.Country.ToLower().Contains(searchString.ToLower().Trim()) ||
                string.Join("", a.Books).ToLower().Contains(searchString.ToLower().Trim())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    authors = authors.OrderByDescending(a => a.Name).ToList();
                    break;
                case "name":
                    authors = authors.OrderBy(a => a.Name).ToList();
                    break;
                case "country_desc":
                    authors = authors.OrderByDescending(a => a.Country).ToList();
                    break;
                case "country":
                    authors = authors.OrderBy(a => a.Country).ToList();
                    break;
                case "books_desc":
                    authors = authors.OrderByDescending(a => string.Join("", a.Books)).ToList();
                    break;
                case "books":
                    authors = authors.OrderBy(a => string.Join("", a.Books)).ToList();
                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(authors.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            AuthorsHelper.PostAuthor(author);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Author author = AuthorsHelper.GetAuthors().Where(a => a.Id == id).First();
            return View(author);
        }
    }
}