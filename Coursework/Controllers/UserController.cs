//Controller code writen using tutorial https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/


using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coursework.DAL;
using Coursework.Models;
using System.Data.Entity.Infrastructure;

namespace Coursework.Controllers
{
    public class UserController : Controller
    {
        //code developed using the tutorial Getting Started with Entity Framework 6 Code First using MVC 5 found at https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/

        private SiteContext db = new SiteContext();

        // GET: User
        public ActionResult Index()
        {
            //only admin can view list of users code developed from https://www.c-sharpcorner.com/UploadFile/225740/introduction-of-session-in-Asp-Net/
            if (Session["UserID"] != null)
            {
                if (Session["Role"].ToString() == "admin")
                {
                    return View(db.Users.ToList());
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //current user and admin can view user details
            if (Session["UserID"] != null)
            {
                if (Convert.ToInt32(Session["UserID"]) == id || Session["Role"].ToString() == "admin")
                {
                    User user = db.Users.Find(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
            }
            return RedirectToAction("Index", "Home");
        }



        [ValidateAntiForgeryToken]
        public JsonResult Register([Bind(Include = "Username,Email,Password")] User user)
        {
            //code taken from https://www.youtube.com/watch?v=dzT0bGUS6Fs
            string result = "Fail";
            var emailTaken = db.Users.Where(x => x.Email == user.Email).SingleOrDefault();
            if (emailTaken != null)
            {
                result = "Email already registered";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            var userTaken = db.Users.Where(x => x.Username == user.Username).SingleOrDefault();
            if (userTaken != null)
            {
                result = "Username already registered";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    user.Role = Role.user;
                    user.UserImage = "/Images/generic-user-icon-19.jpg";
                    db.Users.Add(user);
                    db.SaveChanges();
                    result = "Registration sucessful";

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (RetryLimitExceededException /* dex*/)
            {
                //Log error (uncomment dex variable and add a line here to write to log
                result = "Registration Error. Please try again";
            }
            return Json(result);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //admin and user can only edit accounts developed from https://www.c-sharpcorner.com/UploadFile/225740/introduction-of-session-in-Asp-Net/
            if (Session["UserID"] != null)
            {
                if (Convert.ToInt32(Session["UserID"]) == id || Session["Role"].ToString() == "admin")
                {
                    User user = db.Users.Find(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
            }

            return RedirectToAction("Index", "Home");


        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userToUpdate = db.Users.Find(id);
            if (TryUpdateModel(userToUpdate, "", new string[] { "Username", "Email", "Password" }))
            {
                try
                {
                    db.SaveChanges();
                    return View("Details", userToUpdate);
                }
                catch (RetryLimitExceededException /*dex*/)
                {
                    //Log error (uncomment dex variable and add a line here to write to log
                    ModelState.AddModelError("", "Unable to save changes Try again, and if the problem persists see your system administrator");
                }
            }
            return View(userToUpdate);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            //users and admin can only delete accounts
            if (Session["UserID"] != null)
            {
                if (Convert.ToInt32(Session["UserID"]) == id || Session["Role"].ToString() == "admin")
                {
                    User user = db.Users.Find(id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    return View(user);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException/*dex*/)
            {
                //Log error (uncomment dex variable and add a line here to write to log
                ModelState.AddModelError("", "Unable to save changes Try again, and if the problem persists see your system administrator");
            }
            if (Session["UserID"] != null)
            {
                if (Session["Role"].ToString() == "admin")
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Logout");
        }

        public ActionResult Logout()
        {
            //end session
            Session.Clear();
            Session.Abandon();
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                HttpCookie doomed = new HttpCookie(Request.Cookies[i].Name);
                doomed.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(doomed);
            }
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
