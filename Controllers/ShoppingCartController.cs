using BTVN_B5_5.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTVN_B5_5.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
           /* var context = new BookModelContext();
            var db  = new CartItem();*/
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            if (ShoppingCart.Count == 0)
                return RedirectToAction("Index", "Home");

            ViewBag.Tongsoluong = ShoppingCart.Sum(p => p.Quantity);
            ViewBag.Tongtien = ShoppingCart.Sum(p => p.Quantity * p.Price);
            /*return View(db.CartItems.ToList());*/
            /*return View(db.CartItems.ToList());*/
            return View(ShoppingCart);
        }

        public List<CartItem> GetShoppingCartFromSession()
        {
            var listShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if (listShoppingCart == null)
            {
                listShoppingCart = new List<CartItem>();
                Session["ShoppingCart"] = listShoppingCart;
            }
            return listShoppingCart;
        }

        //Add vao gio hang
        public RedirectToRouteResult AddToCart(int id)
        {
            BookModelContext db = new BookModelContext();
            /* var context = new BookModelContext();*/
           List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            //List<CartItem> ShoppingCart = db.CartItems.ToList();
            CartItem findCartItem = ShoppingCart.FirstOrDefault(m => m.Id == id);
            Book findBook = db.Books.FirstOrDefault(m => m.Id == id);
           /* CartItem findCartItem = (CartItem)ShoppingCart.Select(p=>p);
            Book findBook = (Book)db.Books.Select(p=>p);*/
            if (findCartItem == null)
            {
                //var findBook = new CartItem();
                
                // findBook = db.Books.Include(p => p.Id).FirstOrDefault(m => m.Id == id);
                CartItem newItem = new CartItem() { Id = findBook.Id, Title = findBook.Title, Quantity = 1, Image = findBook.Image, Price = findBook.Price.Value, };
                ShoppingCart.Add(newItem);
            }
            else
                findCartItem.Quantity++;

            return RedirectToAction("Index", "ShoppingCart");
        }

        public RedirectToRouteResult UpdateCart(int id, int txtQuantity)
        {
            var itemFind = GetShoppingCartFromSession().FirstOrDefault(p => p.Id == id);
            if (itemFind != null)
            {
                itemFind.Quantity = txtQuantity;
            }
            return RedirectToAction("Index");
        }

        public ActionResult CartSummary()
        {
            ViewBag.CartCount = GetShoppingCartFromSession().Count();
            return PartialView("CartSummary");
        }

        public RedirectToRouteResult RemoveCartItem(int id)
        {
            var itemFind = GetShoppingCartFromSession().FirstOrDefault(p => p.Id == id);
            GetShoppingCartFromSession().Remove(itemFind);
            return RedirectToAction("Index", "ShoppingCart");
        }

        public RedirectToRouteResult Delete()
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            ShoppingCart.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Order()
        {
            string currentUserId = User.Identity.GetUserId();
            BookModelContext context = new BookModelContext();
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                /*  try
                  {*/

                /* new SqlCommand("SET IDENTITY_INSERT ON; Orders ;SET IDENTITY_INSERT OFF; ", "");*/

                Order objOrder = new Order()
                {
                    //OrderNo = 1,
                    CustumerID = null,
                    OrderDate = DateTime.Now,
                    DeliveryDate = null,
                    isPaid = false,
                    isComplete = false
                };
                objOrder = context.Orders.Add(objOrder);
                context.SaveChanges();

                List<CartItem> listCartItems = GetShoppingCartFromSession();
                foreach (var item in listCartItems)
                {
                    OrderDetail ctdh = new OrderDetail()
                    {
                        
                        BookID = item.Id,
                        OrderNo = objOrder.OrderNo,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };
                    //OrderDetail detail = OrderDetail;
                    OrderDetail orderDetail = context.OrderDetails.Add(ctdh);
                    context.SaveChanges();
                }
                transaction.Commit();
                /*   }
                   catch (Exception ex)
                   {
                       transaction.Rollback();
                       return Content("Gặp Lỗi Đặt Hàng: " + ex.Message);
                   }*/
            }
            Session["GioHang"] = null;
            return RedirectToAction("ConfirmOrder", "ShoppingCart");
        }

        public ActionResult ConfirmOrder()
        {
            return View();
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShoppingCart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShoppingCart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShoppingCart/Delete/5
        public ActionResult Deletes(int id)
        {
            return View();
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost]
        public ActionResult Deletes(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
