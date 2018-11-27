using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        public FileStreamResult GetVideo() {
            // var url = "http://elearning.vps.hartnet.dk/sample.mp4";
            // CTRL E -> V ev
            var url = "C:/ElearnerFiles/long.mp4";
            //var url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

            var stream = _lessonService.GetVideoStream(url);
            // stream.Seek(stream.Length / 2, SeekOrigin.Current);
            return File(stream, new MediaTypeHeaderValue("video/mp4").MediaType, true);
            // return new FileStreamResult(stream, new MediaTypeHeaderValue("video/mp4").MediaType);
        }

        //[HttpGet("stream")]
        //public IActionResult GetVideo() {
        //    //var url = "http://elearning.vps.hartnet.dk/sample.mp4";
        //    //var stream = _lessonService.GetVideoStream(url);
        //    var stream = new FileStream("pathUri", FileMode.Open, FileAccess.Read);
        //    return new FileStreamResult(stream, new MediaTypeHeaderValue("video/mp4").MediaType);
        //}
    }
}