using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5.Enum
{
    /// <summary>
    /// The collection of enums to include and exclude.
    /// </summary>
    public class EnumCollection
    {
        internal List<Type> TypesToInclude { get; } = new List<Type>();
        internal List<Type> TypesToExclude { get; } = new List<Type>();

        /// <summary>
        /// Include the specified enum type to be exposed in JavaScript.
        /// </summary>
        /// <param name="enumType">The enum to be exposed.</param>
        public EnumCollection Include(Type enumType)
        {
            TypesToInclude.Add(enumType);

            return this;
        }

        /// <summary>
        /// Exclude the specified enum type from the JavaScript.
        /// </summary>
        /// <param name="enumType">The enum to exclude from being exposed.</param>
        public EnumCollection Exclude(Type enumType)
        {
            TypesToExclude.Add(enumType);

            return this;
        }
    }
}
