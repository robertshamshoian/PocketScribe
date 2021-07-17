using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PocketScribe.Models;
using PocketScribe.ViewModels;


namespace PocketScribe.Controllers
{
    public class TranscriptController : Controller
    {

        private ApplicationDbContext _context;

        public TranscriptController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        /// <summary>
        /// GET: /transcript
        /// </summary>
        /// <returns>returns all the transcripts the user has already created</returns>
        [Authorize]
        public ViewResult Index()
        {
            var user = User.Identity.GetUserName();
            var transcripts = _context.Transcripts.Where(t => t.UserName == user);
            return View(transcripts);
        }

        /// <summary>
        /// Creates a new transcript, adds it to database, then redirects user to that transcript page
        /// </summary>
        [HttpPost]
        [Authorize]
        public void Create()
        {

            var date = DateTime.Now;
            var user = User.Identity.GetUserName();
            var transcript = new Transcript() { Date = date, Name = "MyTranscript", UserName = user };
            var transcripts = _context.Transcripts.Where(t => t.UserName == user);
            _context.Transcripts.Add(transcript);
            _context.SaveChanges();
            var id = transcript.Id;
            transcript.Name = "MyTranscript" + id;
            _context.SaveChanges();
            Response.Redirect("~/Transcript/Details/" + id);
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
            if (transcript.UserName == User.Identity.Name.ToString())
            {
                var user = User.Identity.GetUserName();
                var transcripts = _context.Transcripts.Where(t => t.UserName == user);
                var viewModel = new AllTranscipts()
                {
                    transcript = transcript,
                    Transcripts = transcripts
                };

                return View(viewModel);

            }
            else return HttpNotFound();

        }
        /// <summary>
        /// When the start recording button is hit, change the started value to true
        /// </summary>
        /// <param name="id">id of the transcript</param>
        [Authorize]
        public void StartRecording(int id)
        {
            var transcript = _context.Transcripts.SingleOrDefault(t => t.Id == id);
            transcript.started = true;
            _context.SaveChanges();
        }
        /// <summary>
        /// Once the stop recording button is hit, get the text that was transcribed and save it to the database for that specific transcription
        /// </summary>
        /// <param name="id">id of the transcription</param>
        /// <param name="text"> contains the entire transcription text</param>
        [Authorize]
        public void StopRecording(int id, string text)
        {

           var transcript = _context.Transcripts.SingleOrDefault(c => c.Id == id);
           transcript.Text = text;
           transcript.stopped = true;
           _context.SaveChanges();
           Details(id);
        }
        [Authorize]
        public void Rename(int id, string name)
        {
            var v = _context.Transcripts.SingleOrDefault(a => a.Id == id);
            v.Name = name;
            _context.SaveChanges();
        }

        [Authorize]
        public void EditText(int id, string text)
        {
            var v = _context.Transcripts.SingleOrDefault(a => a.Id == id);
            v.Text = text;
            _context.SaveChanges();
        }
        /// <summary>
        /// Adds users to the shared transcript list
        /// regexUtilities checks if the string given is a valid email address
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usersList"></param>
        [Authorize]
        public string Share(int id, string users)
        {

            List<string> usersList = users.Split(',').ToList<string>();
            List<string> returnList = new List<string>();
            var transcript = _context.Transcripts.SingleOrDefault(a => a.Id == id);
            foreach (var user in usersList)
            {
                var trimmedUser = user.Trim();
                if (trimmedUser != User.Identity.Name.ToString() && !trimmedUser.IsEmpty() && RegexUtilities.IsValidEmail(trimmedUser) && !transcript.SharedUsersCollection.Select(x => x.User).Contains(trimmedUser))
                {
                    var s = new Share()
                    {
                        TranscriptID = transcript.Id,
                        User = trimmedUser
                    };
                    transcript.SharedUsersCollection.Add(s);
                    returnList.Add(trimmedUser);
                }
            }
            _context.SaveChanges();
            return String.Join(", ", returnList);
        }

    }
}