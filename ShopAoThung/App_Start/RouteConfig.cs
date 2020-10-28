using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopAoThung
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //can doi
            routes.MapRoute(
            name: "huy thanh toan online",
            url: "cancel-order",
            defaults: new { controller = "Checkout", action = "cancel_order", id = UrlParameter.Optional },
            new[] { "ShopAoThung.Controllers" }
            );
            routes.MapRoute(
           name: "thanh toan thanh cong",
           url: "confirm-orderPaymentOnline",
           defaults: new { controller = "Checkout", action = "confirm_orderPaymentOnline", id = UrlParameter.Optional },
           new[] { "ShopAoThung.Controllers" }
           );
            routes.MapRoute(
           name: "huy thanh toan online momo",
           url: "cancel-order-momo",
           defaults: new { controller = "Checkout", action = "cancel_order_momo", id = UrlParameter.Optional },
           new[] { "ShopAoThung.Controllers" }
           );

            routes.MapRoute(
           name: "thanh toan thanh cong momo",
           url: "confirm-orderPaymentOnline-momo",
           defaults: new { controller = "Checkout", action = "confirm_orderPaymentOnline_momo", id = UrlParameter.Optional },
           new[] { "ShopAoThung.Controllers" }
           );

            routes.MapRoute(
        name: "thanh-toan",
        url: "thanh-toan",
        defaults: new { controller = "Checkout", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
        name: "giohang",
        url: "giohang",
        defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );
        }
    }
}
