using StatsUploadServer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace StatsUploadServer.Controllers
{
    public class UploadFileController : ApiController
    {


        // POST api/uploadfile
        public UploadFileModel.Response Post([FromBody]UploadFileModel.Request value)
        {

            UploadFileModel.Response Response = new UploadFileModel.Response();
            try
            {

                if (value == null)
                {
                    Response.IsCompleted = 0;
                    Response.Message = "Not Found Data.";
                }
                else
                {
                    if (string.IsNullOrEmpty(value.device_mac))
                    {
                        var IP = ((HttpContextBase)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                        Logger.writeLog("IP : " + IP + "  Invalid requst. Unknown mac address." + "\n");
                        Response.IsCompleted = 0;
                        Response.Message = "กรุณาระบุ device_mac (string)";
                    }
                    else if (value.file.Count() == 0)
                    {
                        var IP = ((HttpContextBase)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                        Logger.writeLog("IP : " + IP + "  Invalid requst. Unknown File byte[]." + "\n");
                        Response.IsCompleted = 0;
                        Response.Message = "กรุณาระบุ file (byte[])";
                    }
                    else
                    {
                        string filename = value.device_mac + "_stats" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
                        SaveFile(value.file, value, filename);
                        Response.IsCompleted = 1;
                        Response.Message = "Upload File สำเร็จ";
                    }
                }
            }
            catch (Exception ex)
            {

                Response.IsCompleted = 0;
                Response.Message = ex.Message;
            }
            return Response;
        }
        protected void SaveFile(byte[] Data,UploadFileModel.Request value,string file)
        {
            string Name = ConfigurationManager.AppSettings["Path_Stats"] + value.device_mac;
            if (!Directory.Exists(Name))
            {
                System.IO.Directory.CreateDirectory(Name);
            }
            string filename = Name + @"\" + file;
            File.WriteAllBytes(filename, Data);
            FileDBManager obj_FileDBManager = new FileDBManager();
            var IP = ((HttpContextBase)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            obj_FileDBManager.insertFile(value.device_mac, filename, value.os_version, value.device_model, IP);
        }
    }
}
