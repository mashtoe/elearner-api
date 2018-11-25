using System.Collections.Generic;
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
        public async Task<FileStreamResult> GetVideo() {
            // var stream = await _streamingService.GetVideoByName(name);
            var stream = await _lessonService.GetVideoStream();
            return new FileStreamResult(stream, "video/mp4");
        }
    }
}