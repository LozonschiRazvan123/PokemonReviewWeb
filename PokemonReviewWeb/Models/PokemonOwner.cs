namespace PokemonReviewWeb.Models
{
    public class PokemonOwner
    {
        public int PokemonId { get; set; }
        public string OwnerId { get; set;}
        public Pokemon Pokemon { get; set; }
        public Owner Owner { get; set; }
    }
}
