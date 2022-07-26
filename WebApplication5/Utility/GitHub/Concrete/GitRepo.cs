using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApplication5.Utility.GitHub.Concrete
{
    public class GitRepo
    {
        
        public static void CreateRepo(string token, string reponame)
        {
            string quote = "\"";
            string bc = "}";
            string bo = "{";
            string nl = @"\";
            token = token.Replace("bearer ", "");
            // string arg = $"curl -X POST -H {quote}Authorization: token {token.Replace("bearer ", "")}{quote} --data-raw {quote}{bo}    {nl}{quote}name{nl}{quote}: {nl}{quote}{assgname}{nl}{quote}, {nl}{quote}private{nl}{quote}: {nl}{quote}true{nl}{quote}{bc}{quote}  https://api.github.com/user/repos";
            string arg = $"curl -X POST -H {quote}Authorization: token {token}{quote} --data-raw {quote}{bo}    {nl}{quote}name{nl}{quote}: {nl}{quote}{reponame}{nl}{quote}, {nl}{quote}private{nl}{quote}: {nl}{quote}true{nl}{quote}{bc}{quote}  https://api.github.com/user/repos ";

            string deneme = "mkdir ccc";
            //curl -X POST  -H "Authorization: token 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 " -d "{ \""name\"": \""PythonD13B\"",\""private\"": \""true\"" }"   https://api.github.com/user/repos
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            p.StartInfo = info;
            p.Start();
            //token = token.Replace("bearer ", "");
            StreamWriter sw = p.StandardInput;
            sw.WriteLine($@"cd C:\Users\Emrullah\Desktop");
            //sw.WriteLine($@"mkdir gg");
            sw.WriteLine(arg);



        }

        public static void PushTemplate(string token, string reponame, string username, string clsId, string assgnName)
        {

            string quote = "\"";
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            p.StartInfo = info;
            p.Start();
            token = token.Replace("bearer ", "");
            StreamWriter sw = p.StandardInput;
           
           
            
            
             


        }


    }
}