using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PocketScribe.Models;

namespace PocketScribe.ViewModels
{
    public class AllTranscipts
    {
        public Transcript transcript { get; set; }
        public IEnumerable<Transcript> Transcripts { get; set; }

    }
}