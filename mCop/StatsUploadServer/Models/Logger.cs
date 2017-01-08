using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace StatsUploadServer.Models
{
    public class Logger
    {

        public Logger()
        {
            string Name = ConfigurationManager.AppSettings["Path_Log"] + "Log.txt";
            string Name2 = ConfigurationManager.AppSettings["Path_Log"] + "ClientErros.txt";
            if (!(File.Exists(Name)))
            {
                File.CreateText(Name);
            }
            if (!(File.Exists(Name2)))
            {
                File.CreateText(Name2);
            }
        }
        public static void writeLog(string message)
        {
            string Name = ConfigurationManager.AppSettings["Path_Log"] + "Log.txt";
            string data = DateTime.Now.ToString() + "\t" + message + "\n" + Environment.NewLine;
            File.AppendAllText(Name, data);
        }

        public static void writeLine()
        {
            string Name = ConfigurationManager.AppSettings["Path_Log"] + "Log.txt";
            string data = "------****------****------****------****------****------****\n\n" + Environment.NewLine;
            File.AppendAllText(Name, data);
        }

        public static void writeError(string message)
        {
            string Name = ConfigurationManager.AppSettings["Path_Log"] + "ClientErros.txt";
            string data = DateTime.Now.ToString() + "\t" + message + "\n" + Environment.NewLine;
            File.AppendAllText(Name, data);

        }
    }
}