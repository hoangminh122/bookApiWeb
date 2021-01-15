using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Services.Students.dto
{
    public class StudentQueryInput
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string LastName { get; set; } = string.Empty;
    }
}


