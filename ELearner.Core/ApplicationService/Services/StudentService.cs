using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services {

    // Services are part of the onions core. The core contains classes that the outer layers can depend on
    public class StudentService : IStudentService {

        readonly IStudentRepository _studentRepo;
        readonly ICourseRepository _courseRepo;
        readonly StudentConverter _studConv;
        readonly CourseConverter _crsConv;

        public StudentService(IStudentRepository studentRepo, ICourseRepository courseRepo) {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
            _studConv = new StudentConverter();
            _crsConv = new CourseConverter();
        }
        public StudentBO New() {
            return new StudentBO();
        }

        public StudentBO Create(StudentBO student) {
            // TODO check if entity is valid, and throw errors if not
            var studentCreated = _studentRepo.Create(_studConv.Convert(student));
            return _studConv.Convert(studentCreated);
        }

        public StudentBO Delete(int id) {
            var studentDeleted = _studentRepo.Delete(id);
            return _studConv.Convert(studentDeleted);
        }

        public StudentBO Get(int id) {
            var student = _studConv.Convert(_studentRepo.Get(id));
            student.Courses = _courseRepo.GetAllById(student.CourseIds).Select(c => _crsConv.Convert(c)).ToList();
            return student;
        }

        public List<StudentBO> GetAll() {
            var students = _studentRepo.GetAll();
            return students.Select(s => _studConv.Convert(s)).ToList();
        }

        public StudentBO Update(StudentBO student) {
            var updatedStudent = _studentRepo.Update(_studConv.Convert(student));
            return _studConv.Convert(updatedStudent);
        }
    }
}
