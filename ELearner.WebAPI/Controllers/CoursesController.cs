﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Elearner.API.Helpers;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elearner.API.Controllers
{

    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
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

        [Authorize(Roles = "Admin, Educator, Student")]
        [HttpPost("filter")]
        public ActionResult<CoursePaginateDto> GetAllFiltered([FromBody]Filter filter) {
            try {
                return Ok(_courseService.GetFilteredOrders(filter));
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin, Educator, Student")]
        [HttpGet]
        public ActionResult<CoursePaginateDto> Get()
        {
            try
            {
                return Ok(_courseService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin, Educator, Student")]
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<CourseBO> Get(int id)
        {
            var course = _courseService.Get(id);
            return Ok(course);
        }

        [Authorize(Roles = "Admin, Educator")]
        // POST api/<controller>
        [HttpPost]
        public ActionResult<CourseBO> Post([FromBody]CourseBO course)
        {
            if (course == null)
            {
                return BadRequest();
            }
            int idFromJwt = new JwtHelper().GetUserIdFromToken(Request);
            if (idFromJwt != course.CreatorId) {
                return BadRequest();
            }

            return Ok(_courseService.Create(course));
        }

        [Authorize(Roles = "Admin, Educator")]
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<CourseBO> Put(int id, [FromBody]CourseBO course)
        {
            if (course == null)
            {
                return BadRequest();
            }
            int idFromJwt = new JwtHelper().GetUserIdFromToken(Request);
            if (idFromJwt != course.CreatorId) {
                return BadRequest();
            }
            course.Id = id;
            return Ok(_courseService.Update(course));
        }

        // [Authorize(Roles = "Admin")]
        // // DELETE api/<controller>/5
        // [HttpDelete("{id}")]
        // public ActionResult<CourseBO> Delete(int id)
        // {
        //     return Ok(_courseService.Delete(id));
        // }

        [Authorize(Roles = "Admin, Educator")]
        [HttpGet("publish/{courseId}")]
        public ActionResult<CourseBO> PublishCourse(int courseId)
        {
            int idFromJwt = new JwtHelper().GetUserIdFromToken(Request);
            var publishedCourse = _courseService.Publish(courseId, idFromJwt);

            if (publishedCourse != null)
            {
                return Ok(publishedCourse);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("creatorCourses/{creatorId}")]
        public ActionResult<IEnumerable<CourseBO>> GetCreatorsCourses (int creatorId) {
            try {
                var courses = _courseService.GetCreatorsCourses(creatorId);
                return Ok(courses);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}