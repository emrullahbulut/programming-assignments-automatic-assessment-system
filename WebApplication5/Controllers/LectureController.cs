using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Utility.HelperClass;

namespace WebApplication5.Controllers
{
    public class LectureController : Controller
    {
        // GET: Lecture
        EvaluationSystemEntities2 db = new EvaluationSystemEntities2();


        public ActionResult MyLectures()
        {
            if (Utility.HelperClass.SessionAuth.IsActive())
            {
                String mail = Utility.HelperClass.SessionAuth.GetMail();
                String lectId = db.Lecturer.Where(i => i.Mail == mail).FirstOrDefault().Lecturerid;
                List<Lecture> lecturelist = new List<Lecture>();
                foreach (var item in db.Lecturer_Lecuture)
                {
                    if (item.LecturerId == lectId)
                    {
                        foreach (var item2 in db.Lecture)
                        {
                            if (item.LectureId == item2.LecutureId)
                            {
                                lecturelist.Add(item2);
                            }
                        }
                    }
                }
                return View(lecturelist);

            }
            else
            {
                return RedirectToAction("LectureLogin", "Login");
            }


        }


        public ActionResult Info(string lectureId)
        {
            Lecture lecture = db.Lecture.Where(i => i.LecutureId == lectureId).FirstOrDefault();
            return View(lecture);
        }


        public ActionResult InfoS(string lectureId)
        {
            Lecture lecture = db.Lecture.Where(i => i.LecutureId == lectureId).FirstOrDefault();
            return View(lecture);
        }
        
        [HttpGet]
        public ActionResult CreateLecture()
        {
            if (SessionAuth.IsLecturer())
            {
                string mail = SessionAuth.GetMail();
                String ogrId = db.Lecturer.Where(i => i.Mail == mail).FirstOrDefault().Lecturerid;
                ViewBag.id = ogrId;
                return View();
            }
            else
            {
                return RedirectToAction("LectureLogin", "Login");

            }
        }

        [HttpPost]
        public ActionResult CreateLecture(string name, string info, string lectId)
        {
            Lecture lecture = new Lecture();
            string lıd = "lect" + IdGenarator.Id();
            lecture.LecutureId = lıd;
            lecture.Name = name;
            lecture.Info = info;
            lecture.ActivationCode = IdGenarator.Id();
            db.Lecture.Add(lecture);
            db.SaveChanges();
            Lecturer_Lecuture ll = new Lecturer_Lecuture();
            ll.LectureId = lıd;
            ll.LecturerId = lectId;
            ll.Lecture_LecturerId = "ll" + IdGenarator.Id();
            db.Lecturer_Lecuture.Add(ll);
            db.SaveChanges();
            return RedirectToAction("MyLectures", "Lecture");

           // return View();
        }
        [HttpGet]
        public ActionResult JoinTheClass()
        {
            if (SessionAuth.IsStudent())
            {
                string mail = SessionAuth.GetMail();
                string stuId = db.Student.Where(i => i.Mail == mail).FirstOrDefault().StudentId;
                ViewBag.stuId = stuId;
            }
            else
            {
                return RedirectToAction("StudentLogin", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult JoinTheClass(string ActCode, string stuId)
        {

            try
            {
                var x = db.Lecture.Where(i => i.ActivationCode == ActCode).FirstOrDefault().LecutureId == null;
               
             
            }
            catch (Exception)
            {

                return RedirectToAction("Hata", "Lecture");

            }
            
            string lectureId = db.Lecture.Where(i => i.ActivationCode == ActCode).FirstOrDefault().LecutureId;
            
            LectureStudent ls = new LectureStudent();
            ls.StudentId = stuId;
            ls.LectureId = lectureId;
            ls.LectureStudentId = "ls" + IdGenarator.Id();
            db.LectureStudent.Add(ls);
            db.SaveChanges();
            return RedirectToAction("Lectures", "Lecture");

        }

        public ActionResult Lectures()
        {
            if (Utility.HelperClass.SessionAuth.IsActive())
            {
                String mail = Utility.HelperClass.SessionAuth.GetMail();
                String stuId = db.Student.Where(i => i.Mail == mail).FirstOrDefault().StudentId;
                List<Lecture> lecturelist = new List<Lecture>();
                foreach (var item in db.LectureStudent)
                {
                    if (item.StudentId == stuId)
                    {
                        foreach (var item2 in db.Lecture)
                        {
                            if (item.LectureId == item2.LecutureId)
                            {
                                lecturelist.Add(item2);
                            }
                        }
                    }
                }
                return View(lecturelist);

            }
            else
            {
                return RedirectToAction("StudentLogin", "Login");
            }

            
        }

        public ActionResult Hata()
        {
            return View();
        }


    }
}