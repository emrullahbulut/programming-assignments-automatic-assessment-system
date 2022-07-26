using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication5.Utility.GitHub.Abstarct
{
    interface IGitWorks
    {
        void Trigger(string username, string reponame);
        string FindActionId(string username, string reponame);
        void DownloadLogs(string username, string reponame, string id);
        bool DeleteLastAction();

    }
}