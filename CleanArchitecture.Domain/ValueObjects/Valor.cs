using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.ValueObjects
{
    public sealed class Valor
    {
        private double _value;

        public Valor(double value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public static implicit operator double(Valor value)
        {
            return value._value;
        }

        public static Valor operator -(Valor value)
        {
            return new Valor(Math.Abs(value._value) * -1);
        }

        public static implicit operator Valor(double value)
        {
            return new Valor(value);
        }

        public static Valor operator +(Valor amount1, Valor amount2)
        {
            return new Valor(amount1._value + amount2._value);
        }

        public static Valor operator -(Valor amount1, Valor amount2)
        {
            return new Valor(amount1._value - amount2._value);
        }

        public static bool operator <(Valor amount1, Valor amount2)
        {
            return amount1._value < amount2._value;
        }

        public static bool operator >(Valor amount1, Valor amount2)
        {
            return amount1._value > amount2._value;
        }

        public static bool operator <=(Valor amount1, Valor amount2)
        {
            return amount1._value <= amount2._value;
        }

        public static bool operator >=(Valor amount1, Valor amount2)
        {
            return amount1._value >= amount2._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is double)
            {
                return (double)obj == _value;
            }

            return ((Valor)obj)._value == _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
