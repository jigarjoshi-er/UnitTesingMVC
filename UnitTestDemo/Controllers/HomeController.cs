using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using UnitTestDemo.Attributes;

namespace UnitTestDemo.Controllers
{
    public class HomeController : Controller
    {


        public HomeController()
        {

        }

        [ClaimsAuthorize("Dashboard", "View")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page!";

            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize()]
        public ActionResult Chat()
        {
            return View();
        }

        public JsonResult GeneratePdf()
        {
            string sourceHtml = Server.MapPath("/Files/Template.html");
            string targetPdf = Server.MapPath("/Files/sample.pdf");

            #region Itext

            //using (var htmlStream = new FileStream(SRC,FileMode.Open))
            //using (var pdfStream = new FileStream(DEST, FileMode.Create))
            //{
            //    try
            //    {
            //        HtmlConverter.ConvertToPdf(htmlStream, pdfStream);
            //    }
            //    finally
            //    {
            //        htmlStream.Close();
            //        pdfStream.Close();
            //    }
            //}

            #endregion

            #region Select.Pdf

            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(System.IO.File.ReadAllText(sourceHtml));
            doc.Save(targetPdf);
            doc.Close();

            #endregion

            return Json("OK");
        }
    }
}