using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewWeb.DTO;
using PokemonReviewWeb.Interfaces;
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
    }
}
