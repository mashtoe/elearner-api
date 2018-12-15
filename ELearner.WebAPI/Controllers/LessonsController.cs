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
        
        [HttpGet("stream/{lessonID}/{name}")]
        public FileStreamResult GetVideo(string lessonId, string name) {
            var stream = _lessonService.GetVideoStream(name);
            return File(stream, new MediaTypeHeaderValue("video/mp4").MediaType, true);
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public ActionResult UploadFile() {
            try {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                // string webRootPath = _hostingEnvironment.WebRootPath;
                string localPath = "C:/ElearnerFiles/";
                string newPath = Path.Combine(localPath, folderName);
                if (!Directory.Exists(newPath)) {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0) {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create)) {
                        file.CopyTo(stream);
                    }
                }
                return Json("Upload Successful.");
            } catch (System.Exception ex) {
                return Json("Upload Failed: " + ex.Message);
            }
        }
    }
}