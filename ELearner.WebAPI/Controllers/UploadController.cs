using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Elearner.API.Hubs;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Elearner.API.Controllers
{
    [Authorize(Roles = "Admin, Educator")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        readonly IFileHandlingService _fileHandlingService;
        readonly IHubContext<JobProgressHub> _hubContext;

        public UploadController(IFileHandlingService fileHandlingService, IHubContext<JobProgressHub> hubContext) {
            _fileHandlingService = fileHandlingService;
            _hubContext = hubContext;
        }
        /*
        /// <summary>
        /// </summary>
        /// <param name="courseId">course id is for assigning to correct course</param>
        /// <param name="jobId">jobId: (defined as userId) client subscribes to listen for events with their own jobId</param>
        /// <returns></returns>
        [HttpPost("{courseId}/{jobId}"), DisableRequestSizeLimit]
        public ActionResult<UndistributedCourseMaterialBO> UploadFile(int courseId, int jobId) {
            try {
                IFormFile file = Request.Form.Files[0];

                var progressIndicator = new Progress<UploadProgress>(ReportProgress);
                var material = _fileHandlingService.UploadFile(file, courseId, progressIndicator, jobId);
                return material;
            } catch (System.Exception ex) {
                return BadRequest();
            }
        }*/
        
        /// <summary>
        /// </summary>
        /// <param name="courseId">course id is for assigning to correct course</param>
        /// <param name="jobId">jobId: (defined as userId) client subscribes to listen for events with their own jobId</param>
        /// <param name="generatedFileName">generatedFileName: user in client can have multiple files being uploaded at once. 
        /// this is used to assign progress to correct upload. Also used for the name of the file</param>
        /// <returns></returns>
        [HttpPost("{courseId}/{jobId}/{generatedFileName}"), DisableRequestSizeLimit]
        public ActionResult<LessonBO> UploadFile(int courseId, int jobId, string generatedFileName) {
            try {
                IFormFile file = Request.Form.Files[0];

                var progressIndicator = new Progress<UploadProgress>(ReportProgress);
                var lesson = _fileHandlingService.UploadFile(file, courseId, progressIndicator, jobId, generatedFileName);
                return lesson;
            }
            catch (System.Exception ex) {
                return BadRequest();
            }
        }

        void ReportProgress(UploadProgress uploadProgress)
        {
             _hubContext.Clients.Group(uploadProgress.JobId + "").SendAsync("progress", uploadProgress);

        }
    }
}