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
    public class ReviewerController: Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetReviewers()
        {
            var categories = _mapper.Map<List<ReviewerDTO>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetReviewer(int id)
        {
            var categories = _mapper.Map<ReviewerDTO>(_reviewerRepository.GetReviewer(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByAReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ExistReviewer(reviewerId))
                return NotFound();

            var reviews = _mapper.Map<List<ReviewDTO>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        public IActionResult CreateReviwer([FromBody] ReviewerDTO reviewerCreate)
        {
            if (reviewerCreate == null)
                return NotFound();


            var reviewer = _reviewerRepository.GetReviewers()
                .Where(r => r.LastName.Trim().ToLower() == reviewerCreate.LastName.Trim().ToLower())
                .FirstOrDefault();

            if(reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer already exists");
                return StatusCode(422, ModelState);
            }

            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewerId}")]
        public IActionResult updateReviewer(int reviewerId, [FromBody]ReviewerDTO reviewerUpdate) 
        {
            if(reviewerUpdate == null || reviewerUpdate.Id!=reviewerId)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest();

            var reviewerUpdateMap = _mapper.Map<Reviewer>(reviewerUpdate);
            if(!_reviewerRepository.UpdateReviewer(reviewerUpdateMap))
            {
                ModelState.AddModelError("", "Something went wrong updating reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reviewerId}")]
        public IActionResult deleteReviewer(int reviewerId)
        {
            if(!_reviewerRepository.ExistReviewer(reviewerId))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest();
            var reviewerDelete = _reviewerRepository.GetReviewer(reviewerId);
            if(!_reviewerRepository.DeleteReviewer(reviewerDelete))
                ModelState.AddModelError("", "Something went wrong deleting reviewer");
            return NoContent() ;
        }

    }
}
