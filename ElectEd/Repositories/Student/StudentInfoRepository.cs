using ElectEd.DTO;

namespace ElectEd.Repositories.Student
{
    public class StudentInfoRepository : IStudentInfoRepository
    {

        private readonly ApplicationDbContext _context; // Injected DbContext

        public StudentInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

         IQueryable<Models.Student> IStudentInfoRepository.GetStudents()
        {
            return _context.Students;
        }

       
    }
}
