using ElectEd.DTO;

namespace ElectEd.Repositories.Student
{
    public interface IStudentInfoRepository
    {
        IQueryable<Models.Student> GetStudents();

    }
}
