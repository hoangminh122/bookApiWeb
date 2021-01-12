﻿using bookApiWeb.Models;
using bookApiWeb.Services.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNote(string id);
        Task<IEnumerable<Note>> GetNote(string bodyText, DateTime updatedFrom, long headerSizeLimit);
        Task<Note> AddNote(Note item);
        Task<bool> RemoveNote(string id);
        Task UpdateNote(string id, Note item);
        Task<bool> UpdateNoteDocument(string id, string body);
        Task<bool> RemoveAllNotes();


    }
}
