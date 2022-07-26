using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Ionic.Zip;

public class DownloadHelper
{
    HttpContext context = HttpContext.Current;
    int bufferLength = 1024;

    public int BufferLength
    {
        get { return bufferLength; }
        set { bufferLength = value; }
    }

    public DownloadHelper()
    {

    }

    public DownloadHelper(int bufferLength)
    {
        this.bufferLength = bufferLength;
    }

    public void Download(string dosya)
    {
        var dosyaAdi = Path.GetFileName(dosya);

        context.Response.Clear();
        context.Response.ContentType = "application/octet-stream";
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + dosyaAdi);

        byte[] buffer = new Byte[bufferLength];
        int length = 0;
        Stream download = null;
        try
        {
            download = new FileStream(context.Server.MapPath(dosya), FileMode.Open, FileAccess.Read);
            do
            {
                if (context.Response.IsClientConnected)
                {
                    length = download.Read(buffer, 0, bufferLength);
                    context.Response.OutputStream.Write(buffer, 0, length);
                    buffer = new Byte[bufferLength];
                }
                else
                {
                    length = -1;
                }
            }
            while (length > 0);
            context.Response.Flush();
            context.Response.End();
        }
        finally
        {
            if (download != null)
                download.Close();
        }
    }

    public void DownloadZip(string dosya)
    {
        var dosyaAdi = Path.GetFileNameWithoutExtension(dosya);
        var zipKlasor = string.Format("/temp/{0}", Guid.NewGuid().ToString());
        var zipDosyaAdi = string.Format("{0}.zip", dosyaAdi);
        Directory.CreateDirectory(context.Server.MapPath(zipKlasor));
        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(context.Server.MapPath(dosya), "");
            zip.Save(context.Server.MapPath(zipKlasor + "/" + zipDosyaAdi));
        }
        try
        {
            Download(zipKlasor + "/" + zipDosyaAdi);
        }
        finally
        {
            Directory.Delete(context.Server.MapPath(zipKlasor), true);
        }
    }
}