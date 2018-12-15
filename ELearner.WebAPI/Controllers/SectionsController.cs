using System;
using System.Collections.Generic;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin, Educator, Student")]
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<SectionBO>> Get()
        {
            return Ok(_sectionService.GetAll());
        }
        [Authorize(Roles = "Admin, Educator, Student")]
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<SectionBO> Get(int id)
        {
            return Ok(_sectionService.Get(id));
        }
        [Authorize(Roles = "Admin, Educator")]
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
        [Authorize(Roles = "Admin")]
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<SectionBO> Delete(int id)
        {
            return Ok(_sectionService.Delete(id));
        }
    }
}