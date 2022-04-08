using Microsoft.DotNet.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelpers.Mvc5
{
    [TypeConverter(typeof(UnitConverter))]
    [Serializable]
    public struct Unit
    {
        public static readonly Unit Empty = new Unit();

        internal const int MaxValue = 32767;
        internal const int MinValue = -32768;

        private readonly UnitType type;
        private readonly double value;

        public Unit(int value)
        {
            if (value < MinValue || value > MaxValue)
                throw new ArgumentOutOfRangeException(nameof(value));
            this.value = value;
            this.type = UnitType.Pixel;
        }

        public Unit(double value)
        {
            if (value < MinValue || value > MaxValue)
                throw new ArgumentOutOfRangeException(nameof(value));
            this.value = (int)value;
            this.type = UnitType.Pixel;
        }

        public Unit(double value, UnitType type)
        {
            if(value < MinValue || value > MaxValue)
                throw new ArgumentOutOfRangeException(nameof(value));
            this.value = (type == UnitType.Pixel) ? value : value;
            this.type = type;
        }

        public Unit(string value) : this(value, CultureInfo.CurrentCulture, UnitType.Pixel) { }

        public Unit(string value, CultureInfo culture) : this(value, culture, UnitType.Pixel) { }

        internal Unit(string value, CultureInfo culture, UnitType defaultType)
        {
            if(string.IsNullOrEmpty(value))
            {
                this.value = 0;
                this.type = 0;
            }
            else
            {
                if (culture == null) culture = CultureInfo.CurrentCulture;
                string trimLower = value.Trim().ToLower(CultureInfo.InvariantCulture);
                int len = trimLower.Length;

                int lastDigit = -1;
                for(int i = 0; i < len; i++)
                {
                    char ch = trimLower[i];
                    if ((ch < '0' || ch > '9') && ch != '-' && ch != '.' && ch != ',') break;
                    lastDigit = i;
                }
                if (lastDigit == -1)
                    throw new FormatException();
                this.type = (lastDigit < len - 1) ? GetTypeFromString(trimLower[(lastDigit + 1)..].Trim()) : defaultType;

                string numericPart = trimLower.Substring(0, lastDigit + 1);
                try
                {
                    TypeConverter converter = new SingleConverter();
                    this.value = (float)converter.ConvertFromString(null, culture, numericPart);
                    if(type == UnitType.Pixel)
                    {
                        this.value = (int)this.value;
                    }
                } 
                catch
                {
                    throw new FormatException();
                }

                if (this.value < MinValue || this.value > MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(this.value));
            }
        }

        public bool IsEmpty
        {
            get
            {
                return type == 0;
            }
        }

        public UnitType Type
        {
            get
            {
                if (!IsEmpty) return type;
                return UnitType.Pixel;
            }
        }

        public double Value
        {
            get
            {
                return value;
            }
        }

        public override int GetHashCode()
        {
            var combiner = HashCodeCombiner.Start();
            combiner.Add(type.GetHashCode());
            combiner.Add(value.GetHashCode());
            return combiner.CombinedHash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Unit)) return false;
            Unit u = (Unit)obj;
            return (u.type == type && u.value == value);
        }

        public static bool operator ==(Unit left, Unit right)
        {
            return (left.type == right.type && left.value == right.value);
        }

        public static bool operator !=(Unit left, Unit right)
        {
            return (left.type != right.type || left.value != right.value);
        }

        private static string GetStringFromType(UnitType type)
        {
            return type switch
            {
                UnitType.Pixel => "px",
                UnitType.Point => "pt",
                UnitType.Pica => "pc",
                UnitType.Inch => "in",
                UnitType.Mm => "mm",
                UnitType.Cm => "cm",
                UnitType.Percentage => "%",
                UnitType.Em => "em",
                UnitType.Ex => "ex",
                _ => string.Empty,
            };
        }

        private static UnitType GetTypeFromString(string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                return value switch
                {
                    "px" => UnitType.Pixel,
                    "pt" => UnitType.Point,
                    "pc" => UnitType.Pica,
                    "in" => UnitType.Inch,
                    "mm" => UnitType.Mm,
                    "cm" => UnitType.Cm,
                    "%" => UnitType.Percentage,
                    "em" => UnitType.Em,
                    "ex" => UnitType.Ex,
                    _ => throw new ArgumentOutOfRangeException(nameof(value))
                };
            }
            return UnitType.Pixel;
        }

        public static Unit Parse(string s)
        {
            return new Unit(s, CultureInfo.CurrentCulture);
        }

        public static Unit Parse(string s, CultureInfo culture)
        {
            return new Unit(s, culture);
        }

        public static Unit Percentage(double n)
        {
            return new Unit(n, UnitType.Percentage);
        }

        public static Unit Pixel(int n)
        {
            return new Unit(n);
        }

        public static Unit Point(int n)
        {
            return new Unit(n, UnitType.Point);
        }

        public override string ToString()
        {
            return ToString((IFormatProvider)CultureInfo.CurrentCulture);
        }

        public string ToString(CultureInfo culture)
        {
            return ToString((IFormatProvider)culture);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            if (IsEmpty)
            {
                return string.Empty;
            }

            string valuePart = (type == UnitType.Pixel) ? ((int)value).ToString(formatProvider) : ((float)value).ToString(formatProvider);
            return valuePart + GetStringFromType(type);
        }

        public static implicit operator Unit(int n)
        {
            return Pixel(n);
        }
    }
}
