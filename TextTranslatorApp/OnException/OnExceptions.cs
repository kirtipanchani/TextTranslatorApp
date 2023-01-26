using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TextTranslatorApp.OnException
{
    /// <summary>
    /// This is custom exception attribute to loging error onexception.
    /// </summary>
    public class OnExceptions : FilterAttribute, IExceptionFilter
    {
       
         void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            ExceptionLogging.SendErrorToText(filterContext.Exception);
            
        }
    }

    /// <summary>
    /// This method perform Exception Logging in txt
    /// </summary>
    public static class ExceptionLogging
    {

        private static string Error, Errormsg, extype, exurl;

        public static void SendErrorToText(Exception ex)
       {
            var line = Environment.NewLine + Environment.NewLine;

            Error = ex.StackTrace.ToString();
            Errormsg = ex.Message.ToString();
            extype = ex.GetType().ToString();
            exurl = System.Web.HttpContext.Current.Request.Url.ToString();

            try
            {
                string filepath = System.Web.HttpContext.Current.Server.MapPath("~/ExceptionDetailsFile/");  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + "Errorlog.txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + Error + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype +  line + " Error Page Url:" + " " + exurl + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

    }
}