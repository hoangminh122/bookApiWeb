using bookApiWeb.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Repositories.Students
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudent(string id);
        Task AddStudent(Student item);
        Task<bool> RemoveStudent(string id);
        Task UpdateStudent(string id, Student item);
        Task<bool> RemoveAllStudent();
    }
}
