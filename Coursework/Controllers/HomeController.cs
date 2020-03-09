using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Coursework.Models;
using Coursework.DAL;


namespace Coursework.Controllers
{
    public class HomeController : Controller
    {
        //code developed using the tutorial Getting Started with Entity Framework 6 Code First using MVC 5 found at https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/

        private SiteContext db = new SiteContext();

        public ActionResult Index()
        {
            //code take from https://www.codeproject.com/Articles/31914/Beginner-s-Guide-To-ASP-NET-Cookies
            HttpCookie reqCookies = Request.Cookies["UserID"];
            if(reqCookies != null)
            {
                int userID = Convert.ToInt32(Request.Cookies["UserID"].Value);
                var user = db.Users.Where(x => x.ID == userID).SingleOrDefault();
                

                Session["UserID"] = user.ID.ToString();
                Session["Username"] = user.Username.ToString();
                Session["Role"] = user.Role.ToString();
            }
            // code taken from https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take?view=netframework-4.8
            ViewBag.LatestThree = db.Causes.Include(c => c.Category).OrderByDescending(c => c.createdOn).Take(3).ToList();

            return View();
        }

        //Json login written using 
        public JsonResult Login(User model)
        {

            //code take from https://www.youtube.com/watch?v=dzT0bGUS6Fs
            string result = "Fail";
            var DataItem = db.Users.Where(x => x.Email == model.Email && x.Password == model.Password).SingleOrDefault();
            if (DataItem != null)
            {
                Session["UserID"] = DataItem.ID.ToString();
                Session["UserName"] = DataItem.Username.ToString();
                Session["Role"] = DataItem.Role.ToString();
                result = DataItem.Username.ToString();

                Response.Cookies["UserID"].Value = DataItem.ID.ToString();
                Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(7);

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}