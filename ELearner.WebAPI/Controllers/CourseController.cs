using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elearner.API.Controllers {
    [Route("api/[controller]")]
    public class CourseController : Controller {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService) {
            _courseService = courseService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Course>> Get() {
            return Ok(_courseService.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Course> Get(int id) {
            return Ok(_courseService.Get(id));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Course> Post([FromBody]Course course) {
            return Ok(_courseService.Create(course));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<Course> Put(int id, [FromBody]Course course) {
            return Ok(_courseService.Update(course));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<Course> Delete(int id) {
            return Ok(_courseService.Delete(id));
        }
    }
}
