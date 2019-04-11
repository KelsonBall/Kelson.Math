using System;

namespace Kelson.Physics
{    
    public readonly struct Scalar
    {
        public readonly double Value;        
        public readonly Unit Units;

        public Scalar(double value, Unit units) => (Value, Units) = (units.ToSI(value), units.CoreUnits());

        public static implicit operator Scalar((double v, Unit u) vu) => new Scalar(vu.v, vu.u);

        public static Scalar operator *(Scalar a, double s) => new Scalar(a.Value * s, a.Units);

        public static Scalar operator *(double s, Scalar a) => new Scalar(a.Value * s, a.Units);

        public static Scalar operator /(Scalar a, double s) => new Scalar(a.Value / s, a.Units);

        public static Scalar operator /(double s, Scalar a) => new Scalar(a.Value / s, a.Units);

        public static Scalar operator +(Scalar a, double s) => new Scalar(a.Value + s, a.Units);

        public static Scalar operator +(double s, Scalar a) => new Scalar(a.Value + s, a.Units);

        public static Scalar operator -(Scalar a, double s) => new Scalar(a.Value - s, a.Units);

        public static Scalar operator -(double s, Scalar a) => new Scalar(a.Value - s, a.Units);

        public static Scalar operator ^(Scalar a, double pow) => new Scalar(System.Math.Pow(a.Value, pow), a.Units);

        public static Scalar operator *(Scalar a, Scalar b)
        {
            return new Scalar(a.Value * b.Value, a.Units * b.Units);
        }

        public static Scalar operator /(Scalar a, Scalar b)
        {
            return new Scalar(a.Value / b.Value, a.Units / b.Units);
        }

        public static Scalar operator +(Scalar a, Scalar b)
        {
            if (a.Units != b.Units)
                throw new Unit.UnitMismatchException($"Cannot add {b.Units} to {a.Units}");
            return new Scalar(a.Value + b.Value, a.Units);
        }

        public static Scalar operator -(Scalar a, Scalar b)
        {
            if (a.Units != b.Units)
                throw new Unit.UnitMismatchException($"Cannot subtract {b.Units} from {a.Units}");
            return new Scalar(a.Value - b.Value, a.Units);
        }

        public static Scalar operator -(Scalar u)
        {
            return new Scalar(-u.Value, u.Units);
        }

        public static bool operator ==(Scalar a, Scalar b)
        {
            return a.Value == b.Value && a.Units == b.Units;
        }

        public static bool operator !=(Scalar a, Scalar b)
        {
            return a.Value != b.Value || a.Units != b.Units;
        }

        public override bool Equals(object obj)
        {
            if (obj is Scalar scalar)
                return this == scalar;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value} {Units}".Trim();
        }
    }
}
