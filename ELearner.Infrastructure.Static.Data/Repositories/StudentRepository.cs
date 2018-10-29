using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using ELearner.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearner.Infrastructure.Static.Data.Repositories {

    // the implementaion of the repository interfaces are part of the Infrastructure layer, which is an outer layer of the onion
    // the implementaions of the repository interfaces are decoupled & undepenable so that they are easily interchangeable & editable
    public class StudentRepository : IStudentRepository {

        public StudentRepository() {
            if (FakeDB.Students.Count < 1) {
                var student1 = new Student() {
                    Id = FakeDB.Id++,
                    Username = "student1"
                };
                FakeDB.Students.Add(student1);
            }
        }

        public Student Create(Student student) {
            student.Id = FakeDB.Id++;
            FakeDB.Students.Add(student);
            return student;
        }

        public Student Get(int id) {
            return FakeDB.Students.FirstOrDefault(student => student.Id == id);
        }

        public IEnumerable<Student> GetAll() {
            return FakeDB.Students;
        }

        public Student Update(Student student) {
            var studentFromDb = Get(student.Id);
            if (studentFromDb == null) return null;

            studentFromDb.Username = student.Username;
            return studentFromDb;
        }

        public Student Delete(int id) {
            var studentFromDb = Get(id);
            if (studentFromDb == null) return null;
            FakeDB.Students.Remove(studentFromDb);
            return studentFromDb;
        }

        public IEnumerable<Student> GetAllById(IEnumerable<int> ids) {
            var students = FakeDB.Students.Where(s => ids.Contains(s.Id));
            return students;
        }
    }
}
