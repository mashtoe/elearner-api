using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services {

    // Services are part of the onions core. The core contains classes that the outer layers can depend on
    public class StudentService : IStudentService {

        readonly StudentConverter _studConv;
        readonly CourseConverter _crsConv;
        readonly IUnitOfWork _uow;

        public StudentService(IUnitOfWork uow) {
            _studConv = new StudentConverter();
            _crsConv = new CourseConverter();
            _uow = uow;
        }
        public StudentBO New() {
            return new StudentBO();
        }

        public StudentBO Create(StudentBO student) {
            // TODO check if entity is valid, and throw errors if not
            using (_uow)
            {
                //var studentCreated = uow.StudentRepo.Create(_studConv.Convert(student));
                //student.Username += "1";
                //if (studentCreated != null) {
                //    throw new InvalidOperationException();
                //}
                var studentCreated = _uow.StudentRepo.Create(_studConv.Convert(student));

                _uow.Complete();
                return _studConv.Convert(studentCreated);
            }
        }

        public StudentBO Delete(int id) {
            using (_uow) {
                var studentDeleted = _uow.StudentRepo.Delete(id);
                _uow.Complete();
                return _studConv.Convert(studentDeleted);
            }
        }

        public StudentBO Get(int id) {
            using (_uow) {
                var student = _studConv.Convert(_uow.StudentRepo.Get(id));
                if (student != null) {
                    student.Courses = _uow.CourseRepo.GetAllById(student.CourseIds).Select(c => _crsConv.Convert(c)).ToList();
                }
                return student;
            }
        }

        public List<StudentBO> GetAll() {
            using (_uow) {
                var students = _uow.StudentRepo.GetAll();
                return students.Select(s => _studConv.Convert(s)).ToList();
            }
        }

        public StudentBO Update(StudentBO student) {
            using (_uow) {
                var updatedStudent = _uow.StudentRepo.Update(_studConv.Convert(student));
                _uow.Complete();
                return _studConv.Convert(updatedStudent);
            }
        }
    }
}
