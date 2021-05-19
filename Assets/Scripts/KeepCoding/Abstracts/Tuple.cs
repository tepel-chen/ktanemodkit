using System;

namespace KModkit
{
    /// <summary>
    /// An abstract type meant for the tuple data type for C# 4. Written by Emik.
    /// </summary>
    public abstract partial class Tuple : ITuple
    {
        /// <summary>
        /// Indexable tuple. Be careful when using this as the compiler will not notice if you are using the wrong type.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="WrongDatatypeException"></exception>
        /// <param name="i">The index to use.</param>
        /// <returns>The item indexed into.</returns>
        public object this[byte i]
        {
            get
            {
                throw IndexOutOfRange(i);
            }
            set
            {
                throw IndexOutOfRange(i);
            }
        }

        /// <value>
        /// Determines if the tuple data type is empty.
        /// </value>
        public bool IsEmpty
        {
            get
            {
                return Length == 0;
            }
        }

        /// <value>
        /// Gets the length of the tuple, describing the amount of elements there are.
        /// </value>
        public byte Length
        {
            get
            {
                return (byte)GetType().GetGenericArguments().Length;
            }
        }

        /// <value>
        /// Gets the upper bound of the tuple, which is the last index.
        /// </value>
        /// <exception cref="InvalidOperationException"></exception>
        public byte UpperBound
        {
            get
            {
                if (IsEmpty)
                    throw new InvalidOperationException(
                        "The tuple is empty, meaning that the upper bound doesn't exist.");
                return (byte)(Length - 1);
            }
        }

        /// <value>
        /// All of the tuple's items as an array, ordered by item number.
        /// </value>
        public abstract object[] ToArray { get; }

        protected IndexOutOfRangeException IndexOutOfRange(int i)
        {
            return new IndexOutOfRangeException(String.Format("The index {0} was out of range from the tuple of length {1}.", i, ToArray.Length));
        }

        protected static TOutput Cast<TInput, TOutput>(ref TInput value, int index)
        {
            if (value is TOutput)
                return (TOutput) (object) value;
            throw WrongDatatype(value, typeof(TOutput), index);
        }

        private static WrongDatatypeException WrongDatatype<T>(T received, Type expected, int index)
        {
            return new WrongDatatypeException(String.Format(
                "The {0} element in the tuple cannot be assigned because the value {1} is type {2} which doesn't match the expected type {3}.",
                (index + 1).ToOrdinal(), received.UnwrapToString(), received.GetType().Name, expected.Name));
        }
    }
}
