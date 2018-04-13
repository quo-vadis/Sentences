using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sentences.DAL;
using Sentences.Models;

namespace Sentences.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly SentencesContext _context;

        public UploadController(SentencesContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IEnumerable<Sentence> Index()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Request;
            bool isDataBaseChanged = false;

            try
            {
                if (httpRequest.Form.Files[0] != null)
                {
                    var postedFile = httpRequest.Form.Files[0];

                    string pattern = httpRequest.Form["query"];

                    using (var reader = new StreamReader(postedFile.OpenReadStream()))
                    {
                        var result = reader.ReadToEnd();

                        var sentences = result.Split('.').ToList();
                        foreach (var sentence in sentences)
                        {
                            var words = sentence.Split(' ').ToList();
                            foreach (var word in words)
                            {
                                int isReverse = String.Compare(pattern, Reverse(word));
                                if (isReverse == 0)
                                {
                                    _context.Sentences.Add(new Sentence { Text = sentence });
                                    isDataBaseChanged = true;
                                }
                            }
                        }
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return isDataBaseChanged == true ? _context.Sentences : null;
        }

        public static string Reverse( string word)
        {
            var charArray = word.ToCharArray();
            Array.Reverse(charArray);
            return new string ( charArray );
        }
    }
}
