using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketScribe.Models
{
    public class Share
    {
        public int Id { get; set; }
        public int TranscriptID { get; set; }
        public string User { get; set; }
    }
}