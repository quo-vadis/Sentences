using Microsoft.EntityFrameworkCore;
using Sentences.Models;

namespace Sentences.DAL
{
    public class SentencesContext : DbContext
    {
        public SentencesContext(DbContextOptions options)
               : base (options) 
        {            
        }

        public DbSet<Sentence> Sentences { get; set; }
    }
}
