using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Coursework.DAL;
using Coursework.Models;
using PagedList;

namespace Coursework.Controllers
{
    public class CauseController : Controller
    {
        //code developed using the tutorial Getting Started with Entity Framework 6 Code First using MVC 5 found at https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/

        private SiteContext db = new SiteContext();

        // GET: Cause
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? selectedCategory)
        {
            //paging 
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            //Create category sort list
            // code developed from https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
            var categories = db.Categories.OrderBy(q => q.Name).ToList();
            ViewBag.selectedCategory = new SelectList(categories, "CategoryID", "Name", selectedCategory);
            int categoryID = selectedCategory.GetValueOrDefault();

            //create sort order list
            // code developed from https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
            ViewBag.DateSortParam = sortOrder == "Newest" ? "Oldest" : "Newest";
            ViewBag.SignSortParam = sortOrder == "Most Signs" ? "Least Signs" : "Most Signs";

            //search function
            // code developed from https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var causes = db.Causes.Include(c => c.Category);
            if (selectedCategory != null)
            {
                IQueryable<Cause> causes1 = db.Causes.Where(c => !selectedCategory.HasValue || c.CategoryID == categoryID).OrderBy(d => d.CauseID).Include(d => d.Category);
                var sql = causes1.ToString();
                return View(causes1.ToPagedList(pageNumber, pageSize));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                causes = causes.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Most Signs":
                    causes = causes.OrderByDescending(s => s.Signatures.Count);
                    break;
                case "Least Signs":
                    causes = causes.OrderBy(s => s.Signatures.Count);
                    break;
                case "Z-A":
                    causes = causes.OrderByDescending(s => s.Title);
                    break;
                case "Oldest":
                    causes = causes.OrderBy(s => s.createdOn);
                    break;
                case "Newest":
                    causes = causes.OrderByDescending(s => s.createdOn);
                    break;
                default: //Title ascending
                    causes = causes.OrderBy(s => s.Title);
                    break;
            }

            return View(causes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Cause/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Code take from https://stackoverflow.com/questions/41748352/the-model-item-passed-into-the-dictionary-is-of-type-system-data-entity-dynami
            Cause cause = db.Causes.Include("Category").Single(i => i.CauseID == id);
            if (cause == null)
            {
                return HttpNotFound();
            }
            if (Session["UserID"] != null)
            {
                if (cause.Signatures.Any(s => s.UserID == Convert.ToInt32(Session["UserID"].ToString())))
                {
                    ViewBag.signedAlready = "signed";
                }
            }

            if (cause == null)
            {
                return HttpNotFound();
            }
            return View(cause);
        }

        // GET: Cause/Create
        public ActionResult Create()
        {
            if (Session["UserID"] != null)
            {
                PopulateCategoryList();
                return View();
            }
            return RedirectToAction("Index");
        }

        // POST: Cause/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,CategoryID,Target")] Cause cause)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cause.createdOn = DateTime.Now;
                    cause.createdBy = Convert.ToString(Session["Username"]);
                    cause.UserID = Convert.ToInt32(Session["UserID"]);
                    db.Causes.Add(cause);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (RetryLimitExceededException /*dex*/)
            {
                //log error iuncommenmt dex
                ModelState.AddModelError("", "Unable to save changes try again or cantact adminstrator");
            }
            PopulateCategoryList(cause.CategoryID);

            return View(cause);
        }

        // GET: Cause/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cause cause = db.Causes.Find(id);

            if (cause == null)
            {
                return HttpNotFound();
            }
            //only admin and creator of cause can edit cause
            if (Session["UserID"] != null)
            {
                if (Convert.ToInt32(Session["UserID"]) == cause.UserID || Session["Role"].ToString() == "admin")
                {
                    PopulateCategoryList(cause.CategoryID);
                    return View(cause);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Cause/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, Cause cause)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var causeToUpdate = db.Causes.Find(id);
            if (TryUpdateModel(causeToUpdate, "", new string[] { "Title", "Description", "Target", "CategoryID" }))
            {
                try
                {

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /*dex*/)
                {
                    //log error iuncommenmt dex
                    ModelState.AddModelError("", "Unable to save changes try again or cantact adminstrator");
                }
            }
            PopulateCategoryList(causeToUpdate.CategoryID);
            return View(causeToUpdate);
        }

        private void PopulateCategoryList(object SelectedDepartment = null)
        {
            var categoryQuery = from c in db.Categories
                                orderby c.Name
                                select c;
            ViewBag.CategoryID = new SelectList(categoryQuery, "CategoryID", "Name", SelectedDepartment);

        }



        // GET: Cause/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cause cause = db.Causes.Include("Category").Single(i => i.CauseID == id);
            if (Session["UserID"] != null)
            {
                if (Convert.ToInt32(Session["UserID"]) == cause.UserID || Session["Role"].ToString() == "admin")
                {
                    if (cause == null)
                    {
                        return HttpNotFound();
                    }
                    return View(cause);
                }
            }
             
            return RedirectToAction("Index");
              
        }

        // POST: Cause/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cause cause = db.Causes.Find(id);
            db.Causes.Remove(cause);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sign(int id, Signature signature)
        {
            if (Session["UserID"] != null)
            {
                Cause cause = db.Causes.Find(id);
                var userid = Convert.ToInt32(Session["UserID"]);
                signature.CauseID = cause.CauseID;
                signature.UserID = db.Users.Single(c => c.ID == userid).ID;
                signature.signedOn = DateTime.Now;
            }
            db.Signatures.Add(signature);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult signTable(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            if (cause.Signatures.Any(s => s.UserID == Convert.ToInt32(Session["UserID"].ToString())))
            {
                ViewBag.signedAlready = "signed";
            }
            return PartialView("_Sign", cause);
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
