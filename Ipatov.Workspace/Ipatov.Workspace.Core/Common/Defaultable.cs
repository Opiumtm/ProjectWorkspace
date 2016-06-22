using System;
using System.Collections.Generic;

namespace Ipatov.Workspace
{
    /// <summary>
    /// Data with information if value is default.
    /// </summary>
    /// <typeparam name="T">Data type.</typeparam>
    public struct Defaultable<T>
    {
        public bool Equals(Defaultable<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value) && IsDefault == other.IsDefault;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false. 
        /// </returns>
        /// <param name="obj">The object to compare with the current instance. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Defaultable<T> && Equals((Defaultable<T>) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Value)*397) ^ IsDefault.GetHashCode();
            }
        }

        /// <summary>
        /// Identifier.
        /// </summary>
        public readonly T Value;

        /// <summary>
        /// Value is default.
        /// </summary>
        public readonly bool IsDefault;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="isDefault">Value is default.</param>
        public Defaultable(T value, bool isDefault)
        {
            Value = value;
            IsDefault = isDefault;
        }

        /// <summary>
        /// Throw exception if value is default.
        /// </summary>
        public T ThrowIfDefault(string message = "Default value is not allowed")
        {
            if (IsDefault)
            {
                throw new InvalidOperationException(message);
            }
            return Value;
        }

        public static implicit operator T(Defaultable<T> src)
        {
            return src.Value;
        }

        public static bool operator == (Defaultable<T> a, T b)
        {
            return !a.IsDefault && (a.Value?.Equals(b) ?? false);
        }

        public static bool operator !=(Defaultable<T> a, T b)
        {
            return !(a == b);
        }

        public static bool operator ==(T b, Defaultable<T> a)
        {
            return !a.IsDefault && (a.Value?.Equals(b) ?? false);
        }

        public static bool operator !=(T b, Defaultable<T> a)
        {
            return !(b == a);
        }

        public static bool operator ==(Defaultable<T> a, Defaultable<T> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Defaultable<T> a, Defaultable<T> b)
        {
            return !(a == b);
        }
    }
}