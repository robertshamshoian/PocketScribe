    using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Antlr.Runtime;
using AutoMapper;
using PocketScribe.Dtos;
using PocketScribe.Models;

namespace PocketScribe.Controllers.Api
{
    public class TranscriptsController : ApiController
    {

        private ApplicationDbContext _context;


        public TranscriptsController()
        {
            _context = new ApplicationDbContext();
        }

        //Get /api/transcripts
        public IEnumerable<TranscriptDto> GetTranscripts()
        {
            return _context.Transcripts.ToList().Select(Mapper.Map<Transcript,TranscriptDto>);
        }


        //Get api/transcripts/1
        public IHttpActionResult GetTranscript(int id)
        {
            var transcript = _context.Transcripts.SingleOrDefault(t => t.Id == id);
            if (transcript == null)
                return NotFound();
            return Ok(Mapper.Map<Transcript,TranscriptDto>(transcript));
        }



        //POST /api/transcript
        [HttpPost]
        public IHttpActionResult CreateTranscript(TranscriptDto transcriptDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transcript = Mapper.Map<TranscriptDto, Transcript>(transcriptDto);
            _context.Transcripts.Add(transcript);
            _context.SaveChanges();

            transcriptDto.Id = transcript.Id;
            return Created(new Uri(Request.RequestUri + "/" + transcript.Id), transcriptDto);
        }

        //PUT /api/transcript/1
        [HttpPut]
        public void UpdateTranscript(int id, TranscriptDto transcriptDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var transcriptInDb = _context.Transcripts.SingleOrDefault(t => t.Id == id);
            if (transcriptInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<TranscriptDto, Transcript>(transcriptDto, transcriptInDb);
            _context.SaveChanges();
        }

        //DELETE /api/transcript/1
        [HttpDelete]
        public void DeleteTranscript(int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var transcriptInDb = _context.Transcripts.SingleOrDefault(t => t.Id == id);
            if (transcriptInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Transcripts.Remove(transcriptInDb);
            _context.SaveChanges();

        }

    }
}
