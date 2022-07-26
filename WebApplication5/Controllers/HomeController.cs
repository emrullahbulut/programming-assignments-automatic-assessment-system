

using RestSharp;
using SharpCompress.Archives.Zip;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Utility.GitHub.Concrete;
using WebApplication5.Utility.HelperClass;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {

        EvaluationSystemEntities2 db = new EvaluationSystemEntities2();
        public FileResult DownloadZipFile(string clsId, string assgnName)
        {
            FileStream stream = new FileStream($@"C:\Users\Emrullah\Proje\{clsId}\{assgnName}.rar",FileMode.Open);
       
            return File(stream, "application/zip",$"{assgnName}.rar");

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            
            
            return View();
        }
        
        public ActionResult Contact()
        {
          

            return View();
        }
       
    }
}