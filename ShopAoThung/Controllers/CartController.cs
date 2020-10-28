using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ShopAoThung.Models;
namespace ShopAoThung.Controllers
{
    public class CartController : Controller
    {
        // khởi tạo session:
        private const string SessionCart = "SessionCart";
        // GET: Cart
        BookstoreDbContext db = new BookstoreDbContext();
        public ActionResult Index()
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
            }
            else if (cart == null)
            {
                ViewBag.statusCart = "Giỏ hàng trống";

            }
            return View(list);
        }
        public ActionResult cart_header()
        {
            var cart = Session[SessionCart];
            var list = new List<Cart_item>();
            float priceTotol = 0;
            if (cart != null)
            {
                list = (List<Cart_item>)cart;
                foreach (var item1 in list)
                {
                    if (item1.product.pricesale > 0)
                    {
                        int temp = (((int)item1.product.price) - ((int)item1.product.price / 100 * (int)item1.product.pricesale)) * ((int)item1.quantity);

                        priceTotol += temp;
                    }
                    else
                    {
                        int temp = (int)item1.product.price * (int)item1.quantity;
                        priceTotol += temp;
                    }
                }
            }
            ViewBag.CartTotal = priceTotol;
            return View("_cartHeader", list);
        }
        public RedirectToRouteResult updateitem(long P_SanPhamID, int P_quantity)
        {
            var cart = Session[SessionCart];
            var list = (List<Cart_item>)cart;
            Cart_item itemSua = list.FirstOrDefault(m => m.product.ID == P_SanPhamID);
            if (itemSua != null)
            {
                itemSua.quantity = P_quantity;
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public JsonResult Additem(long productID, int quantity)
        {
            var item = new Cart_item();
            Product product = db.Products.Find(productID);
            var cart = Session[SessionCart];
            if (cart != null)
            {
                var list = (List<Cart_item>)cart;
                if (list.Exists(m => m.product.ID == productID))
                {
                    int quantity1 = 0;
                    foreach (var item1 in list)
                    {
                        if (item1.product.ID == productID)
                        {
                            item1.quantity += quantity;
                            quantity1 = item1.quantity;
                        }
                    }
                    var cartCount1 = list.Count();
                    return Json(
                        new
                        {
                           cartCount = cartCount1
                        }
                        , JsonRequestBehavior.AllowGet) ;

                }
                else
                {
                    item.product = product;
                    item.quantity = quantity;
                    list.Add(item);
                    item.countCart = list.Count();
                    var cartCount1 = list.Count();
                    return Json(
                        new
                        {
                            cartCount = cartCount1
                        }
                        , JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                item.product = product;
                item.quantity = quantity;
                item.countCart = 1;
                item.priceTotal = (int)product.price;
                var list = new List<Cart_item>();
                list.Add(item);
                Session[SessionCart] = list;

            }
                    return Json(
                        new
                        {
                           cartCount = 1
                        }
                       , JsonRequestBehavior.AllowGet);
        }
    }
}
