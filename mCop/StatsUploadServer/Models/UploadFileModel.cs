using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StatsUploadServer.Models
{
    public class UploadFileModel
    {
        public class Request {
            [Required]
            public string device_mac { get; set; }
            [Required]
            public byte[] file { get; set; }
            public string os_version { get; set; }
            public string device_model { get; set; }
        }
        public class Response {
            public int IsCompleted;
            public string Message;

        }


    }
}