using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EFcoreLearningProject
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) ||
                            (p.ParameterType.IsGenericType &&
                             p.ParameterType.GetGenericTypeDefinition() == typeof(IFormFile)));

            foreach (var param in fileParams)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = param.Name,
                   
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string", Format = "binary" }
                });
            }
        }
    }
}
