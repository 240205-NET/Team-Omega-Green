using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BRS.Data;
using BRS.Logic;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace BRS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        // Fields
        private readonly IReviewRepository _repo;
        private readonly ILogger<ReviewController> _logger;

        // Constructor
        public ReviewController(IReviewRepository repo, ILogger<ReviewController> logger)
        {
            this._repo = repo;
            this._logger = logger;
        }

        //Methods

        // POST api/<ReviewController>

        [HttpPost]
        public async Task<ActionResult> PostNewReviewAsync([FromBody] Reviews review)
        {
            try
            {
                await _repo.AddNewReviewAsync(review);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

        // GET api/<ReviewController>/{id}

        [HttpGet("/reviews/book/{bId}")]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetAllReviewsByBookIdAsync(int bId)
        {
            IEnumerable<Reviews> reviews;
            try
            {
                reviews = await _repo.GetAllReviewsByBookIdAsync(bId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return reviews.ToList();
        }

        [HttpGet("/reviews/book/user/{uId}")]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetAllReviewsByUserIdAsync(int uId)
        {
            IEnumerable<Reviews> reviews;
            try
            {
                reviews = await _repo.GetAllReviewsByUserIdAsync(uId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return reviews.ToList();
        }

        [HttpGet("/reviews/book/review/{rId}")]
        public async Task<ActionResult<Reviews>> GetReviewByReviewIdAsync(int rId)
        {
            Reviews review;
            try
            {
                review = await _repo.GetReviewByReviewIdAsync(rId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return review;
        }

        // DELETE api/<ReviewController>/{id}

        [HttpDelete("/reviews/delete-review/{rId}")]
        public async Task<ActionResult> DeleteReviewByReviewIdAsync(int rId)
        {
            try
            {
                await _repo.DeleteReviewByReviewIdAsync(rId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

        [HttpDelete("/reviews/delete-book-reviews/{bId}")]
        public async Task<ActionResult> DeleteReviewByBookIdAsync(int bId)
        {
            try
            {
                await _repo.DeleteReviewByBookIdAsync(bId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

        [HttpDelete("/reviews/delete-user-reviews/{uId}")]
        public async Task<ActionResult> DeleteReviewByUserIdAsync(int uId)
        {
            try
            {
                await _repo.DeleteReviewByUserIdAsync(uId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }
    }
}