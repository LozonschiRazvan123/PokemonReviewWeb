using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewWeb.DTO;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Repository;

namespace PokemonReviewWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController: Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOwners()
        {
            var owner = _mapper.Map<List<OwnerDTO>>(_ownerRepository.GetOwners());
            /*var owners = _ownerRepository.GetOwners().Select(s => new OwnerDTO()
            {
                FirstName = s.FirstName,
                Gym= s.Gym,
            });

            return Ok(owners);*/
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpGet("{id}/Pokemon")]
        public IActionResult GetOwner(Guid id) 
        {
            if(!_ownerRepository.OwnerExist(id))
                return NotFound();
            var owner = _mapper.Map<OwnerDTO>(_ownerRepository.GetOwner(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpGet("{id}")]
        public IActionResult GetPokemonByOwner(Guid id)
        {
            if (!_ownerRepository.OwnerExist(id))
                return NotFound();
            var owner = _mapper.Map<List<OwnerDTO>>(_ownerRepository.GetOwnerOfPokemon(id).FirstOrDefault());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }
    }
}
