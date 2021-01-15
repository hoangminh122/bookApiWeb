using bookApiWeb.Models;
using bookApiWeb.Models.Students;
using bookApiWeb.Repositories.Students;
using bookApiWeb.Services.Students.dto;
using bookApiWeb.Shares.Filters;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

        public async Task<bool> AddStudent(StudentParam item)
        {
            try
            {
                var student = new Student()
                {
                    LastName = item.LastName,
                    StudentId = item.StudentId
                };
                await _context.Students.InsertOneAsync(student);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<PagedResponse<List<Student>>> GetAllStudents(StudentQueryInput filter)
        {
            try
            {
                //filter query
                FilterDefinition<Student> filterBody = Builders<Student>.Filter.Regex("last_name", new BsonRegularExpression(filter.LastName));
                BuildersFilterResponse<Student> filterResult =
                    new BuildersFilterResponse<Student>();
                filterResult.AddNewFilter(filterBody);
                //end filter query

                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                return new PagedResponse<List<Student>>(
                    await _context.Students.Find(filterResult.FilterResult)
                        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Limit(validFilter.PageSize)
                                .ToListAsync(), filter.PageNumber, filter.PageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Student> GetStudent(string id)
        {
            try
            {
                return await _context.Students.Find(student => student.InternalId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> RemoveAllStudent()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveStudent(string id)
        {
            var idObject = new ObjectId(id);
            try
            {
                DeleteResult actionResult =
                    await _context.Students.DeleteOneAsync(
                        Builders<Student>.Filter.Eq("_id", idObject));
                return actionResult.IsAcknowledged
                        && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateStudent(string id, StudentParam item)
        {
            try
            {
                var student = new Student()
                {
                    InternalId=id,
                    LastName = item.LastName,
                    StudentId = item.StudentId
                };
                var result = await _context.Students.ReplaceOneAsync(n => n.InternalId == id, student);
                if (result != null)
                    return true;
                return false;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
