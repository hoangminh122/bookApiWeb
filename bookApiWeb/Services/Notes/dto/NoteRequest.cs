using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services.dto
{
    public class NoteRequest
    {
        //[Required]
        public string Id { get; set; }
        //[MaxLength(200)]
        public string Body { get; set; }

      //  public DateTime UpdatedOn { get; set; }

      //  public NoteImageParam HeadImage { get; set; } = null;

       // public int UserId { get; set; } = 0;
    }
}
