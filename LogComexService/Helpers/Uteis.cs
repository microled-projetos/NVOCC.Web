using System;
using System.IO;

namespace LogComexService.Helpers
{
    public class Uteis
    {
        public void salvaLog(string Dados, string Log)
        {
            var filename = @"C:\TrackingBl\log\";

            bool exists = System.IO.Directory.Exists(filename);

            if (!exists)
                System.IO.Directory.CreateDirectory(filename);

            var sw = new System.IO.StreamWriter(filename + "log_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".txt", true);            

            var tFiles = Directory.EnumerateFiles(filename, "*.txt");

            foreach (string fileCurrent in tFiles)
            {
                FileInfo file = new FileInfo(fileCurrent);

                int dateCreated = (int)DateTime.Now.Subtract(file.CreationTime).TotalHours;

                if (dateCreated >= 72)
                    file.Delete();
            }

            sw.WriteLine("Log Tracking BL em  " + DateTime.Now.ToString() + "\n\n"); 
            sw.WriteLine("Relaçao de dados  " + Dados + "\n\n");
            //sw.WriteLine(" Query Sql:  " + Log); 
            sw.Close();

        }
    }
}
