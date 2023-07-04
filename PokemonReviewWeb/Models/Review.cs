using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonReviewWeb.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        [ForeignKey("Reviewer")]
        public int ReviewerId { get; set; }
        public Reviewer Reviewer { get; set; }
        [ForeignKey("PokemonId")]
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
