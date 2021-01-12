using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services.dto
{
    public class NoteImageParam
    {
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public long ImageSize { get; set; }

    }
}
