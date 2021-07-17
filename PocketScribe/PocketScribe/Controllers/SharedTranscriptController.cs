using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PocketScribe.Models;
using PocketScribe.ViewModels;

namespace PocketScribe.Controllers
{
    public class SharedTranscriptController : Controller
    {


        private ApplicationDbContext _context;

        public SharedTranscriptController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Shared
        public ActionResult Index()
        {
            var user = User.Identity.GetUserName();
            var transcripts = _context.Transcripts.Where(t => t.SharedUsersCollection.Select(x => x.User).Contains(user));

            return View(transcripts);
        }

        /// <summary>
        /// Gets the information from a single transcript for the main recording and modifying transcript page
        /// Also gets the list of that users transcripts for the collapsable sidebar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Details(int id)
        {
            var transcript = _context.Transcripts.SingleOrDefault(t => t.Id == id);
            if (transcript == null)
            {
                return HttpNotFound();
            }
            var user = User.Identity.GetUserName();
            if (transcript.SharedUsersCollection.Select(x => x.User).Contains(user))
            {
                var transcripts = _context.Transcripts.Where(t => t.SharedUsersCollection.Select(x => x.User).Contains(user));
                var viewModel = new AllTranscipts()
                {
                    transcript = transcript,
                    Transcripts = transcripts
                };

                return View(viewModel);

            }
            else return HttpNotFound();

        }
    }
}