using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services.Students.dto
{
    public class SourceParam
    {

        [Required]
        public string SourceId { get; set; }
        [MaxLength(200)]
        public string SourceNumber { get; set; }

        public string SourceName { get; set; }
    }
}
