using bookApiWeb.Models.Students;
using bookApiWeb.Services.Notes.dto;
using bookApiWeb.Services.Students.dto;
using bookApiWeb.Shares.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Repositories.Students
{
    public interface IStudentRepository
    {
        Task<PagedResponse<List<Student>>> GetAllStudents(StudentQueryInput filter);
        Task<Student> GetStudent(string id);
        Task<bool> AddStudent(StudentParam item);
        Task<bool> RemoveStudent(string id);
        Task<bool> UpdateStudent(string id, StudentParam item);
        Task<bool> RemoveAllStudent();
    }
}
