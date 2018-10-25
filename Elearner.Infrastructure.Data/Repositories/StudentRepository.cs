using Elearner.Core.DomainService;

namespace Elearner.Infrastructure.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        readonly ElearnerAppContext _context;
        public StudentRepository(ElearnerAppContext context)
        {
            _context = context;
        }

        public Student Get(int id)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<Student> GetAll()
        {
            throw new System.NotImplementedException();
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
