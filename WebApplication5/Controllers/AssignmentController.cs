using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Utility.HelperClass;
using WebApplication5.Utility.GitHub;
using WebApplication5.Utility.GitHub.Concrete;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace WebApplication.Controllers
{
    public class AssignmentController : Controller
    {

        EvaluationSystemEntities2 db = new EvaluationSystemEntities2();
        // GET: Assignment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sonuclar(string assgName,string clsId)
        {

            //CI surecinin ciktilarının veri tabanından okutulup view'a gönderilir.
            var ass = db.Assignment.Where(i => i.Name == assgName).Where(i => i.LectureId == clsId).FirstOrDefault();
            var cc = ass.ExpectationEvaluate.ToList();
            List<String> ls = new List<string>();
            

            //Duzletilmesi gerekli.
            StreamReader reader = new StreamReader(@"C:\Users\Emrullah\Proje\lint.txt");
            string lint = reader.ReadLine();
            
            foreach (var item in cc)
            {
                ls.Add(item.StudentId);
                
            }
            int x = 0;
            foreach (var item in ls)
            {
                x = 0;
                foreach (var item2 in cc)
                {
                    if(item2.StudentId == item)
                    {
                        x += Int32.Parse(item2.Point);
                    }
                }

            }
            if (lint.Equals("Hiçbir hataya rastlanmadı."))
            {
                ViewBag.lint = lint;
                x += Int32.Parse(ass.Expectation.Where(i => i.ExpectedType == "lint").FirstOrDefault().Factor);
               
            }
            else
            {


                ViewBag.lint = lint;
            }
            reader.Close();
        
            ViewBag.list = ls.Distinct();
            ViewBag.item = x.ToString();
            return View(cc);
        }

        [HttpGet]
        public ActionResult Create(String LectureId)
        {
            Object packet = LectureId;
            return View(packet);
        }
        [HttpPost]

        //Ogretmenin beklentilerine gore odev template'i olusurma islemleri.
        public ActionResult Create(string LectureId, string date, string name, string info,
            string cikti, string ciktipuan,
            string komut1, string kc1, string kp1,
            string komut2, string kc2, string kp2,
            string komut3, string kc3, string kp3,
            string lp)
        {
            //Ödev oluşturma
            Assignment assignment = new Assignment();
            string asgnId = "asgn" + IdGenarator.Id();
            assignment.AssignmentId = asgnId;
            assignment.DeliveryDate = DateTime.Now.AddDays(Int32.Parse(date));
            assignment.Info = info;
            assignment.LectureId = LectureId;
            assignment.Name = name;
            db.Assignment.Add(assignment);
            db.SaveChanges();


            //Ödevin beklentilerini kaydetme.
            if (cikti.Length > 0)
            {
                string cp = "sifir";
                if (ciktipuan.Length > 0)
                {
                    cp = ciktipuan;
                }
                Expectation expectation = new Expectation() { AssignmentId = asgnId, ExpectedId = "exp" + IdGenarator.Id(),
                    Factor = cp, ExpectedValue = cikti, InputTypye = "null", InputValue = "null", ExpectedType = "CiktiTesti"
                };
                // CiktiTest deneme = new CiktiTest(cikti);
                db.Expectation.Add(expectation);
                db.SaveChanges();
            }
            if (komut1.Length > 0) {
                Expectation expectation2 = new Expectation() { AssignmentId = asgnId, InputValue = komut1,
                    Factor = kp1, ExpectedId = "expa" + IdGenarator.Id(), ExpectedValue = kc1, ExpectedType= "KomutTesti1"
                };
                db.Expectation.Add(expectation2);
                db.SaveChanges();
            }
            if (komut2.Length > 0)
            {
                Expectation expectation3 = new Expectation()
                {
                    AssignmentId = asgnId,
                    InputValue = komut2,
                    Factor = kp2,
                    ExpectedId = "exps" + IdGenarator.Id(),
                    ExpectedValue = kc2,
                    ExpectedType = "KomutTesti2"
                };
                db.Expectation.Add(expectation3);
                db.SaveChanges();
            }
            if (komut3.Length > 0)
            {
                Expectation expectation5 = new Expectation()
                {
                    AssignmentId = asgnId,
                    InputValue = komut3,
                    Factor = kp3,
                    ExpectedId = "exp" + IdGenarator.Id()+IdGenarator.Id(),
                    ExpectedValue = kc3,
                    ExpectedType = "KomutTesti3"
                };
                db.Expectation.Add(expectation5);
                db.SaveChanges();
            }
            Expectation expectation4 = new Expectation()
            {
                AssignmentId = asgnId,
                InputValue = "lint",
                Factor = lp,
                ExpectedId = "expbb" + IdGenarator.Id(),
                ExpectedValue = null,
                ExpectedType = "lint"
            };
            db.Expectation.Add(expectation4);
            db.SaveChanges();
            TestCreator.DirectoryCreate(LectureId, name, true);
           
            //Beklentilere gore unit Java odevleri icin girdi/Cıktı testi hazırlama.
            TestCreator.UnitTest(LectureId, name, cikti, komut1, kc1, komut2, kc2, komut3, kc3);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Details(string assgnID)
        {
            var assgn = db.Assignment.Where(i => i.AssignmentId == assgnID).FirstOrDefault();
            return View(assgn);
        }

        public ActionResult TakeDelivery(string assignId)
        {
            string assgnName = db.Assignment.Where(i => i.AssignmentId == assignId).FirstOrDefault().Name;
            string lectId = db.Assignment.Where(i => i.AssignmentId == assignId).FirstOrDefault().LectureId;
            string mail = SessionAuth.GetMail();
            var stutokens = db.Student.Where(i => i.Mail == mail).FirstOrDefault().GitHubToken;
            string username = GitWorks.getUserName(stutokens);
            ViewBag.token = stutokens;
            ViewBag.userName = username;
            ViewBag.assgnName = assgnName;
            ViewBag.clsId = lectId;
            return View();
          
        }
        public ActionResult Deneme(string token,  string assgname, string username, string clsıd)
        {
            ViewBag.assgnName = assgname;
            ViewBag.clsId = clsıd;
            ViewBag.user = GitWorks.getUserName(token);
            ViewBag.token = token;
            ViewBag.Harf = "";
            ViewBag.Mesaj = $@"https://{username}:{token.Replace("bearer ","")}@github.com/{username}/{assgname}.git";
            string quote = "\"";
            string bc = "}";
            string bo = "{";
            string nl = @"\";
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            p.StartInfo = info;
            p.Start();
            StreamWriter sw = p.StandardInput;
            string tokenx = token.Replace("bearer", "");
            string tokeny = token.Replace("bearer ", "");
            string deneme = $@"{quote}C:\Program Files\WinRAR\Rar.exe{quote} a -cfg- -ep1 -inul -m5 -r -- {quote}C:\Users\Emrullah\Proje\{clsıd}\{assgname}.rar{quote} {quote}C:\Users\Emrullah\Proje\{clsıd}\{assgname}\{quote}";
            string arg = $"curl -X POST -H {quote}Authorization: token {tokenx}{quote} --data-raw {quote}{bo}    {nl}{quote}name{nl}{quote}: {nl}{quote}{assgname}{nl}{quote}, {nl}{quote}private{nl}{quote}: {nl}{quote}true{nl}{quote}{bc}{quote}  https://api.github.com/user/repos ";
            sw.WriteLine(deneme);
            sw.WriteLine(arg);
            return View();
        }
        public ActionResult Evaluate(string clsId, string assgname)
        {
            var ass = db.Assignment.Where(i=>i.LectureId == clsId).Where(i=>i.Name == assgname).FirstOrDefault();
            string reponame = ass.Name;
            string deneme = "";
            string stuId;
            foreach (var item in db.LectureStudent.Where(i=> i.LectureId == clsId))
            {
                var token = db.Student.Where(i => i.StudentId == item.StudentId).FirstOrDefault().GitHubToken;
                stuId = db.Student.Where(i => i.StudentId == item.StudentId).FirstOrDefault().StudentId;
                var username = GitWorks.getUserName(token);
                GitWorks gw = new GitWorks(username, reponame, token, stuId, clsId, ass.AssignmentId);
                deneme += token + "  " + username +" /"; 
            }
            ViewBag.deneme = deneme;
            return View();
        }

        public ActionResult EvaluateStudents(string assgname, string stuId, string clsId)
        {
            var ass = db.Assignment.Where(i => i.Name == assgname).Where(i => i.LectureId == clsId).FirstOrDefault();
            var student = db.Student.Where(i => i.StudentId == stuId).FirstOrDefault();
            string reponame = ass.Name;
            string token = student.GitHubToken;
            var username = GitWorks.getUserName(token);
            GitWorks gw = new GitWorks(username, reponame, token, stuId, clsId, ass.AssignmentId);
            var cc = db.ExpectationEvaluate.Where(i => i.AssignmentId == ass.AssignmentId).ToList();
            return RedirectToAction("Index", "Home");
        }
     


    }
}