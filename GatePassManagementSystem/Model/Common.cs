using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GatePassManagementSystem.Model
{
    public class Common
    {
        public string TextBoxValue { get; set; }

        public void Logwrite(string message)
        {
            try
            {
                string logFile = "GatePassSystemLogs_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string Filepath = "C:\\Users\\Janani\\source\\repos\\GatePassManagementSystem\\GatePassManagementSystem\\wwwroot\\Logs\\";
                Filepath = Filepath + logFile;
                DateTime CurrentTime = DateTime.Now;
                using (StreamWriter file = new StreamWriter(@Filepath, true))
                {
                    file.Write("\r\nLog Entry : ");
                    file.WriteLine(CurrentTime + " || " + message );
                    file.WriteLine("-------------------------------");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
