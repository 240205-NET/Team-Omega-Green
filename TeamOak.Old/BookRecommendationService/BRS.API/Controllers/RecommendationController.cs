using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BRS.Data;
using BRS.Logic;

namespace BRS.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RecommendationController : ControllerBase 
	{
		private readonly IRecommendationSqlRepository repo;

		private readonly ILogger<RecommendationController> _logger;

		public RecommendationController(IRecommendationSqlRepository repo, ILogger<RecommendationController> logger)
		{
			this.repo = repo;
			this._logger = logger;
		}
		[HttpGet]
		public async Task<ActionResult<List<Book>>> GetRecommendationList(int userId)
		{
			List<Book> recommendationList = new List<Book>();
			try
			{
				recommendationList = await repo.GetRecommenedBookList(userId);
			}
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);
				return StatusCode(500);
			}
			return recommendationList.ToList();
		}

	}


}