using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDevelopmentBackend.Models
{
    public class VideoModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public string UploadedAt { get; set; }
        public string UploadedBy { get; set; }


    }
}