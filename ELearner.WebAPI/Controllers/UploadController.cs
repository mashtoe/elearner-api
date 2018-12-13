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
using Microsoft.AspNetCore.SignalR;

namespace Elearner.API.Controllers
{
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

        [HttpPost("{courseId}/{jobId}"), DisableRequestSizeLimit]
        public ActionResult<UndistributedCourseMaterialBO> UploadFile(int courseId, int jobId) {
            try {
                IFormFile file = Request.Form.Files[0];

                var progressIndicator = new Progress<UploadProgress>(ReportProgress);
                var material = _fileHandlingService.UploadFile(file, courseId, progressIndicator, jobId);
                return Json("Upload succesful");
            }
            catch (System.Exception ex) {
                return BadRequest();
            }
        }

        void ReportProgress(UploadProgress uploadProgress)
        {
             _hubContext.Clients.Group(uploadProgress.JobId + "").SendAsync("progress", uploadProgress.Progress);

        }
    }
}