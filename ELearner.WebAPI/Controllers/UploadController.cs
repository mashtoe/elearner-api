using System.IO;
using System.Net.Http.Headers;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elearner.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        readonly IFileHandlingService _fileHandlingService;

        public UploadController(IFileHandlingService fileHandlingService) {
            this._fileHandlingService = fileHandlingService;

        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult<UndistributedCourseMaterialBO> UploadFile() {
            try {
                IFormFile file = Request.Form.Files[0];
                
                return Json("Upload Successful.");
            } catch (System.Exception ex) {
                return Json("Upload Failed: " + ex.Message);
            }
        }
    }
}