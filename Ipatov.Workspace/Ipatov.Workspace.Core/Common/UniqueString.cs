using System;

namespace Ipatov.Workspace
{
    /// <summary>
    /// String with attached GUID.
    /// </summary>
    /// <remarks>Useful for localizable strings.</remarks>
    public struct UniqueString : IEquatable<UniqueString>
    {
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(UniqueString other)
        {
            // ReSharper disable once ImpureMethodCallOnReadonlyValueField
            return Id.Equals(other.Id);
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
            return obj is UniqueString && Equals((UniqueString) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(UniqueString left, UniqueString right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UniqueString left, UniqueString right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// String identifier.
        /// </summary>
        public readonly Guid Id;

        /// <summary>
        /// String value.
        /// </summary>
        public readonly string Value;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">String identifier.</param>
        /// <param name="value">String value.</param>
        public UniqueString(Guid id, string value)
        {
            Id = id;
            Value = value;
        }

        public static implicit operator Guid(UniqueString obj)
        {
            return obj.Id;
        }

        public static implicit operator string(UniqueString obj)
        {
            return obj.Value;
        }
    }
}