using System.Collections.Generic;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

namespace ELearner.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationsController : Controller
    {
        readonly IApplicationService _applicationService;
        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<ApplicationBO>> Get()
        {
            return Ok(_applicationService.GetAll());
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<ApplicationBO> Get(int id)
        {
            return Ok(_applicationService.Get(id));
        }
        // POST api/<controller>
        [HttpPost]
        public ActionResult<ApplicationBO> Post([FromBody]ApplicationBO application)
        {
            if (application == null)
            {
                return BadRequest();
            }
            var applicationCreated = _applicationService.Create(application);
            if (applicationCreated != null) {
                return Ok(applicationCreated);
            } else {
                return BadRequest();
            }
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<ApplicationBO> Delete(int id)
        {
            return Ok(_applicationService.Delete(id));
        }
        
    }
}