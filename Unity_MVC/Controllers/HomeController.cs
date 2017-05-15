using Microsoft.Practices.Unity;
using Service.ImplServices;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity_MVC.Entity;

namespace Unity_MVC.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public I_t_Case_Main_Service Service_t_Case_Main_Service { get; set; }

        [Dependency]
        public Sugar _sugar { set; get; }

        public ActionResult Index()
        {
            //t_Case_Main_Service service = new t_Case_Main_Service();

            //var list = Service_t_Case_Main_Service.GetIQueryable().OrderBy(a => a.RootId).Skip(100).Take(100).ToList();

            IProduct milk = new Milk();

            return View();
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