using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Utility.HelperClass
{
    public class SessionAuth
    {
        public static void AddLecturer(string userName, string Mail)
        {
            HttpContext.Current.Session["userName"] = userName;
            HttpContext.Current.Session["Mail"] = Mail;
            HttpContext.Current.Session["userRole"] = "Lecturer";
        }
        public static void AddStudent(string userName, string Mail)
        {

            HttpContext.Current.Session["userName"] = userName;
            HttpContext.Current.Session["Mail"] = Mail;
            HttpContext.Current.Session["userRole"] = "Student";
        }

        public static bool IsLecturer()
        {

            if (HttpContext.Current.Session["userRole"].Equals("Lecturer"))
            {

                return true;
            }

            return false;
        }

        public static bool IsStudent()
        {
            if (HttpContext.Current.Session["userRole"].Equals("Student"))
            {

                return true;
            }
            return false;
        }
        public static bool IsActive()
        {
            if (HttpContext.Current.Session["userRole"] != null)
            {
                return true;

            }
            return false;
        }
        public static string Name()
        {
            return HttpContext.Current.Session["userName"].ToString();
        }
        public static string GetMail()
        {
            return HttpContext.Current.Session["Mail"].ToString();
        }
    }
}
