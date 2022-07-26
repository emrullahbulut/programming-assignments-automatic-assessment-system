using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication5.Utility.HelperClass
{
    public class TestCreator
    {
        public static string commandToParameter(string command)
        {
           
            command = command.Replace(" ", " ");
            string quote = "\"";
            string bc = "}";
            string[] veri2 = command.Split(' ');
            string cikti = "{";
            
            for (int i = 2; i < veri2.Length - 1; i++)
            {
                cikti += $"{quote}{veri2[i]}{quote}, ";
            }
            cikti += $"{quote}{veri2[veri2.Length - 1]}{quote}{bc}";

            string asilCikti = $"String[] commands = {cikti};";
            return asilCikti;
        }
        public static string outToAssert(string veri)
        {
            string quote = "\"";
            string backquote = @"\n";
            string[] veri2 = veri.Split(new string[] { $"{backquote}" }, StringSplitOptions.None);
            String cikti = "";
            cikti += $"{quote}{veri2[0]}{quote}";
            for (int i = 1; i < veri2.Length; i++)
            {
                if (veri2[i].Length > 0)
                {
                    cikti += " + System.lineSeparator()" + $"+ {quote}{veri2[i]}{quote}";
                }
            }
            cikti += " + System.lineSeparator()";
            return cikti;
        }
        public static void DirectoryCreate(string clsId,string asName,bool isLint)
        {
            String direct = $@"C:\Users\Emrullah\Proje\{clsId}\{asName}";
            String direct2 = $@"C:\Users\Emrullah\Proje\{clsId}\{asName}\.github\workflows";
            String direct3 = $@"C:\Users\Emrullah\Proje\{clsId}\{asName}\.github\workflows";

            if (!Directory.Exists(direct))
            {
                Directory.CreateDirectory(direct);
                Directory.CreateDirectory(direct2);
            }
            var fileName = "junit-platform-console-standalone-1.8.2.jar";
            var source = Path.Combine(@"C:\Users\Emrullah\Desktop", fileName);
            var destination = Path.Combine(direct, fileName);
            System.IO.File.Copy(source, destination);
            if (isLint)
            {
                var fileName2 = "checkstyle-5.5-all.jar";
                var source2 = Path.Combine(@"C:\Users\Emrullah\Desktop", fileName2);
                var destination2 = Path.Combine(direct, fileName2);
                System.IO.File.Copy(source2, destination2);
                var fileName3 = "main.yml";
                var source3 = Path.Combine(@"C:\Users\Emrullah\Desktop\lint", fileName3);
                var destination3 = Path.Combine(direct3, fileName3);
                System.IO.File.Copy(source3, destination3);
                var fileName4 = "sun_checks.xml";
                var source4 = Path.Combine(@"C:\Users\Emrullah\Desktop\lint", fileName4);
                var destination4 = Path.Combine(direct, fileName4);
                System.IO.File.Copy(source4, destination4);
                var fileName5 = "Main.java";
                var source5 = Path.Combine(@"C:\Users\Emrullah\Desktop", fileName5);
                var destination5 = Path.Combine(direct, fileName5);
                System.IO.File.Copy(source5, destination5);
            }
            else
            {
                var fileName3 = "main.yml";
                var source3 = Path.Combine(@"C:\Users\Emrullah\Desktop", fileName3);
                var destination3 = Path.Combine(direct3, fileName3);
                System.IO.File.Copy(source3, destination3);
            }

           
           


        }
        public static void UnitTest(string clsId, string asName, string cikti,string komut1, string kc1, string komut2, string kc2, string komut3, string kc3)
        {
            
            
            string backquote = @"\n";
            string beklenti1 = $"Merhaba Dünya{backquote}Benim Adım Murat{backquote}";
            beklenti1.Replace(backquote, "+  System.lineSeparator()");
            string space = "        ";
            string quote = "\"";
            string path = $@"C:\Users\Emrullah\Proje\{clsId}\{asName}\MainTest.java";
            string importText = "import org.junit.Before;\nimport org.junit.After;\nimport org.junit.Test;\nimport static org.junit.Assert.*;\nimport java.io.*;\n";
            string ciktiTesti = "public class MainTest{";
            string ciktiTesti1=     "    @Test\n    public void CiktiTesti() throws Exception {\n        ByteArrayOutputStream outContent = new ByteArrayOutputStream();\n        System.setOut(new PrintStream(outContent));\n        Main.main(null);";
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(importText);
            writer.WriteLine(ciktiTesti);
            if (cikti.Length>1) {
                string ciktiTesti2 = $@"{space}assertEquals({outToAssert(cikti)}, outContent.toString());";
                string ciktiTesti3 = "    }\n";
                writer.WriteLine(ciktiTesti1);
                writer.WriteLine(ciktiTesti2);
                writer.WriteLine(ciktiTesti3);
            }
            if (komut1.Length > 0)
            {
                string komuttest1 = "    @Test\n    public void KomutTesti1() throws Exception {\n        ByteArrayOutputStream outContent = new ByteArrayOutputStream();\n        System.setOut(new PrintStream(outContent));\n";
                string komuttest2 = $@"{space}{commandToParameter(komut1)}";
                string komuttest3 = "        Main.main(commands);";
                string komuttest4 = $@"{space}assertEquals({outToAssert(kc1)}, outContent.toString());";
                writer.WriteLine(komuttest1);
                writer.WriteLine(komuttest2);
                writer.WriteLine(komuttest3);
                writer.WriteLine(komuttest4);
                writer.WriteLine("    }");
            }
            if (komut2.Length > 0)
            {
                string komuttest1 = "    @Test\n    public void KomutTesti2() throws Exception {\n        ByteArrayOutputStream outContent = new ByteArrayOutputStream();\n        System.setOut(new PrintStream(outContent));\n";
                string komuttest2 = $@"{space}{commandToParameter(komut2)}";
                string komuttest3 = "        Main.main(commands);";
                string komuttest4 = $@"{space}assertEquals({outToAssert(kc2)}, outContent.toString());";
                writer.WriteLine(komuttest1);
                writer.WriteLine(komuttest2);
                writer.WriteLine(komuttest3);
                writer.WriteLine(komuttest4);
                writer.WriteLine("    }");
            }
            if (komut3.Length > 0)
            {
                string komuttest1 = "    @Test\n    public void KomutTesti3() throws Exception {\n        ByteArrayOutputStream outContent = new ByteArrayOutputStream();\n        System.setOut(new PrintStream(outContent));\n";
                string komuttest2 = $@"{space}{commandToParameter(komut3)}";
                string komuttest3 = "        Main.main(commands);";
                string komuttest4 = $@"{space}assertEquals({outToAssert(kc3)}, outContent.toString());";
                writer.WriteLine(komuttest1);
                writer.WriteLine(komuttest2);
                writer.WriteLine(komuttest3);
                writer.WriteLine(komuttest4);
                writer.WriteLine("    }");
            }




            writer.WriteLine("}");
            writer.Close();


        }




    }
}