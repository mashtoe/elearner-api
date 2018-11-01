using ELearner.Core.DomainService;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.Entities;
using ELearner.Infrastructure.Static.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Infrastructure.Static.Data.UOW {
    class UnitOfWorkStatic : IUnitOfWork {

        public IStudentRepository StudentRepo { get; }
        public ICourseRepository CourseRepo { get; }

        private bool saveChanges;
        readonly FakeDB _db;
        readonly StaticDbRollback _rollbackDb;

        public UnitOfWorkStatic() {
            _db = FakeDB.GetInstance();
            _rollbackDb = new StaticDbRollback(_db);

            StudentRepo = new StudentRepository(_db);
            CourseRepo = new CourseRepository(_db);
        }

        public int Complete() {
            saveChanges = true;
            return 0;
        }

        public void Dispose() {
            if (saveChanges == false) {
                // rollback
                _rollbackDb.Rollback(_db);
            }
        }

        private class StaticDbRollback {
            private List<Course> courses;
            private List<Student> students;
            private List<StudentCourse> studentCourses;


            public StaticDbRollback(FakeDB db) {

                students = new List<Student>(db.Students);
                courses = new List<Course>(db.Courses);
                studentCourses = new List<StudentCourse>(db.StudentCourses);
            }

            public void Rollback(FakeDB db) {
                db.Students = students;
                db.Courses = courses;
                db.StudentCourses = studentCourses;
            }
        }
    }
}
