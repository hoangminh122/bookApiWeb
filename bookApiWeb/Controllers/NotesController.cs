using bookApiWeb.Configurations.Jwt;
using bookApiWeb.Models;
using bookApiWeb.Repositories;
using bookApiWeb.Services.dto;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController :ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        /// <summary>
        /// Get All Note
        /// </summary>
        /// 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           
            //throw new Exception();
            return Ok(await _noteRepository.GetAllNotes());
        }

        [HttpPost]
        public async Task<IActionResult> Post(NoteRequest newNote)
        {
            Note note = new Note
            {
                Id = newNote.Id,
                Body =  newNote.Body
            };

            var noteResult = await _noteRepository.AddNote(note);
            if (noteResult != null)
                return Ok(noteResult);
            else
                return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var note = await _noteRepository.GetNote(id);
            if(note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            //fix no clean
            var note = await _noteRepository.GetNote(id);
            if(note == null)
            {
                return NotFound();
            }
             await _noteRepository.RemoveNote(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id,Note newNote)
        {
            
            await _noteRepository.UpdateNote(id, newNote);
           
    
            return Ok("{ success:1,status:201}");
        }


    }
}
