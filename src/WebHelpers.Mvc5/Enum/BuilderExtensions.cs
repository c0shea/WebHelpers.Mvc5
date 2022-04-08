using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.Enum
{
    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseEnum(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware(typeof(EnumMiddleware));
        }
    }
}
