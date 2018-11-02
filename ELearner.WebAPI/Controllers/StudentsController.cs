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
        public ActionResult<IEnumerable<UserBO>> Get() {
            return Ok(_servicesFacade.UserService.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<UserBO> Get(int id) {
            return Ok(_servicesFacade.UserService.Get(id));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<UserBO> Post([FromBody]UserBO student) {
            return Ok(_servicesFacade.UserService.Create(student));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<UserBO> Put(int id, [FromBody]UserBO student) {
            student.Id = id;
            return Ok(_servicesFacade.UserService.Update(student));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<UserBO> Delete(int id) {
            return Ok(_servicesFacade.UserService.Delete(id));
        }
    }
}
