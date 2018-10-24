using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService;
using ELearner.Core.Entity;

namespace ELearner.Core.ApplicationService.Services {
    public class StudentService : IStudentService {

        readonly IStudentRepository _studentRepo;

        public StudentService(IStudentRepository studentRepo) {
            _studentRepo = studentRepo;
        }
        public Student New() {
            return new Student();
        }

        public Student Create(Student entity) {
            // TODO check if entity is valid, and throw errors if not
            return _studentRepo.Create(entity);
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

        public Student Update(Student entity) {
            return _studentRepo.Update(entity);
        }
    }
}
