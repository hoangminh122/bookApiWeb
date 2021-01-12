using bookApiWeb.Models;
using bookApiWeb.Models.Students;
using bookApiWeb.Repositories.Students;
using bookApiWeb.Services.Students.dto;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services.Students
{
    public class StudentServices : IStudentRepository
    {
        private readonly NoteContext _context = null;

        public StudentServices(IOptions<Settings> settings)
        {
            _context = new NoteContext(settings);
        }

        public async Task AddStudent(Student item)
        {
            try
            {
                await _context.Students.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            try
            {
                return await _context.Students.Find(_=>true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Student> GetStudent(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAllStudent()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveStudent(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudent(string id, Student item)
        {
            throw new NotImplementedException();
        }
    }
}
