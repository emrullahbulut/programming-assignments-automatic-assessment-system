using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading;
using WebApplication5.Models;
using WebApplication5.Utility.Entities;
using WebApplication5.Utility.HelperClass;

namespace WebApplication5.Utility.GitHub.Concrete
{   //
    public class GitWorks
    {
        EvaluationSystemEntities2 db = new EvaluationSystemEntities2();
        public GitWorks()
        {

        }
        public GitWorks(string username, string reponame, string token, string stuId, string clsId,string assId)
        {

            //fixme
            this.Trigger(username, reponame, token);
            Thread.Sleep(1000);
          
            Thread.Sleep(29000);

            this.DownloadLogs(username, reponame, this.FindActionId(username, reponame, token), token,stuId, assId);
           
        }
        public bool DeleteLastAction()
        {
            throw new NotImplementedException();
        }

        public string  DownloadLogs(string username, string reponame, string id,string token, string stuId,string asgnId)
        {
            if(id == "hata")
            {
                return "";
            }
            string random = DateTime.Now.Second.ToString();
            string path = $"C:/Users/Emrullah/Documents/{random}/ogrId";
            string geturl = $"https://api.github.com/repos/{username}/{reponame}/actions/runs/{id}/logs";
            var client = new RestClient(geturl);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", token);
            request.AddHeader("Accept", "application/vnd.github.v3+json");
            var x = client.DownloadStreamAsync(request).Result;
            ZipArchive zipArchive = new ZipArchive(x);
            var yy = zipArchive.GetEntry("build/5_test.txt");
            StreamReader reader = new StreamReader(yy.Open());
             string studentId = stuId;
            var beklentiler = db.Expectation.Where(i => i.AssignmentId == asgnId).ToList();
            string xx = beklentiler.Count().ToString();
           
            //StreamWriter writer = new StreamWriter(@"C:\Users\Emrullah\Desktop\sonucc.txt");
            
            
            foreach (var item in db.ExpectationEvaluate)
            {
                if (item.AssignmentId == asgnId && item.StudentId == studentId)
                {
                    db.ExpectationEvaluate.Remove(item);
                   // writer.WriteLine("remove");
                }
            }
           
                while (reader.Peek() >= 0)
                {            
                    
                    
                    string metin = reader.ReadLine();
                    if (metin.Contains("CiktiTesti"))
                    {
                     var xxx = db.Assignment.Where(i => i.AssignmentId == asgnId).FirstOrDefault().Expectation.Where(i=> i.ExpectedType == "CiktiTesti").FirstOrDefault();

                        // xx = "girdim";
                        ExpectationEvaluate expectationEvaluate = new ExpectationEvaluate();
                        expectationEvaluate.AssignmentId = asgnId;
                        expectationEvaluate.ExpectationEvaluateId = "aa" + IdGenarator.Id();
                        expectationEvaluate.ExpectationId = xxx.ExpectedId;
                        expectationEvaluate.StudentId = studentId;

                        if (metin.Contains("✔"))
                        {
                            expectationEvaluate.Point = xxx.Factor;
                            expectationEvaluate.Log = "test basarili";
                            

                            

                        }
                        else
                        {
                            xx += "basarisiizm";
                            expectationEvaluate.Point = "0";
                            expectationEvaluate.Log = "test basarisiz.";
                            

                        }
                    db.ExpectationEvaluate.Add(expectationEvaluate);
                    try
                    {
                        db.SaveChanges();

                    }
                    catch (Exception)
                    {

                        continue;
                    }


                }
                if (metin.Contains("KomutTesti1"))
                {
                    var xxx = db.Assignment.Where(i => i.AssignmentId == asgnId).FirstOrDefault().Expectation.Where(i => i.ExpectedType == "KomutTesti1").FirstOrDefault();

                    // xx = "girdim";
                    ExpectationEvaluate expectationEvaluate = new ExpectationEvaluate();
                    expectationEvaluate.AssignmentId = asgnId;
                    expectationEvaluate.ExpectationEvaluateId = "bb" + IdGenarator.Id();
                    expectationEvaluate.ExpectationId = xxx.ExpectedId;
                    expectationEvaluate.StudentId = studentId;

                    if (metin.Contains("✔"))
                    {
                        expectationEvaluate.Point = xxx.Factor;
                        expectationEvaluate.Log = "test basarili";
                        db.ExpectationEvaluate.Add(expectationEvaluate);
                        try
                        {
                            db.SaveChanges();

                        }
                        catch (Exception)
                        {

                            continue;
                        }



                    }
                    else
                    {
                        xx += "basarisiizm";
                        expectationEvaluate.Point = "0";
                        db.ExpectationEvaluate.Add(expectationEvaluate);
                        expectationEvaluate.Log = "test basarisiz.";
                        try
                        {
                            db.SaveChanges();

                        }
                        catch (Exception)
                        {

                            continue;
                        }


                    }


                }
                if (metin.Contains("KomutTesti2"))
                {
                    var xxx = db.Assignment.Where(i => i.AssignmentId == asgnId).FirstOrDefault().Expectation.Where(i => i.ExpectedType == "KomutTesti2").FirstOrDefault();

                    // xx = "girdim";
                    ExpectationEvaluate expectationEvaluate = new ExpectationEvaluate();
                    expectationEvaluate.AssignmentId = asgnId;
                    expectationEvaluate.ExpectationEvaluateId = "cc" + IdGenarator.Id();
                    expectationEvaluate.ExpectationId = xxx.ExpectedId;
                    expectationEvaluate.StudentId = studentId;

                    if (metin.Contains("✔"))
                    {
                        expectationEvaluate.Point = xxx.Factor;
                        expectationEvaluate.Log = "test basarili";
                        db.ExpectationEvaluate.Add(expectationEvaluate);

                        try
                        {
                            db.SaveChanges();

                        }
                        catch (Exception)
                        {

                            continue;
                        }
                    



                    }
                    else
                    {
                        xx += "basarisiizm";
                        expectationEvaluate.Point = "0";
                        db.ExpectationEvaluate.Add(expectationEvaluate);
                        expectationEvaluate.Log = "test basarisiz.";
                        try
                        {
                            db.SaveChanges();

                        }
                        catch (Exception)
                        {

                            continue;
                        }


                    }


                }
                if (metin.Contains("KomutTesti3"))
                {
                    var xxx = db.Assignment.Where(i => i.AssignmentId == asgnId).FirstOrDefault().Expectation.Where(i => i.ExpectedType == "KomutTesti3").FirstOrDefault();

                    // xx = "girdim";
                    ExpectationEvaluate expectationEvaluate = new ExpectationEvaluate();
                    expectationEvaluate.AssignmentId = asgnId;
                    expectationEvaluate.ExpectationEvaluateId = "dd" + IdGenarator.Id();
                    expectationEvaluate.ExpectationId = xxx.ExpectedId;
                    expectationEvaluate.StudentId = studentId;

                    if (metin.Contains("✔"))
                    {
                        expectationEvaluate.Point = xxx.Factor;
                        expectationEvaluate.Log = "test basarili";
                        db.ExpectationEvaluate.Add(expectationEvaluate);
                        try
                        {
                            db.SaveChanges();

                        }
                        catch (Exception)
                        {

                            continue;
                        }



                    }
                    else
                    {
                        xx += "basarisiizm";
                        expectationEvaluate.Point = "0";
                        db.ExpectationEvaluate.Add(expectationEvaluate);
                        expectationEvaluate.Log = "test basarisiz.";
                        try
                        {
                            db.SaveChanges();

                        }
                        catch (Exception)
                        {

                            continue;
                        }


                    }


                }


            }




           

            var yyz = zipArchive.GetEntry("lint/4_lint.txt");
            StreamReader readerr = new StreamReader(yyz.Open());
            var xxxy = db.Assignment.Where(i => i.AssignmentId == asgnId).FirstOrDefault().Expectation.Where(i => i.ExpectedType == "lint").FirstOrDefault();
            ExpectationEvaluate ee = new ExpectationEvaluate();
            ee.ExpectationId = xxxy.ExpectedId;
            ee.ExpectationEvaluateId = "eeeasd" + IdGenarator.Id();
            ee.StudentId = studentId;
            ee.AssignmentId = asgnId;
            int lint = 0;
           
            string logs = "";
            StreamWriter wr = new StreamWriter(@"C:\Users\Emrullah\Proje\lint.txt");

            while (readerr.Peek() >= 0)
            {
                
                
                
                string metinx = readerr.ReadLine();
                if (metinx.Contains("Main.java:"))
                {
                  
                    if(metinx.Contains("Main.java:0") || metinx.Contains("Main.java:1:") || metinx.Contains("Main.java:2:"))
                    {

                    }
                    else
                    {
                        lint++;
                        logs += " " + metinx.Substring(70) + " ";

                    }
                   
                }
                
            }

            if(lint == 0)
            {
               // ee.Log = "Hiçbir hataya rastlanmadı.";
                logs += "Hiçbir hataya rastlanmadı.";
            }
            else
            {
               ee.Log = $"Bulunan hata sayisi {lint} {logs}";
               
            }
            wr.WriteLine($"Bulunan hata sayisi {lint} Hatalar  : {logs}");
            wr.WriteLine(logs);
 
            wr.Close();
            return "";


        }
        public static string getUserName(string token) {

            string url = "https://api.github.com/user";
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", token);
            request.AddHeader("Accept", "application/vnd.github.v3+json");
            var data = client.GetAsync(request).Result.Content;
            var details = JObject.Parse(data);
            return details["login"].ToString();

           
        }
        public string FindActionId(string username, string reponame, string token)
        {

            //string username = "emrullahbulut";
            //string reponame = "ruby-hw-template";
            //string token = "bearer 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";
            string url = "https://api.github.com/repos/" + username + "/" + reponame + "/actions/runs";
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest();
            request.AddHeader("Authorization", token);
            request.AddHeader("Accept", "application/vnd.github.v3+json");
            var data = client.GetAsync(request).Result.Content;
            var details = JObject.Parse(data);
            try
            {
                return details["workflow_runs"][0]["id"].ToString();
                
            }
            catch (Exception)
            {
                return "hata";
                
            }


        }

        public void Trigger(string username, string reponame, string token)
        {
            string url = "https://api.github.com/repos/"+ $"{username}/{reponame}" +"/dispatches";
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest();
            request.Method = Method.Post;
            var data = new EventData { event_type = "run-ping" };
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(EventData));
            request.AddHeader("Authorization", token);
            request.AddHeader("Accept", "application/vnd.github.v3+json");
            
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, data);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);
            string json = sr.ReadToEnd();
            //request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.AddStringBody(json, DataFormat.Json);
            var response = client.ExecuteAsync(request);
            msObj.Close();
            sr.Close();

        }

        public  void TestDegerlendir( string asgnId, string stuId)
        {
          

        }

    }
}