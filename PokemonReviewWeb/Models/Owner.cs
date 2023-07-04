using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace PokemonReviewWeb.Models
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gym { get; set; }
        [ForeignKey("OwnerId")]
        public int OwnerId { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<PokemonOwner> PokemonOwners { get; set; }
    }
}
