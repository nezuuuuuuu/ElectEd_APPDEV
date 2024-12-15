using ElectEd.DTO;

namespace ElectEd.Services.Student
{
    public interface IStudentInfoService
    {
        Task<List<StudentDtoWithId>> GetStudents();

        StudentDtoWithId? GetStudentById(string id);
    }
}
