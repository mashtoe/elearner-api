using System;
using System.Collections.Generic;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elearner.API.Controllers {
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class CoursesController : Controller {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService) {
            _courseService = courseService;
        }


        //// GET: api/<controller>
        //[HttpGet]
        //public ActionResult<CoursePaginateDto> Get([FromQuery]Filter filter)
        //{
        //    try
        //    {
        //        return Ok(_courseService.GetFilteredOrders(filter));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // GET: api/<controller>

        [HttpPost("filter")]
        public ActionResult<CoursePaginateDto> GetAllFiltered([FromBody]Filter filter)
        {
            try
            {
                return Ok(_courseService.GetFilteredOrders(filter));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<CoursePaginateDto> Get() {
            try {
                return Ok(_courseService.GetAll());
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<CourseBO> Get(int id) {
            return Ok(_courseService.Get(id));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<CourseBO> Post([FromBody]CourseBO course) {
            if (course == null) {
                return BadRequest();
            }
            return Ok(_courseService.Create(course));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<CourseBO> Put(int id, [FromBody]CourseBO course) {
            if (course == null) {
                return BadRequest();
            }
            course.Id = id;
            return Ok(_courseService.Update(course));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<CourseBO> Delete(int id) {
            return Ok(_courseService.Delete(id));
        }

        [HttpGet("publish/{courseId}")]
        public ActionResult<CourseBO> PublishCourse(int courseId) {
            var publishedCourse = _courseService.Publish(courseId);
            if (publishedCourse != null) {
                return Ok(publishedCourse);
            } else {
                return BadRequest();
            }
        }


        
    }
}