using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

namespace ELearner.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class LessonsController : Controller
    {
        readonly ILessonService _lessonService;
        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<LessonBO>> Get()
        {
            return Ok(_lessonService.GetAll());
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<LessonBO> Get(int id)
        {
            return Ok(_lessonService.Get(id));
        }
        // POST api/<controller>
        [HttpPost]
        public ActionResult<LessonBO> Post([FromBody]LessonBO lesson)
        {
            if (lesson == null)
            {
                return BadRequest();
            }
            return Ok(_lessonService.Create(lesson));
        }
        public ActionResult<LessonBO> Put(int id, [FromBody]LessonBO lesson)
        {
            if (lesson == null)
            {
                return BadRequest();
            }
            lesson.Id = id;
            return Ok(_lessonService.Update(lesson));
        }
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<LessonBO> Delete(int id)
        {
            return Ok(_lessonService.Delete(id));
        }
        
        [HttpGet("stream")]
        public IActionResult GetVideo() {
            var headers = Request.Headers;
            var stream = new FileStream("pathUri", FileMode.Open, FileAccess.Read);
            return new FileStreamResult(stream, new MediaTypeHeaderValue("video/mp4").MediaType);
        }
    }
}