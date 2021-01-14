using bookApiWeb.Models.Students;
using bookApiWeb.Repositories.Students;
using bookApiWeb.Services.Students.dto;
using bookApiWeb.Shares.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _studentRepository.GetAllStudents());
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentParam newStudent)
        {
            var student = await _studentRepository.AddStudent(newStudent);
            if (student)
                return Created("success" ,new { success = 1} );
            return  BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var note = await _studentRepository.GetStudent(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(new Response<Student>(note));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            //fix no clean
            var note = await _studentRepository.GetStudent(id);
            if (note == null)
            {
                return NotFound();
            }
            await _studentRepository.RemoveStudent(id);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, StudentParam newNote)
        {
            var result = await _studentRepository.UpdateStudent(id, newNote);
            if (!result)
            {
                return BadRequest();
            }
            return Ok( new { success = 1 });
        }
    }
}
