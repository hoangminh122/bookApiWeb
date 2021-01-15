using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services.Notes.dto
{
    public class NoteQueryInput
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string Body { get; set; } = string.Empty;
    }
}
