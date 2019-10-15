using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebHelpers.Mvc5.Enum
{
    /// <summary>
    /// ASP.NET handler that renders Enums as a frozen object in JavaScript to promote re-usability
    /// between the server and client.
    /// </summary>
    public class EnumHandler : IHttpHandler
    {
        private static readonly TimeSpan CacheFor = TimeSpan.FromDays(7);
        private static readonly Lazy<string> JavaScript = new Lazy<string>(GetJavaScript);
        private static string _globalVariableName = "Enums";

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="IHttpHandler"/> instance.
        /// </summary>
        public bool IsReusable => true;

        /// <summary>
        /// Gets the URL to the <see cref="EnumHandler"/>, with a unique hash in the URL.
        /// The hash will change every time an Enum changes.
        /// </summary>
        public static string HandlerUrl => GetHandlerUrl();

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

        /// <summary>
        /// Handles a HTTP request.
        /// </summary>
        /// <param name="context">
        /// An <see cref="HttpContext" /> object that provides references to the intrinsic server objects
        /// (for example, Request, Response, Session, and Server) used to service HTTP requests.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            SetCachingHeaders(context, JavaScript.Value);

            context.Response.ContentType = "text/javascript";
            context.Response.Write(JavaScript.Value);
        }

        /// <summary>
        /// Set the HTTP headers to cache the response.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="output">The output of the handler.</param>
        private static void SetCachingHeaders(HttpContext context, string output)
        {
            context.Response.Cache.SetETag(Hash(output));
            context.Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            context.Response.Cache.SetExpires(DateTime.Now + CacheFor);
            context.Response.Cache.SetMaxAge(CacheFor);
            context.Response.Cache.SetVaryByCustom("*");
        }

        private static string GetJavaScript()
        {
            var sb = new StringBuilder("window.");
            sb.Append(GlobalVariableName);
            sb.Append(" = Object.freeze({");

            var enumsFound = GetTypesWithExposeAttribute();

            for (int i = 0; i < enumsFound.Count; i++)
            {
                AppendEnumJson(sb, enumsFound[i]);

                if (i < enumsFound.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("});");

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
                sb.Append(":");

                // We can't assume a cast to an int since an enum can be any integral type and will throw an InvalidCastException.
                sb.Append(Convert.ChangeType(value, Type.GetTypeCode(value.GetType())));

                if (i < values.Length)
                {
                    sb.Append(",");
                }
            }

            sb.Append("}");
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
        /// Gets the URL to the <see cref="EnumHandler"/>, with a unique hash in the URL.
        /// The hash will change every time an Enum changes.
        /// </summary>
        private static string GetHandlerUrl()
        {
            var hash = Hash(JavaScript.Value);

            return string.Join("/", VirtualPathUtility.ToAbsolute("~/WebHelpers.axd"), hash, "webhelpers.js");
        }
    }
}
