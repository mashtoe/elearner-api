using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELearner.Core.DomainService;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;

namespace ELearner.Core.ApplicationService.Services {

    // Services are part of the onions core. The core contains classes that the outer layers can depend on
    public class StudentService : IStudentService {

        /*readonly IStudentRepository _studentRepo;
        readonly ICourseRepository _courseRepo;*/
        readonly StudentConverter _studConv;
        readonly CourseConverter _crsConv;
        //readonly IUnitOfWork _uow;
        readonly IDataAccessFacade _facade;

        public StudentService(IDataAccessFacade facade) {
            _studConv = new StudentConverter();
            _crsConv = new CourseConverter();
            _facade = facade;
            //_uow = uow;
        }
        public StudentBO New() {
            return new StudentBO();
        }

        public StudentBO Create(StudentBO student) {
            // TODO check if entity is valid, and throw errors if not
            using (var uow = _facade.UnitOfWork)
            {
                var studentCreated = uow.StudentRepo.Create(_studConv.Convert(student));
                uow.Complete();
                return _studConv.Convert(studentCreated);
            }
        }

        public StudentBO Delete(int id) {
            using (var uow = _facade.UnitOfWork) {
                var studentDeleted = uow.StudentRepo.Delete(id);
                uow.Complete();
                return _studConv.Convert(studentDeleted);
            }
        }

        public StudentBO Get(int id) {
            using (var uow = _facade.UnitOfWork) {
                var student = _studConv.Convert(uow.StudentRepo.Get(id));
                student.Courses = uow.CourseRepo.GetAllById(student.CourseIds).Select(c => _crsConv.Convert(c)).ToList();
                return student;
            }
        }

        public List<StudentBO> GetAll() {
            using (var uow = _facade.UnitOfWork) {
                var students = uow.StudentRepo.GetAll();
                return students.Select(s => _studConv.Convert(s)).ToList();
            }
        }

        public StudentBO Update(StudentBO student) {
            using (var uow = _facade.UnitOfWork) {
                var updatedStudent = uow.StudentRepo.Update(_studConv.Convert(student));
                uow.Complete();
                return _studConv.Convert(updatedStudent);
            }
        }
    }
}
