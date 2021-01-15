using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services.Students.dto
{
    public class StudentParam
    {

        //[Required]
        public string StudentId { get; set; }
        //[MaxLength(200)]
        public string LastName { get; set; }

    }
}
