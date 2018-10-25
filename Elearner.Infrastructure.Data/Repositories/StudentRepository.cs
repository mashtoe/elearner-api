//using Elearner.Core.DomainService;

using ELearner.Core.DomainService;
using ELearner.Core.Entity;
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
            return _context.Students.FirstOrDefault(stud => stud.Id == id);
        }
        public IEnumerable<Student> GetAll()
        {
            return _context.Students;
        }
        //Update Data
        public Student Update(Student entity)
        {
            throw new System.NotImplementedException();
        }
        //Delete Data
        public Student Delete(int id)
        {
            throw new System.NotImplementedException();
        }

    }
}
