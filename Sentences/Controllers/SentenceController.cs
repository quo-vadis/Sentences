using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sentences.DAL;
using Sentences.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sentences.Controllers
{
    [Route("api/[controller]")]
    public class SentenceController : Controller
    {
        private readonly SentencesContext _context;

        public SentenceController(SentencesContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Sentence> GetAll()
        {
            return _context.Sentences.ToList();
        }

        // GET: api/<controller>/{id}
        [HttpGet("{id}", Name = "GetSentence")]
        public IActionResult GetById(int id)
        {
            var sentence = _context.Sentences.FirstOrDefault(s => s.Id == id);
            if(sentence == null)
            {
                return NotFound();
            }

            return new ObjectResult(sentence);
        }


        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody]Sentence sentence)
        {
            if(sentence == null)
            {
                return BadRequest();
            }

            _context.Sentences.Add(sentence);
            _context.SaveChanges();

            return CreatedAtRoute("GetSentence", new { id = sentence.Id }, sentence);
        }
    }
}
