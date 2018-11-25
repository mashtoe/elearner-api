using System.Collections.Generic;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

namespace ELearner.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<CategoryBO>> Get()
        {
            return Ok(_categoryService.GetAll());
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<CategoryBO> Get(int id)
        {
            return Ok(_categoryService.Get(id));
        }
        // POST api/<controller>
        [HttpPost]
        public ActionResult<CategoryBO> Post([FromBody]CategoryBO category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            return Ok(_categoryService.Create(category));
        }
        public ActionResult<CategoryBO> Put(int id, [FromBody]CategoryBO category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            category.Id = id;
            return Ok(_categoryService.Update(category));
        }
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<CategoryBO> Delete(int id)
        {
            return Ok(_categoryService.Delete(id));
        }
        
    }
}