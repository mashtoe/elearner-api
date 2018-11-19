using System;
using System.Collections.Generic;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

namespace ELearner.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SectionsController : Controller
    {
        readonly ISectionService _sectionService;
        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<SectionBO>> Get()
        {
            return Ok(_sectionService.GetAll());
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<SectionBO> Get(int id)
        {
            return Ok(_sectionService.Get(id));
        }
        // POST api/<controller>
        [HttpPost]
        public ActionResult<SectionBO> Post([FromBody]SectionBO section)
        {
            if (section == null)
            {
                return BadRequest();
            }
            return Ok(_sectionService.Create(section));
        }
        public ActionResult<SectionBO> Put(int id, [FromBody]SectionBO section)
        {
            if (section == null)
            {
                return BadRequest();
            }
            section.Id = id;
            return Ok(_sectionService.Update(section));
        }
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<SectionBO> Delete(int id)
        {
            return Ok(_sectionService.Delete(id));
        }
    }
}