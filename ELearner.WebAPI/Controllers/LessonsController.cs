using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearner.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class LessonsController : Controller
    {
        readonly ILessonService _lessonService;
        readonly IFileHandlingService _fileHandlingService;

        public LessonsController(ILessonService lessonService, IFileHandlingService fileHandlingService)
        {
            _fileHandlingService = fileHandlingService;
            _lessonService = lessonService;
        }

        /*[Authorize(Roles = "Admin, Educator, Student")]
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<LessonBO>> Get()
        {
            return Ok(_lessonService.GetAll());
        }*/
        [Authorize(Roles = "Admin, Educator, Student")]
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<LessonBO> Get(int id)
        {
            return Ok(_lessonService.Get(id));
        }
        /*
        [Authorize(Roles = "Admin, Educator")]
        // POST api/<controller>
        [HttpPost]
        public ActionResult<LessonBO> Post([FromBody]LessonBO lesson)
        {
            if (lesson == null)
            {
                return BadRequest();
            }
            return Ok(_lessonService.Create(lesson));
        }*/
        /*public ActionResult<LessonBO> Put(int id, [FromBody]LessonBO lesson)
        {
            if (lesson == null)
            {
                return BadRequest();
            }
            lesson.Id = id;
            return Ok(_lessonService.Update(lesson));
        }*/
        [Authorize(Roles = "Admin")]
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<LessonBO> Delete(int id)
        {
            return Ok(_lessonService.Delete(id));
        }

        // [Authorize(Roles = "Admin, Educator, Student")]
        [HttpGet("stream/{lessonID}/{name}")]
        public FileStreamResult GetVideo(string lessonId, string name)
        {
            var stream = _fileHandlingService.GetVideoStream(name);
            return File(stream, new MediaTypeHeaderValue("video/mp4").MediaType, true);
        }
    }
}