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

        public ISectionRepository SectionRepo => throw new NotImplementedException();

        readonly FakeDB _db;
        readonly StaticDbRollback _rollbackDb;

        public UnitOfWorkFakeDB() {
            _db = FakeDB.GetInstance();
            _rollbackDb = new StaticDbRollback(_db);

            UserRepo = new UserRepository(_db);
            CourseRepo = new CourseRepository(_db);
        }

        public int Complete() {
            _rollbackDb.Save(_db);
            return 0;
        }

        public void Dispose() {
            //_rollbackDb.Rollback(_db);
        }

        private class StaticDbRollback {
            /*
            private List<Course> courses;
            private List<User> students;
            private List<UserCourse> studentCourses;
            */

            public StaticDbRollback(FakeDB db) {
                db.UsersNotSaved = new List<User>(db.Users);
                db.CoursesNotSaved = new List<Course>(db.Courses);
                db.UserCoursesNotSaved = new List<UserCourse>(db.UserCourses);
                /*
                students = new List<User>(db.Users);
                courses = new List<Course>(db.Courses);
                studentCourses = new List<UserCourse>(db.UserCourses);*/
            }

            public void Save(FakeDB db) {
                db.Users = db.UsersNotSaved;
                db.Courses = db.CoursesNotSaved;
                db.UserCourses = db.UserCoursesNotSaved;
                /*
                db.Users = students;
                db.Courses = courses;
                db.UserCourses = studentCourses;*/
            }
        }
    }
}
