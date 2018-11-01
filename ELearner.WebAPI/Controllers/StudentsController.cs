using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearner.Core.ApplicationService;
using ELearner.Core.ApplicationService.ServicesFacade;
using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elearner.API.Controllers {
    [Route("api/[controller]")]

    public class StudentsController : Controller {
        private readonly IServicesFacade _servicesFacade;
        // We get the servicesfacade class via dependancy injection. The service classes inside the facade are part of the Core of the Onion architecture
        // The call hierarchy goes roughly: Controller --> Service --> Repository --> DB 
        public StudentsController(IServicesFacade facade) {
            _servicesFacade = facade;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<StudentBO>> Get() {
            return Ok(_servicesFacade.StudentService.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<StudentBO> Get(int id) {
            return Ok(_servicesFacade.StudentService.Get(id));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<StudentBO> Post([FromBody]StudentBO student) {
            return Ok(_servicesFacade.StudentService.Create(student));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<StudentBO> Put(int id, [FromBody]StudentBO student) {
            student.Id = id;
            return Ok(_servicesFacade.StudentService.Update(student));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<StudentBO> Delete(int id) {
            return Ok(_servicesFacade.StudentService.Delete(id));
        }
    }
}
