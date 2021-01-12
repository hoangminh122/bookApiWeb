using bookApiWeb.Models.Students;
using bookApiWeb.Repositories.Students;
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
        public void Post(Student newStudent)
        {
            _studentRepository.AddStudent(newStudent);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(string id)
        //{
        //    var note = await _noteRepository.GetNote(id);
        //    if (note == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(note);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    //fix no clean
        //    var note = await _noteRepository.GetNote(id);
        //    if (note == null)
        //    {
        //        return NotFound();
        //    }
        //    await _noteRepository.RemoveNote(id);
        //    return NoContent();
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(string id, Note newNote)
        //{

        //    await _noteRepository.UpdateNote(id, newNote);
        //    return Ok("ok");
        //}
    }
}
