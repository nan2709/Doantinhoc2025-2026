using System;
using System.Collections.Generic;

namespace DoAnTinHoc2025_2026
{
    public class AVLNode
    {
        public Student Data;
        public AVLNode Left;
        public AVLNode Right;
        public int Height;
        public double Key;

        public AVLNode(Student data)
        {
            Data = data;
            Height = 1;
        }
    }

    public class AVLTree
    {
        public AVLNode Root;

        private int Height(AVLNode n) => n?.Height ?? 0;
        private int GetBalance(AVLNode n) => n == null ? 0 : Height(n.Left) - Height(n.Right);

        private AVLNode RotateRight(AVLNode y)
        {
            var x = y.Left;
            var T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private AVLNode RotateLeft(AVLNode x)
        {
            var y = x.Right;
            var T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public AVLNode Insert(AVLNode node, Student s)
        {
            if (node == null)
                return new AVLNode(s);

            if (s.MathScore < node.Data.MathScore)
                node.Left = Insert(node.Left, s);
            else if (s.MathScore > node.Data.MathScore)
                node.Right = Insert(node.Right, s);
            else
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            int balance = GetBalance(node);

            // 4 trường hợp mất cân bằng
            if (balance > 1 && s.MathScore < node.Left.Data.MathScore)
                return RotateRight(node);

            if (balance < -1 && s.MathScore > node.Right.Data.MathScore)
                return RotateLeft(node);

            if (balance > 1 && s.MathScore > node.Left.Data.MathScore)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && s.MathScore < node.Right.Data.MathScore)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        public void InOrder(AVLNode node, List<Student> result)
        {
            if (node == null) return;
            InOrder(node.Left, result);
            result.Add(node.Data);
            InOrder(node.Right, result);
        }

        public int GetHeight(AVLNode node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        }

        public int CountLeafNodes(AVLNode node)
        {
            if (node == null) return 0;
            if (node.Left == null && node.Right == null) return 1;
            return CountLeafNodes(node.Left) + CountLeafNodes(node.Right);
        }

        public Student FindMin(AVLNode node)
        {
            if (node == null) return null;
            while (node.Left != null)
                node = node.Left;
            return node.Data;
        }

        public Student FindMax(AVLNode node)
        {
            if (node == null) return null;
            while (node.Right != null)
                node = node.Right;
            return node.Data;
        }

        public Student FindValue(AVLNode node, double x)
        {
            if (node == null) return null;
            if (Math.Abs(x - node.Data.MathScore) < 1e-9) return node.Data;
            else if (x < node.Data.MathScore) return FindValue(node.Left, x);
            else return FindValue(node.Right, x);
        }
        public List<Student> GetNodesAtLevel(AVLNode root, int level)
        {
            List<Student> result = new List<Student>();
            if (root == null) return result;

            Queue<(AVLNode node, int level)> queue = new Queue<(AVLNode, int)>();
            queue.Enqueue((root, 0));

            while (queue.Count > 0)
            {
                var (node, currentLevel) = queue.Dequeue();

                if (currentLevel == level)
                    result.Add(node.Data);

                if (node.Left != null)
                    queue.Enqueue((node.Left, currentLevel + 1));

                if (node.Right != null)
                    queue.Enqueue((node.Right, currentLevel + 1));
            }

            return result;
        }
    }
}
    