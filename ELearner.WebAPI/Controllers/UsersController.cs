using System;
using System.Collections.Generic;
using System.Linq;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elearner.API.Controllers {
    [Route("api/[controller]")]

    public class UsersController : Controller {

        private readonly IUserService _userService;
        // We get the service class via dependancy injection. The service classes are part of the Core of the Onion architecture
        // The call hierarchy goes roughly: Controller --> Service --> Repository --> DB 
        public UsersController(IUserService userService) {
            _userService = userService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<UserBO>> Get() {
            return Ok(_userService.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<UserBO> Get(int id) {
            return Ok(_userService.Get(id));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<UserBO> Post([FromBody]UserBO student) {
            return Ok(_userService.Create(student));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<UserBO> Put(int id, [FromBody]UserBO student) {
            student.Id = id;
            return Ok(_userService.Update(student));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<UserBO> Delete(int id) {
            return Ok(_userService.Delete(id));
        }
    }
}
