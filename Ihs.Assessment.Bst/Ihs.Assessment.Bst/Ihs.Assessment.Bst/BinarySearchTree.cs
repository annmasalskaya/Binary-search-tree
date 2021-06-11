using System;

namespace Ihs.Assessment.Bst
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public Node<T> Root = null;

        public void Add(T value)
        {
            if (Root == null)
            {
                Root = new Node<T>(value);
            }
            else
            {
                Add(Root, value);
            }
        }

        private void Add(Node<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new Node<T>(value);
                }
                else
                {
                    Add(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new Node<T>(value);
                }
                else
                {
                    Add(node.Right, value);
                }
            }
        }

        public int GetHeight()
        {
            return GetHeight(Root);
        }

        private int GetHeight(Node<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                var leftSubTreeHeight = GetHeight(root.Left);
                var rightSubTreeHeight = GetHeight(root.Right);

                return Math.Max(leftSubTreeHeight, rightSubTreeHeight) + 1;
            }
        }

        //Remove method is taken from https://github.com/Marusyk/BinaryTree/blob/35dbfd34c2a6db689b7b54fe92fbc9bf18289810/src/BinaryTree/BinaryTree.cs#L92
        public bool Remove(T value)
        {
            var current = Find(value, out var parent);

            if (current == null)
            {
                return false;
            }

            if (current.Right == null)
            {
                if (parent == null)
                {
                    Root = current.Left;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                {
                    Root = current.Right;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            else
            {
                var leftTop = current.Right.Left;
                var leftTopParent = current.Right;

                while (leftTop.Left != null)
                {
                    leftTopParent = leftTop;
                    leftTop = leftTop.Left;
                }

                leftTopParent.Left = leftTop.Right;
                leftTop.Left = current.Left;
                leftTop.Right = current.Right;

                if (parent == null)
                {
                    Root = leftTop;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = leftTop;
                    }
                    else
                    {
                        if (result < 0)
                        {
                            parent.Right = leftTop;
                        }
                    }
                }
            }

            return true;
        }

        private Node<T> Find(T value, out Node<T> parent)
        {
            var current = Root;
            parent = null;

            while (current != null)
            {
                var result = current.CompareTo(value);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else
                {
                    if (result < 0)
                    {
                        parent = current;
                        current = current.Right;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return current;
        }
    }
}
