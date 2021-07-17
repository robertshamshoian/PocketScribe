using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PocketScribe.Dtos
{
    public class TranscriptDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string UserName { get; set; }
        public bool started { get; set; }
        public bool stopped { get; set; }
        public bool paused { get; set; }
        public string Text { get; set; }
        public string SharedUsers { get; set; }

    }
}