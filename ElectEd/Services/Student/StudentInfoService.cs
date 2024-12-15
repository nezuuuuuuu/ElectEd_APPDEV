using ElectEd.DTO;
using ElectEd.Repositories.Position;
using ElectEd.Repositories.Student;
using ElectEd.Services.Position;

namespace ElectEd.Services.Student
{
    public class StudentInfoService : IStudentInfoService
    {

        public readonly IStudentInfoRepository _studentInfoRepository; // Injected DbContext

        public StudentInfoService(IStudentInfoRepository context)
        {
            _studentInfoRepository = context;
        }

        StudentDtoWithId? IStudentInfoService.GetStudentById(int id)
        {
            var student = _studentInfoRepository.GetStudents()
                                     .FirstOrDefault(c => c.Id == id);

            if (student == null)
            {
                return null;
            }
            var studentDto = new StudentDtoWithId
            {
                Id = student.Id,
                StudentId = student.StudentId,
                Name = student.Name,
                Department = student.Department,

            };

            return studentDto;
        }

        Task<List<StudentDtoWithId>> IStudentInfoService.GetStudents()
        {
            var students = _studentInfoRepository.GetStudents()
                .Select(student => new StudentDtoWithId
                {
                    Id = student.Id,

                   StudentId= student.StudentId,
                   Name = student.Name, 
                   Department= student.Department,


                })
                .ToList();

            return Task.FromResult(students);
        }





    }
}
