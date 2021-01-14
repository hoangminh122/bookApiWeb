using bookApiWeb.Models;
using bookApiWeb.Repositories;
using bookApiWeb.Services.dto;
using bookApiWeb.Services.Notes.dto;
using bookApiWeb.Shares.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services
{
    public class NotesServices : INoteRepository
    {
        private readonly NoteContext _context = null;

        public NotesServices(IOptions<Settings> settings)
        {
            _context = new NoteContext(settings);
        }
        public async Task<Note> AddNote(Note item)
        {
            try
            {
                #region test
                //var newNote = new Note
                //{
                //    Id = item.Id,
                //    Body = item.Body,
                //    UpdatedOn = DateTime.Now,
                //    //HeadImage = new NoteImage{
                //    //    Url = newNote.HeadImage.Url,
                //    //    ThumbnailUrl = newNote.HeadImage.ThumbnailUrl,
                //    //    ImageSize = newNote.HeadImage.ImageSize
                //    //},
                //    UserId = item.UserId
                //};
                #endregion
                await _context.Notes.InsertOneAsync(item);
                return item;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResponse<List<Note>>> GetAllNotes(NoteQueryInput filter)
        {
            try
            {
                FilterDefinition<Note>  filter2 = Builders<Note>.Filter.Regex("body", new BsonRegularExpression(filter.Body));
                FilterDefinition<Note> bodyFilter = Builders<Note>.Filter.Eq(x => x.Body, filter.Body);
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                return new PagedResponse<List<Note>>(
                    await _context.Notes.Find(filter2)
                        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Limit(validFilter.PageSize)
                                .ToListAsync(), filter.PageNumber, filter.PageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> GetNote(string id)
        {
            try
            {
                return await _context.Notes.Find(note => note.InternalId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task<IEnumerable<Note>> GetNote(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAllNotes()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveNote(string id)
        {
            var idObject = new ObjectId(id);
            try
            {
                DeleteResult actionResult =
                    await _context.Notes.DeleteOneAsync(
                        Builders<Note>.Filter.Eq("_id", idObject));
                return actionResult.IsAcknowledged
                        && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateNote(string id, Note item)
        {
            try
            {
                #region test
                //find item by id
                //var noteFind = await _context.Notes.Find(note => note.InternalId == idObject).FirstOrDefaultAsync();
                //if (noteFind != null)
                //{

                //var newNote = new Note
                //{
                //    Id = item.Id,
                //    Body = item.Body,
                //    UpdatedOn = DateTime.Now,
                //    //HeadImage = new NoteImage{
                //    //    Url = newNote.HeadImage.Url,
                //    //    ThumbnailUrl = newNote.HeadImage.ThumbnailUrl,
                //    //    ImageSize = newNote.HeadImage.ImageSize
                //    //},
                //    UserId = item.UserId

                //};
                //NoteImage headImage = null;
                //if (noteFind.HeadImage != null)
                //{
                //    headImage = new NoteImage
                //    {
                //        Url = noteFind.HeadImage.Url,
                //        ThumbnailUrl = noteFind.HeadImage.ThumbnailUrl,
                //        ImageSize = noteFind.HeadImage.ImageSize
                //    };
                //}

                //var noteUpdate = new Note
                //{
                //    Id = noteFind.Id,
                //    Body = noteFind.Body,
                //    UpdatedOn = DateTime.Now,
                //    HeadImage = headImage,
                //    UserId = noteFind.UserId
                //};
                #endregion
                var result = await _context.Notes.ReplaceOneAsync(n => n.InternalId == id, item);
                if (result != null)
                    return true;
                return false;
                #region
                //}
                //else
                //    return false;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> UpdateNoteDocument(string id, string body)
        {
            throw new NotImplementedException();
        }
    }
}
