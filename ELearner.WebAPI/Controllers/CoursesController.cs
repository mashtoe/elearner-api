using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearner.Core.ApplicationService;
using ELearner.Core.ApplicationService.ServicesFacade;
using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elearner.API.Controllers {
    [Route("api/[controller]")]
    public class CoursesController : Controller {
        private readonly IServicesFacade _servicesFacade;

        public CoursesController(IServicesFacade facade) {
            _servicesFacade = facade;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<CourseBO>> Get() {
            return Ok(_servicesFacade.CourseService.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<CourseBO> Get(int id) {
            return Ok(_servicesFacade.CourseService.Get(id));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<CourseBO> Post([FromBody]CourseBO course) {
            return Ok(_servicesFacade.CourseService.Create(course));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<CourseBO> Put(int id, [FromBody]CourseBO course) {
            course.Id = id;
            return Ok(_servicesFacade.CourseService.Update(course));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<CourseBO> Delete(int id) {
            return Ok(_servicesFacade.CourseService.Delete(id));
        }
    }
}
