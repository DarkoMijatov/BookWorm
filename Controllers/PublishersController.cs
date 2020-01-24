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
    public class PublishersController : Controller
    {
        // GET: Publishers
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : sortOrder == "name" ? "name_desc" : "name";
            ViewBag.CountrySortParam = sortOrder == "country" ? "country_desc" : "country";
            ViewBag.AuthorsSortParam = sortOrder == "authors" ? "authors_desc" : "authors";
            List<Publisher> publishers = PublishersHelper.GetPublishers();
            if (searchString != null)
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
                publishers = publishers.Where(p => p.Name.ToLower().Contains(searchString.ToLower().Trim()) ||
                p.Country.ToLower().Contains(searchString.ToLower().Trim()) ||
                string.Join("", p.Authors).ToLower().Contains(searchString.ToLower().Trim())).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    publishers = publishers.OrderByDescending(p => p.Name).ToList();
                    break;
                case "name":
                    publishers = publishers.OrderBy(p => p.Name).ToList();
                    break;
                case "country_desc":
                    publishers = publishers.OrderByDescending(p => p.Country).ToList();
                    break;
                case "country":
                    publishers = publishers.OrderBy(p => p.Country).ToList();
                    break;
                case "authors_desc":
                    publishers = publishers.OrderByDescending(p => string.Join("", p.Authors)).ToList();
                    break;
                case "authors":
                    publishers = publishers.OrderBy(p => string.Join("", p.Authors)).ToList();
                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(publishers.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Publisher publisher)
        {
            PublishersHelper.PostPublisher(publisher);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Publisher publisher = PublishersHelper.GetPublishers().Where(p => p.Id == id).First();
            return View(publisher);
        }
    }
}