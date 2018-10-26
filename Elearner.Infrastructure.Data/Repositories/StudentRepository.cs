//using Elearner.Core.DomainService;

using ELearner.Core.DomainService;
using ELearner.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Elearner.Infrastructure.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        readonly ElearnerAppContext _context;
        public StudentRepository(ElearnerAppContext context)
        {
            _context = context;
        }
        public Student Create(Student entity){
            var stud = _context.Students.Add(entity).Entity;
            _context.SaveChanges();
            return stud;
        }
        public Student Get(int id)
        {
            return _context.Students.Include(s => s.Courses).FirstOrDefault(stud => stud.Id == id);
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
            _context.SaveChanges();
            return stud;
        }
        //Delete Data
        public Student Delete(int id)
        {
            var studRemoved = Get(id);
            _context.Remove(studRemoved);
            _context.SaveChanges();
            return studRemoved;
        }

        public IEnumerable<Student> GetAllById(params int[] ids) {
            return _context.Students.Where(e => ids.Contains(e.Id));
        }
    }
}
