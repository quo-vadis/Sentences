using System.ComponentModel.DataAnnotations;

namespace Sentences.Models
{
    public class Sentence
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
