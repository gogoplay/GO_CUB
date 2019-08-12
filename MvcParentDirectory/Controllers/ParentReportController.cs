using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcParentDirectory.Models;

namespace MvcParentDirectory.Controllers
{
    public class ParentReportController : Controller
    {
        //DbContext 
        private ParentDirectoryDBContext context = new ParentDirectoryDBContext();
        // GET: ParentReport
        public ActionResult Index()
        {

            var parentList = context.ParentDirectories.ToList();
            return View(parentList);
            //return View();
        }
        public ActionResult ExportCustomers()
        {
            List<ParentDirectory> allParentList = new List<ParentDirectory>();
            allParentList = context.ParentDirectories.ToList();


            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CReport"), "CrystalReport1.rpt"));

            rd.SetDataSource(allParentList);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();


            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ParentList.pdf");
        }
    }
}