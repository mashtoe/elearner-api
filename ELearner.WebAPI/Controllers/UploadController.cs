using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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

        [HttpPost("{courseId}"), DisableRequestSizeLimit]
        public ActionResult<UndistributedCourseMaterialBO> UploadFile(int courseId) {
            try {
                IFormFile file = Request.Form.Files[0];
                var material = _fileHandlingService.UploadFile(file, courseId);
                return Ok(material);
            } catch (System.Exception ex) {
                return BadRequest();
            }
        }
    }
}