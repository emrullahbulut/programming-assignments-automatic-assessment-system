using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Utility.HelperClass;

namespace WebApplication5.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        EvaluationSystemEntities2 db = new EvaluationSystemEntities2();


        [HttpGet]
        public ActionResult LectureLogin()
        {

            return View();
        }
        [HttpPost]

        public RedirectToRouteResult LectureLogin(string mail, string password)
        {
            if (!SessionAuth.IsActive())
            {

                var Lecturer = db.Lecturer.Where(i => i.Mail == mail).FirstOrDefault();
                bool cond = (Lecturer.Password.Equals(password));
                if (cond)
                {
                    SessionAuth.AddLecturer(Lecturer.Name, Lecturer.Mail);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    return RedirectToAction("LectureLogin");

                }
            }
            return RedirectToAction("Index", "Home");


        }
        [HttpGet]
        public ActionResult StudentLogin()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult StudentLogin(string mail, string password)
        {


            if (!SessionAuth.IsActive())
            {

                var Student = db.Student.Where(i => i.Mail == mail).FirstOrDefault();
                bool cond = (Student.Password.Equals(password));
                if (cond)
                {
                    SessionAuth.AddStudent(Student.Name, Student.Mail);
                    return RedirectToAction("Index", "Home");

                }
                else
                {

                    return View("LoginFail");

                }

            }
            else
            {
                RedirectToAction("Index", "Home");
                return View();
            }
        }

        [HttpGet]
        public ActionResult LecturerSignUp()
        {


            return View();
        }
        [HttpPost]
        public ActionResult LecturerSignUp(string Name, string Surname, string Mail, string Password)
        {

            try
            {
                string id = "lect" + IdGenarator.Id();
                Lecturer lecturer = new Lecturer { Lecturerid = id, Name = Name, Mail = Mail, Surname = Surname, Password = Password, GitHubToken = "null" };
                db.Lecturer.Add(lecturer);
                db.SaveChanges();

                SessionAuth.AddLecturer(Name, Mail);
                return RedirectToAction("Index","Home");

            }
            catch (Exception)
            {

                return View("signupfail");
            }

        }

        public ActionResult StudentSignUp()
        {

            return View();
        }
        [HttpPost]
        public ActionResult StudentSignUp(string Name, string Surname, string Mail, string Password)
        {
            try
            {
                string id = "stu" + IdGenarator.Id();
                Student student = new Student { StudentId = id, Name = Name, Mail = Mail, Surname = Surname, Password = Password, GitHubToken = "null" };
                db.Student.Add(student);
                db.SaveChanges();
                SessionAuth.AddStudent(Name, Mail);
                return RedirectToAction("Index", "Home");


            }
            catch (Exception)
            {

                return View("signupfail");
            }


        }
    }
}