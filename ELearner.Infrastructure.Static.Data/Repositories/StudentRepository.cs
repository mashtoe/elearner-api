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

        readonly FakeDB _fakeDb;

        public StudentRepository(FakeDB fakeDb) {
            _fakeDb = fakeDb;
            if (_fakeDb.Students.Count < 1) {
                var student1 = new Student() {
                    Id = FakeDB.Id++,
                    Username = "student1"
                };
                _fakeDb.Students.Add(student1);
            }
        }

        public Student Create(Student student) {
            student.Id = FakeDB.Id++;

            if (student.Courses != null) {
                // adding the reference between objects in the fake db
                foreach (var item in student.Courses) {
                    item.StudentId = student.Id;
                    _fakeDb.StudentCourses.Add(item);
                }
            }
            student.Courses = null;
            _fakeDb.Students.Add(student);


            return student;
        }

        public Student Get(int id) {
            var student = _fakeDb.Students.FirstOrDefault(s => s.Id == id);
            // include course ids. In EF we would use Include() method, but here we are using a fake db consisting of lists only,
            // but we have to return the same properties that are returned in the other implementations of the infrastructure layer
            if (student != null) {
                student.Courses = _fakeDb.StudentCourses.Where(sc => sc.StudentId == id).ToList();
            }
            return student;
        }

        public IEnumerable<Student> GetAll() {
            return _fakeDb.Students;
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
            _fakeDb.Students.Remove(studentFromDb);
            return studentFromDb;
        }

        public IEnumerable<Student> GetAllById(IEnumerable<int> ids) {
            var students = _fakeDb.Students.Where(s => ids.Contains(s.Id));
            return students;
        }
    }
}
