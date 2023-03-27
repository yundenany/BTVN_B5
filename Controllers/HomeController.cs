using BTVN_B5_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace BTVN_B5_5.Controllers
{
    public class HomeController : Controller
    {
        private BookModelContext db = new BookModelContext();

        // GET: Books
        public ActionResult Index(string searchString, int? page)
        {
            if (page == null) page = 1;
            var links = (from l in db.Books select l).OrderBy(x => x.Id);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var context = new BookModelContext();
            return View(context.Books.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetBookByCategory(int id)
        {
            var context = new BookModelContext();
            return View("Index", context.Books.Where(p => p.CategoryId == id).ToList());
        }

        public ActionResult GetCategory(int id)
        {
            var context = new BookModelContext();
            var listCategory = context.Categories.ToList();
            return PartialView(listCategory);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Search(string searchString)
        {
            var context = new BookModelContext().Books.Where(p => p.Title.Trim().ToLower().Contains(searchString.Trim().ToLower())).ToList();
            return View("Index", context.ToPagedList(1, 3));
        }

        public ActionResult AddToCart(int id)
        {
            try
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            catch (Exception ex)
            {
                return Content("Success Adding");
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}