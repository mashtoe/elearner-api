using ELearner.Core.DomainService;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.Entities;
using ELearner.Infrastructure.Static.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Infrastructure.Static.Data.UOW {
    public class UnitOfWorkFakeDB : IUnitOfWork {

        public IUserRepository UserRepo { get; }
        public ICourseRepository CourseRepo { get; }

        private bool saveChanges;
        readonly FakeDB _db;
        readonly StaticDbRollback _rollbackDb;

        public UnitOfWorkFakeDB() {
            _db = FakeDB.GetInstance();
            _rollbackDb = new StaticDbRollback(_db);

            UserRepo = new UserRepository(_db);
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
            private List<User> students;
            private List<UserCourse> studentCourses;


            public StaticDbRollback(FakeDB db) {

                students = new List<User>(db.Users);
                courses = new List<Course>(db.Courses);
                studentCourses = new List<UserCourse>(db.UserCourses);
            }

            public void Rollback(FakeDB db) {
                db.Users = students;
                db.Courses = courses;
                db.UserCourses = studentCourses;
            }
        }
    }
}
