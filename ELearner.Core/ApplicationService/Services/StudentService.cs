using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService;
using ELearner.Core.Entity;

namespace ELearner.Core.ApplicationService.Services {

    // Services are part of the onions core. The core contains classes that the outer layers can depend on
    public class StudentService : IStudentService {

        readonly IStudentRepository _studentRepo;

        public StudentService(IStudentRepository studentRepo) {
            _studentRepo = studentRepo;
        }
        public Student New() {
            return new Student();
        }

        public Student Create(Student student) {
            // TODO check if entity is valid, and throw errors if not
            return _studentRepo.Create(student);
        }

        public Student Delete(int id) {
            return _studentRepo.Delete(id);
        }

        public Student Get(int id) {
            return _studentRepo.Get(id);
        }

        public List<Student> GetAll() {
            return _studentRepo.GetAll().ToList();
        }

        public Student Update(Student student) {
            return _studentRepo.Update(student);
        }
    }
}
