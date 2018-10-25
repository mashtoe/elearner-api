using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearner.Core.ApplicationService;
using ELearner.Core.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elearner.API.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {

        private readonly IStudentService studentService;

        // We get the student service class via dependancy injection. The service classes are part of the Core of the Onion architecture
        // The call hierarchy goes: Controller --> Service --> Repository --> DB 
        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return Ok(studentService.GetAll());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            return Ok(studentService.Get(id));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Student student)
        {
            return Ok(studentService.Create(student));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult<Student> Delete(int id)
        {
            return Ok(studentService.Delete(id));
        }
    }
}
