using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PocketScribe.Models
{
    public class Transcript
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string UserName { get; set; }

        public bool started { get; set; }
        public bool stopped { get; set; }
        public bool paused { get; set; }

        public virtual ICollection<Share> SharedUsersCollection { get; set; }

        public string Text { get; set; }
    }
}