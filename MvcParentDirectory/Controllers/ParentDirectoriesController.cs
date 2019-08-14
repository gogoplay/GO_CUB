using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcParentDirectory.Models;

using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace MvcParentDirectory.Controllers
{
    public class ParentDirectoriesController : Controller
    {
        private ParentDirectoryDBContext db = new ParentDirectoryDBContext();

        public static class GlobalVar
        {
            public static string g_searchString = string.Empty;
            public static string g_searchAddress = string.Empty;
            public static List<ParentDirectory> g_getParentList = new List<ParentDirectory>();
        }

            //test
            // GET: ParentDirectories
            //   public actionresult index()
            //{
            //    return view(db.parentdirectories.tolist());
            //}

            // GET:ParentDirectories by searchString
        public ActionResult Index(string searchString, string searchAddress)
        {
  
            //System.Diagnostics.Debug.WriteLine("==============Index 1===========GsearchString=" + GlobalVar.g_searchString);
            //System.Diagnostics.Debug.WriteLine("==============Index 3===========GsearchAddress=" + GlobalVar.g_searchAddress);

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
  
            GlobalVar.g_getParentList = parentDirectories.ToList();//畫面結果存入全域變數g_getParentList

            return View(parentDirectories);
        }

        public ActionResult ExportCustomers()
        {

            //System.Diagnostics.Debug.WriteLine("==============ExportCustomers start============");

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CReport"), "ParentDirectoryReport.rpt"));

            rd.SetDataSource(GlobalVar.g_getParentList);//使用全域變數g_getParentList產出PDF檔

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ParentList.pdf");
        }


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
