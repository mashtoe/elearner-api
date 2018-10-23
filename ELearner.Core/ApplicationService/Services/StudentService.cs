using System;
using System.Collections.Generic;
using System.Text;
using ELearner.Core.Entity;

namespace ELearner.Core.ApplicationService.Services {
    class StudentService : IStudentService {

        public StudentService() {

        }
        public Student New() {
            return new Student();
        }

        public Student Create(Student entity) {
            throw new NotImplementedException();
        }

        public Student Delete(int id) {
            throw new NotImplementedException();
        }

        public Student Get(int id) {
            throw new NotImplementedException();
        }

        public List<Student> GetAll() {
            throw new NotImplementedException();
        }

        public Student Update(Student entity) {
            throw new NotImplementedException();
        }
    }
}
