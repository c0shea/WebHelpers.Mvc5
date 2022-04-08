using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5
{
    public class UnitConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor)) 
                || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null) return null;
            if (value is string stringValue)
            {
                string textValue = stringValue.Trim();
                if (textValue.Length == 0) return Unit.Empty;
                if (culture != null) return Unit.Parse(textValue, culture);
                return Unit.Parse(textValue, CultureInfo.CurrentCulture);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if(destinationType == typeof(string))
            {
                if (value == null || ((Unit)value).IsEmpty) return string.Empty;
                return ((Unit)value).ToString(culture);
            }
            if(destinationType == typeof(InstanceDescriptor) && value != null)
            {
                Unit u = (Unit)value;
                MemberInfo member;
                object[] args = null;

                if(u.IsEmpty)
                {
                    member = typeof(Unit).GetField("Empty");
                } 
                else
                {
                    member = typeof(Unit).GetConstructor(new Type[] { typeof(double), typeof(UnitType) });
                    args = new object[] { u.Value, u.Type };
                }

                if (member != null) return new InstanceDescriptor(member, args);
                return null;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
