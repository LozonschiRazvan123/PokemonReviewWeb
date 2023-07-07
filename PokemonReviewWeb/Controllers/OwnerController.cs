using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewWeb.DTO;
using PokemonReviewWeb.Interfaces;
using PokemonReviewWeb.Models;
using PokemonReviewWeb.Repository;

namespace PokemonReviewWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
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
            if (!_ownerRepository.OwnerExist(id))
                return NotFound();
            var owner = _mapper.Map<OwnerDTO>(_ownerRepository.GetOwner(id));
            if (!ModelState.IsValid)
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

        [HttpPost]
        public IActionResult OwnerCreate([FromBody] OwnerDTO createOwner)
        {
            if (createOwner == null)
                return NotFound();

            var owner = _ownerRepository.GetOwners()
                .Where(o => o.LastName.Trim().ToLower() == createOwner.LastName.ToLower())
                .FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            var ownerMap = _mapper.Map<Owner>(createOwner);

            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{ownerId}")]
        public IActionResult ownerUpdate(Guid ownerId, [FromBody] OwnerDTO updateOwner)
        {
            if (updateOwner == null || ownerId != updateOwner.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();
            
            var updateOwnerMap = _mapper.Map<Owner>(updateOwner);

            if (!_ownerRepository.UpdateOwner(updateOwnerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ownerId}")]
        public IActionResult deleteOwner(Guid ownerId)
        {
            if(_ownerRepository.OwnerExist(ownerId))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest();

            var ownerDelete = _mapper.Map<Owner>(ownerId);
            if(!_ownerRepository.DeleteOwner(ownerDelete))
                ModelState.AddModelError("", "Something went wrong deleting owner");

            return NoContent();
        }
    }
}
