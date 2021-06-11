using System;

namespace Ihs.Assessment.Bst
{
    public class Node<T> : IComparable<T> where T : IComparable<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }
        public T Value { get; set; }

        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }
}
