﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.Serialization;

namespace HRManager.Models.Enums
{
    public class DescribeEnumMemberValues : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();

                foreach (var member in context.Type.GetMembers())
                {
                    var memberAttr = member.GetCustomAttributes(typeof(EnumMemberAttribute), false).FirstOrDefault();
                    if (memberAttr != null)
                    {
                        var attr = (EnumMemberAttribute)memberAttr;
                        schema.Enum.Add(new OpenApiString(attr.Value));
                    }
                }
            }
        }
    }
}
