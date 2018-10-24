using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearner.Infrastructure.Static.Data.Repositories {
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

        public Student Create(Student entity) {
            entity.Id = FakeDB.Id++;
            FakeDB.Students.Add(entity);
            return entity;
        }

        public Student Get(int id) {
            return FakeDB.Students.FirstOrDefault(student => student.Id == id);
        }

        public IEnumerable<Student> GetAll() {
            return FakeDB.Students;
        }

        public Student Update(Student entity) {
            var studentFromDb = Get(entity.Id);
            if (studentFromDb == null) return null;

            studentFromDb.Username = entity.Username;
            return studentFromDb;
        }

        public Student Delete(int id) {
            var studentFromDb = Get(id);
            if (studentFromDb == null) return null;
            FakeDB.Students.Remove(studentFromDb);
            return studentFromDb;
        }

    }
}
