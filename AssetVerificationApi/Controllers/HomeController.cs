using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetVerificationApi.Models;
using AssetVerificationApi.Context_;

namespace AssetVerificationApi.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            

            ViewBag.Title = "Home Page";

            AssetModel d = new AssetModel()
            {
                //Name = "dd"
            };
            db.AssetModel.Add(d);
            db.SaveChanges();
            return View();
        }
    }
}
