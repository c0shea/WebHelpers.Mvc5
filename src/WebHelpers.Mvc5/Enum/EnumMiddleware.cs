using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.Enum
{
    /// <summary>
    /// ASP.NET handler that renders Enums as a frozen object in JavaScript to promote re-usability
    /// between the server and client.
    /// </summary>
    public class EnumMiddleware
    {
        private static readonly TimeSpan CacheFor = TimeSpan.FromDays(7);
        private static readonly Lazy<string> JavaScript = new Lazy<string>(GetJavaScript);
        private static string _globalVariableName = "Enums";

        /// <summary>
        /// Gets the URL to the <see cref="EnumMiddleware"/>, with a unique hash in the URL.
        /// The hash will change every time an Enum changes.
        /// </summary>
        //public static string HandlerUrl => GetHandlerUrl();

        /// <summary>
        /// Allows you to fluently declare which enums should be exposed or excluded instead of decorating the enum with
        /// the <see cref="ExposeInJavaScriptAttribute"/>. This is useful for enums that reside in other
        /// libraries that you can't add the attribute to.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public static EnumCollection EnumsToExpose { get; } = new EnumCollection();

        /// <summary>
        /// Allows you change the name of the global variable that is created for the enums.
        /// It is Enums by default.
        /// </summary>
        public static string GlobalVariableName
        {
            get => _globalVariableName;
            set => _globalVariableName = value ?? throw new ArgumentNullException(nameof(value));
        }

        private readonly RequestDelegate _next;

        public EnumMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Handles a HTTP request.
        /// </summary>
        /// <param name="context">
        /// An <see cref="HttpContext" /> object that provides references to the intrinsic server objects
        /// (for example, Request, Response, Session, and Server) used to service HTTP requests.
        /// </param>
        public async Task InvokeAsync(HttpContext context)
        {
            SetCachingHeaders(context, JavaScript.Value);

            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                var response = context.Response.Body;
                context.Response.Body = stream;

                await _next.Invoke(context);

                stream.Seek(0, SeekOrigin.Begin);

                var body = await reader.ReadToEndAsync();
                context.Response.Clear();

                await context.Response.WriteAsync(body);
                await context.Response.WriteAsync(JavaScript.Value);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await context.Response.Body.CopyToAsync(response);
                context.Request.Body = response;
            }
        }

        /// <summary>
        /// Set the HTTP headers to cache the response.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="output">The output of the handler.</param>
        private static void SetCachingHeaders(HttpContext context, string output)
        {
            context.Response.Headers["ETag"] = Hash(output);
            context.Response.Headers["Cache-Control"] = $"no-cache, private, max-age={CacheFor}";
            context.Response.Headers["Expires"] = (DateTime.Now + CacheFor).ToString();
            context.Response.Headers["Vary"] = "*";
        }

        private static string GetJavaScript()
        {
            var sb = new StringBuilder("<script> window.");
            sb.Append(GlobalVariableName);
            sb.Append(" = Object.freeze({");

            var enumsFound = GetTypesWithExposeAttribute();

            for (int i = 0; i < enumsFound.Count; i++)
            {
                AppendEnumJson(sb, enumsFound[i]);

                if (i < enumsFound.Count - 1)
                {
                    sb.Append(',');
                }
            }

            sb.Append("}); </script>");

            return sb.ToString();
        }

        private static List<Type> GetTypesWithExposeAttribute()
        {
            var enumsFound = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                enumsFound.AddRange(GetLoadableTypes(assembly).Where(t => t.IsEnum &&
                                                                          t.GetCustomAttributes(typeof(ExposeInJavaScriptAttribute), inherit: true).Any()));
            }

            enumsFound.AddRange(EnumsToExpose.TypesToInclude);
            enumsFound.RemoveAll(e => EnumsToExpose.TypesToExclude.Contains(e));

            return enumsFound.Distinct().ToList();
        }

        /// <summary>
        /// The call to <see cref="Assembly.GetTypes"/> sometimes throws a <see cref="ReflectionTypeLoadException"/>
        /// when one or more of the inner types reference an assembly that isn't loaded and can't be found.
        /// This works around that by only finding types that are loadable at runtime.
        /// </summary>
        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null);
            }
        }

        private static void AppendEnumJson(StringBuilder sb, Type type)
        {
            sb.Append(type.Name);
            sb.Append(":{");

            var i = 0;
            var values = System.Enum.GetValues(type);

            foreach (var value in values)
            {
                i++;
                sb.Append(System.Enum.GetName(type, value));
                sb.Append(';');

                // We can't assume a cast to an int since an enum can be any integral type and will throw an InvalidCastException.
                sb.Append(Convert.ChangeType(value, Type.GetTypeCode(value.GetType())));

                if (i < values.Length)
                {
                    sb.Append(',');
                }
            }

            sb.Append('}');
        }

        /// <summary>
        /// Calculate the SHA1 hash of the specified content.
        /// </summary>
        /// <param name="content">The content to hash.</param>
        private static string Hash(string content)
        {
            using (var sha1 = SHA1.Create())
            {
                var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(content));
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
                return hash;
            }
        }

        /// <summary>
        /// Gets the URL to the <see cref="EnumMiddleware"/>, with a unique hash in the URL.
        /// The hash will change every time an Enum changes.
        /// </summary>
        //private static string GetHandlerUrl()
        //{
        //    var hash = Hash(JavaScript.Value);

        //    return string.Join("/", VirtualPathUtility.ToAbsolute("~/WebHelpers.axd"), hash, "webhelpers.js");
        //}
    }
}
