using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Utility.HelperClass;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class GitAuthorizationController : Controller
    {
        // GET: GitAuthorization

        EvaluationSystemEntities2 db = new EvaluationSystemEntities2();
        
        /* 
         Ogrencilerin GitHubdaki repositorylerine erşiim ve CI(Sürekli entagrasyon)
         sürecini başlatmak ve bu surecin sonuclarını almak icin Github yetkilendirme islemi.)
         Bu islem sonucunda Github token olusturulup veri tabanına kaydedilmektedir.
         
         */
        public ActionResult Index(String code)
        {
            WebClient client = new WebClient();
            string postUrl = "https://github.com/login/oauth/access_token";
            var gelenYanit = client.UploadValues(postUrl, new NameValueCollection() { { "client_id", "3c32ff0b36a3be983b3e" }, { "client_secret", "c147da3c7bd8d8f79c46acf2266d2dff933339d3" }, { "code", code } });
            string result = System.Text.Encoding.UTF8.GetString(gelenYanit);
            result = result.Substring(13, 40);
            string mail = SessionAuth.GetMail();
            if (SessionAuth.IsStudent())
            {

                Student student = db.Student.Where(i => i.Mail == mail).FirstOrDefault();
                student.GitHubToken = "bearer " + result;
            }
            if (SessionAuth.IsLecturer())
            {
                Lecturer lecturer = db.Lecturer.Where(i => i.Mail == mail).FirstOrDefault();
                lecturer.GitHubToken = "bearer " + result;
            }
            db.SaveChanges();

            return View();
        }


        public ActionResult Etkinlestir()
        {
            return View();
        }
    }
}