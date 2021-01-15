using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookApiWeb.Shares.Swaggers
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if(operation.OperationId == "Post")
            {
                operation.Parameters.Clear();
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "UploadFile1",
                    Schema = new OpenApiSchema() { Type = "file" },
                    Required =true,
                    In = ParameterLocation.Header,
                    Description = "Upload File"
                });

                
            }
        }
    }
}
