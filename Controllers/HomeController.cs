using BTVN_B5_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Web.WebPages;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq.Expressions;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace BTVN_B5_5.Controllers
{
    public class HomeController : Controller
    {
        private BookModelContext db = new BookModelContext();

        // GET: Books
        public ActionResult Index(int? page, string searchString)
        {
            var context = new BookModelContext();
            searchString = searchString ?? "";
            if (page == null) page = 1;
            var links = (from l in db.Books.AsEnumerable() where convertToUnSign3(l.Title.ToLower())
                        .Contains(searchString)  select l).OrderBy(l => l.Id);

            //var links = (from l in context.Books where convertToUnSign3(l.Title.ToLower()).Contains(convertToUnSign3(searchString)) select l).OrderBy(x => x.Id);
            if (!searchString.IsEmpty())
            {
                links = (from l in db.Books.AsEnumerable()
                         where convertToUnSign3(l.Title.ToLower())
                       .Contains(searchString.ToLower())
                         select l).OrderBy(l => l.Id);
                //links = (from l in context.Books where convertToUnSign3(l.Title.ToLower()).Contains(convertToUnSign3(searchString).ToLower()) select l).OrderBy(x => x.Id);

            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        public string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
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
            var context = new BookModelContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*Book book = db.Books.Find(id);*/
            Book book = context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost]
        public ActionResult Search(FormCollection formCollection)
        {
            string searchString = formCollection["search"].ToString();
            /* var context = new BookModelContext().Books.Where(p => p.Title.Trim().ToLower().Contains(searchString.Trim().ToLower())).ToList();
             Response.Write("<script>alert('"+ searchString.Trim().ToLower() + "')</script>");
             return View("Index", context.ToPagedList(1, 3));*/


            return RedirectToAction("Index", new { searchString });
        }

        public ActionResult AddToCart(int id)
        {
            /*  try
              {*/
            return RedirectToAction("AddToCart", "ShoppingCart", new { @id = id });
            //return RedirectToAction("ShoppingCart", new { id = 99 });
            //return RedirectToAction("AddToCart", "ShoppingCart", id);
            //}
            /* catch (Exception ex)
             {
                 return Content("Success Adding");
             }*/
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