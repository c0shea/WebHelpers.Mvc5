using System;

namespace WebHelpers.Mvc5.Enum
{
    /// <summary>
    /// Exposes enums in the <c>Enums</c> frozen object in JavaScript.
    /// Enums that are not decorated with this attribute are not exposed.
    /// </summary>
    public class ExposeInJavaScriptAttribute : Attribute
    {
    }
}
