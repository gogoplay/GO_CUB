using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcParentDirectory.Models;

namespace MvcParentDirectory.Controllers
{
    public class ParentDirectoriesController : Controller
    {
        private ParentDirectoryDBContext db = new ParentDirectoryDBContext();

        // GET: ParentDirectories
        //   public actionresult index()
        //{
        //    return view(db.parentdirectories.tolist());
        //}

        // GET:ParentDirectories by searchString
        public ActionResult Index(string searchString, string searchAddress)
        {
            var parentDirectories = from m in db.ParentDirectories
                                    select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                parentDirectories = parentDirectories.Where(s => s.Name.Contains(searchString));
            }


            if (!String.IsNullOrEmpty(searchAddress))
            {
                parentDirectories = parentDirectories.Where(x => x.Address.Contains(searchAddress));
            }

            return View(parentDirectories);
        }

        // GET:ParentDirectories by id
        //public ActionResult Index(string id)
        //{
        //    string searchString = id;
        //    var parentDirectories = from m in db.ParentDirectories
        //                            select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        parentDirectories = parentDirectories.Where(s => s.Name.Contains(searchString));
        //    }

        //    return View(parentDirectories);
        //}


        // GET: ParentDirectories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentDirectory parentDirectory = db.ParentDirectories.Find(id);
            if (parentDirectory == null)
            {
                return HttpNotFound();
            }
            return View(parentDirectory);
        }

        // GET: ParentDirectories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParentDirectories/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,Email,PhoneNum,CreateDate")] ParentDirectory parentDirectory)
        {
            if (ModelState.IsValid)
            {
                db.ParentDirectories.Add(parentDirectory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parentDirectory);
        }

        // GET: ParentDirectories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentDirectory parentDirectory = db.ParentDirectories.Find(id);
            if (parentDirectory == null)
            {
                return HttpNotFound();
            }
            return View(parentDirectory);
        }

        // POST: ParentDirectories/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,Email,PhoneNum,CreateDate")] ParentDirectory parentDirectory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parentDirectory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parentDirectory);
        }

        // GET: ParentDirectories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentDirectory parentDirectory = db.ParentDirectories.Find(id);
            if (parentDirectory == null)
            {
                return HttpNotFound();
            }
            return View(parentDirectory);
        }

        // POST: ParentDirectories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParentDirectory parentDirectory = db.ParentDirectories.Find(id);
            db.ParentDirectories.Remove(parentDirectory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
