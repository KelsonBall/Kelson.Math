using System;

namespace Kelson.Physics
{
    public readonly struct Unit
    {
        public static readonly Func<double, double> IDENTITY = i => i;

        private readonly sbyte[] degrees;
        public readonly Func<double, double> ToSI;
        public readonly Func<double, double> FromSI;

        public int this[SI unit] => degrees[(int)unit];

        private sbyte this[int unit] => degrees[unit];

        private Unit(sbyte[] values) => (degrees, ToSI, FromSI) = (values, IDENTITY, IDENTITY);

        private Unit(sbyte[] values, Func<double, double> toSi, Func<double, double> fromSi) => (degrees, ToSI, FromSI) = (values, toSi, fromSi);

        private Unit(Unit from, SI mod, int amount)
        {
            degrees = new sbyte[7];
            for (int i = 0; i < 7; i++)
                degrees[i] = (sbyte)(i == (int)mod ? from[i] + amount : from[i]);
            ToSI = from.ToSI;
            FromSI = from.FromSI;
        }

        public Unit(Unit from, Func<double, double> toSi, Func<double, double> fromSi) =>
            (degrees, ToSI, FromSI) = (from.degrees, i => toSi(from.ToSI(i)), i => fromSi(from.FromSI(i)));

        public Unit CoreUnits() => new Unit(this.degrees, IDENTITY, IDENTITY);

        public static Unit operator *(Unit a, Unit b)
        {
            sbyte[] next = new sbyte[7];
            for (int i = 0; i < 7; i++)
                next[i] = a[i];
            for (int i = 0; i < 7; i++)
                next[i] += b[i];
            double factor = a.ToSI(1) * b.ToSI(1);
            return new Unit(next, i => i / factor, i => i * factor);
        }

        public static Unit operator /(Unit a, Unit b)
        {
            sbyte[] next = new sbyte[7];
            for (int i = 0; i < 7; i++)
                next[i] = a[i];
            for (int i = 0; i < 7; i++)
                next[i] -= b[i];            
            return new Unit(next, i => i / (b.ToSI(i) / a.ToSI(i)), i => i * (b.ToSI(i) / a.ToSI(i)));
        }

        public static bool operator ==(Unit a, Unit b)
        {
            for (int i = 0; i < 7; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }

        public static bool operator !=(Unit a, Unit b)
        {
            for (int i = 0; i < 7; i++)
                if (a[i] != b[i])
                    return true;
            return false;
        }

        public static Unit operator !(Unit a)
        {
            sbyte[] next = new sbyte[7];
            for (int i = 0; i < 7; i++)
                next[i] = (sbyte)-a[i];
            return new Unit(next);
        }

        public override bool Equals(object obj)
        {
            if (obj is Unit unit)
                return this == unit;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return degrees.GetHashCode();
        }

        private static string[] UNIT_NAMES = new string[] { "kg", "m", "s", "K", "A", "mol", "cd" };

        private static char[] SUPERSCRIPTS = new char[] { '⁰', '¹', '²', '³', '⁴', '⁵', '⁶', '⁷', '⁸', '⁹', };
        private static char SUPERSCRIPT_MINUS = '⁻';

        public override string ToString()
        {
            string num = "";
            string den = "";
            for (int i = 0; i < 7; i++)
            {
                sbyte value = this[i];
                if (value > 1)
                    num += UNIT_NAMES[i] + SUPERSCRIPTS[value];
                else if (value == 1)
                    num += UNIT_NAMES[i];
                else if (value < -1)
                    den += UNIT_NAMES[i] + SUPERSCRIPT_MINUS + SUPERSCRIPTS[-value];
                else if (value == -1)
                    den += UNIT_NAMES[i];
            }
            string result = num + "/" + den;
            if (result == "/")
                return "";
            else if (num.Length == 0)
                return $"1/{den}";
            else if (result.EndsWith("/"))
                return num;
            else
                return result;
        }

        public class UnitMismatchException : Exception
        {
            public UnitMismatchException(string message) : base(message)
            {

            }

            public UnitMismatchException(Unit a, Unit b) : base($"Unit {a} does not match unit {b}")
            {

            }
        }

        public static void Assert(Unit a, Unit b)
        {
            if (a != b)
                throw new UnitMismatchException(a, b);
        }

        public static readonly Unit One = new Unit(new sbyte[7]);

        public static readonly Unit Kilograms = new Unit(One, SI.Kilogram, 1);

        public static readonly Unit EarthPounds = new Unit(Kilograms, lbs => lbs / 0.43592, kg => kg * 0.43592);

        public static readonly Unit Grams = new Unit(Kilograms, g => g * 1000, kg => kg / 1000);
        
        public static readonly Unit Meters = new Unit(One, SI.Meter, 1);

        public static readonly Unit Feet = new Unit(Meters, f => f * 0.3048, m => m / 0.3048);

        public static readonly Unit Kilometers = new Unit(Meters, km => km * 1000, m => m / 1000);

        public static readonly Unit Miles = new Unit(Meters, mile => mile * 1609.34, m => m / 1609.34);
        
        public static readonly Unit Seconds = new Unit(One, SI.Second, 1);

        public static readonly Unit Minutes = new Unit(Seconds, m => m * 60, s => s / 60);

        public static readonly Unit Hours = new Unit(Seconds, h => h * 3600, s => s / 3600);
        
        public static readonly Unit Kelvin = new Unit(One, SI.Kelvin, 1);

        public static readonly Unit Celsius = new Unit(Kelvin, c => c - 273.15, k => k + 273.15);

        public static readonly Unit Fahrenheit = new Unit(Celsius, f => (f - 32) / 1.8, c => (c * 1.8) + 32);
        
        public static readonly Unit Amperes = new Unit(One, SI.Ampere, 1);

        public static readonly Unit Mole = new Unit(One, SI.Mol, 1);

        public static readonly Unit Candela = new Unit(One, SI.Candela, 1);

        /// <summary>
        /// Square meters
        /// </summary>
        public static readonly Unit Area = Meters * Meters;

        /// <summary>
        /// Cubic meters
        /// </summary>
        public static readonly Unit Volume = Meters * Meters * Meters;

        /// <summary>
        /// Meters per second
        /// </summary>
        public static readonly Unit Velocity = Meters / Seconds;

        /// <summary>
        /// Meters per second per second
        /// </summary>
        public static readonly Unit Acceleration = Meters / Seconds / Seconds;

        public static readonly Unit Wavenumber = One / Meters;

        /// <summary>
        /// Kilograms per meter cubed
        /// </summary>
        public static readonly Unit Density = Kilograms / Volume;

        /// <summary>
        /// Kilograms per meter squared
        /// </summary>
        public static readonly Unit SurfaceDensity = Kilograms / Area;

        /// <summary>
        /// Meters cubed per kilogram
        /// </summary>
        public static readonly Unit SpecificVolume = Volume / Kilograms;

        /// <summary>
        /// Candelas per meter squared
        /// </summary>
        public static readonly Unit Luminence = Candela / Area;

        /// <summary>
        /// Hertz
        /// </summary>
        public static readonly Unit Frequency = One / Seconds;

        /// <summary>
        /// Newtons
        /// </summary>
        public static readonly Unit Force = Meters * Kilograms / Seconds / Seconds;

        /// <summary>
        /// Pascals
        /// </summary>
        public static readonly Unit Pascals = Force / Area;
        
        public static readonly Unit Joules = Force * Meters;

        public static readonly Unit KilowattHour = new Unit(Joules, kwh => kwh / 3600, j => j * 3600);

        /// <summary>
        /// Watts
        /// </summary>
        public static readonly Unit Power = Joules / Seconds;

        /// <summary>
        /// Coulumbs
        /// </summary>
        public static readonly Unit Coulumbs = Amperes * Seconds;
        
        /// <summary>
        /// Volts
        /// </summary>
        public static readonly Unit Volts = Power / Amperes;

        /// <summary>
        /// Farads
        /// </summary>
        public static readonly Unit Farads = Coulumbs / Volts;

        /// <summary>
        /// Ohms
        /// </summary>
        public static readonly Unit Ohms = Volts / Amperes;

        /// <summary>
        /// Siemens
        /// </summary>
        public static readonly Unit Conductance = Amperes / Volts;

        /// <summary>
        /// Webers
        /// </summary>
        public static readonly Unit Flux = Volts * Seconds;

        /// <summary>
        /// Teslas
        /// </summary>
        public static readonly Unit FluxDenisty = Flux / Area;

        /// <summary>
        /// Henrys
        /// </summary>
        public static readonly Unit Inductance = Flux / Amperes;

        /// <summary>
        /// Grays
        /// </summary>
        public static readonly Unit SpecificEnergy = Joules / Kilograms;        
    }
}
