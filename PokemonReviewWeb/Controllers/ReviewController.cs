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
    public class ReviewController: Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            var review = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("{id}")]
        public IActionResult GetOfReview(int id)
        {
            if(!_reviewRepository.ReviewExist(id))
                return NotFound();
            var review = _mapper.Map<ReviewDTO>(_reviewRepository.GetReview(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("pokemon/{PokemonId}")]
        public IActionResult GetReviewOfPokemon(int PokemonId)
        {
            if (!_reviewRepository.ReviewExist(PokemonId))
                return NotFound();
            var review = _mapper.Map<List<ReviewDTO>>(_reviewRepository.GetReviewOfPokemon(PokemonId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpPost]
        public IActionResult CreateReviw([FromBody] ReviewDTO reviewCreate)
        {
            if (reviewCreate == null)
                return NotFound();


            var review = _reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToLower() == reviewCreate.Title.Trim().ToLower())
                .FirstOrDefault();

            if(review != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            var reviwMap = _mapper.Map<Review>(reviewCreate);
            if(!_reviewRepository.CreateReview(reviwMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        public IActionResult updateReview(int reviewId, [FromBody]ReviewDTO review)
        {
            if(review == null || review.Id!=reviewId)
                return BadRequest(ModelState);
            
            if(!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(review);
            if(!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("reviewId")]
        public IActionResult deleteReview(int reviewId)
        {
            if(!_reviewRepository.ReviewExist(reviewId))
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest();

            var reviewDelete = _reviewRepository.GetReview(reviewId);
            if(!_reviewRepository.DeleteReview(reviewDelete))
                ModelState.AddModelError("", "Something went wrong deleting review");
            return NoContent();
        }
    }
}
