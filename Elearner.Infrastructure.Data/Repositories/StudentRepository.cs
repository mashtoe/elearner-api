//using Elearner.Core.DomainService;

using ELearner.Core.DomainService;
using ELearner.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Elearner.Infrastructure.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        readonly ElearnerAppContext _context;

        // cant use dependancy injection here since we neewd to call saveChanges on the context from the unitofwork
        // so we need to parse the context from the unitofwork
        public StudentRepository(ElearnerAppContext context)
        {
            _context = context;
        }
        public Student Create(Student entity) {
            _context.Students.Add(entity);
            return entity;
        }

        public Student Get(int id)
        {
            var student = _context.Students.Include(s => s.Courses).FirstOrDefault(stud => stud.Id == id);
            return student;
        }
        public IEnumerable<Student> GetAll()
        {
            return _context.Students;
        }
        //Update Data
        public Student Update(Student entity)
        {
            //var stud = _context.Students.Update(entity).Entity;
            var stud = Get(entity.Id);
            stud.Username = entity.Username;
            return stud;
        }
        //Delete Data
        public Student Delete(int id)
        {
            var studRemoved = Get(id);
            _context.Remove(studRemoved);
            return studRemoved;
        }

        public IEnumerable<Student> GetAllById(IEnumerable<int> ids) {
            var students = _context.Students.Where(e => ids.Contains(e.Id));
            return students;
        }
    }
}
