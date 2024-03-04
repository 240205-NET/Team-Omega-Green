using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRS.Data;
using BRS.Logic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BRS.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repo;
        private readonly ILogger<CategoryController> _logger;

        //constructor
        public CategoryController(ICategoryRepository repo, ILogger<CategoryController> logger)
        {
            this._repo = repo;
            this._logger = logger;

        }
        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCateogriesAsync()
        {
            IEnumerable<Category> categories;
            try
            {
                categories = await _repo.GetAllCategoriesAsync();
            }
            catch (Exception e) {

                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return categories.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id)
        {
            Category cat;
            try
            {
                cat = await _repo.GetCategoryByIdAsync(id);
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return cat;
        }

        // GET api/values/5
        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<Category>> GetCategoryByNameAsync(string name)
        {
            Category cat;
            try
            {
                cat = await _repo.GetCategoryByNameAsync(name);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return cat;
        }
        // POST api/values
        [HttpPost]
        public async Task<ActionResult> AddCategoriesAsync([FromBody]Category category)
        {
            try
            {
                await _repo.AddNewCategoriesAsync(category);
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return StatusCode(200);
        }

    }
}

