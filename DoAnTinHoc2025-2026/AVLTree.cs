using System;
using System.Collections.Generic;
using System.Linq;

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
            Key = data.MathScore;
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
        public void XuatLa(AVLNode node, List<double> kq)
        {
            if (node == null) return;

            if (node.Left == null && node.Right == null)
                kq.Add(node.Key);

            XuatLa(node.Left, kq);
            XuatLa(node.Right, kq);
        }

        public void XuatLaChan(AVLNode node, List<double> kq)
        {
            if (node == null) return;

            if (node.Left == null && node.Right == null && node.Key % 2 == 0)
                kq.Add(node.Key);

            XuatLaChan(node.Left, kq);
            XuatLaChan(node.Right, kq);
        }

        public void XuatLaLe(AVLNode node, List<double> kq)
        {
            if (node == null) return;

            if (node.Left == null && node.Right == null && node.Key % 2 != 0)
                kq.Add(node.Key);

            XuatLaLe(node.Left, kq);
            XuatLaLe(node.Right, kq);
        }

        public double TongLa(AVLNode node)
        {
            if (node == null) return 0;

            if (node.Left == null && node.Right == null)
                return node.Key;

            return TongLa(node.Left) + TongLa(node.Right);
        }

        public double TongLaChan(AVLNode node)
        {
            if (node == null) return 0;

            if (node.Left == null && node.Right == null && node.Key % 2 == 0)
                return node.Key;

            return TongLaChan(node.Left) + TongLaChan(node.Right);
        }

        public double TongLaLe(AVLNode node)
        {
            if (node == null) return 0;

            if (node.Left == null && node.Right == null && node.Key % 2 != 0)
                return node.Key;

            return TongLaLe(node.Left) + TongLaLe(node.Right);
        }
        // kiểm tra node lá
        private bool IsLeaf(AVLNode node)
        {
            return node != null && node.Left == null && node.Right == null;
        }

        // ===== XUẤT LÁ =====
        public List<double> GetLeafValues(AVLNode root)
        {
            List<double> result = new List<double>();
            GetLeafValues(root, result);
            return result;
        }

        private void GetLeafValues(AVLNode node, List<double> list)
        {
            if (node == null) return;

            if (IsLeaf(node))
                list.Add(node.Data.MathScore);

            GetLeafValues(node.Left, list);
            GetLeafValues(node.Right, list);
        }

        // ===== LÁ CHẴN =====
        public List<double> GetLeafEvenValues(AVLNode root)
        {
            return GetLeafValues(root).Where(x => ((int)x) % 2 == 0).ToList();
        }

        // ===== LÁ LẺ =====
        public List<double> GetLeafOddValues(AVLNode root)
        {
            return GetLeafValues(root).Where(x => ((int)x) % 2 != 0).ToList();
        }

        // ===== TỔNG =====
        public double SumLeaf(AVLNode root)
        {
            return GetLeafValues(root).Sum();
        }

        public double SumLeafEven(AVLNode root)
        {
            return GetLeafEvenValues(root).Sum();
        }

        public double SumLeafOdd(AVLNode root)
        {
            return GetLeafOddValues(root).Sum();
        }
        public AVLNode InsertPrimitive(AVLNode node, Student s)
        {
            if (node == null)
                return new AVLNode(s);

            if (s.MathScore < node.Data.MathScore)
                node.Left = InsertPrimitive(node.Left, s);
            else if (s.MathScore > node.Data.MathScore)
                node.Right = InsertPrimitive(node.Right, s);
            // TRÙNG => KHÔNG LÀM GÌ
            return node;
        }
        public class DuplicateResult
        {
            public int Key { get; set; }    // giá trị MathScore (làm tròn)
            public int Count { get; set; }  // số lần xuất hiện
        }
        // ======================
        // THỐNG KÊ NODE TRÙNG
        // ======================

        // Thu thập tất cả node trong cây
        private void CollectNodes(AVLNode node, List<Student> list)
        {
            if (node == null) return;

            CollectNodes(node.Left, list);
            list.Add(node.Data);
            CollectNodes(node.Right, list);
        }

        // Nút trùng nhiều nhất
        public DuplicateResult FindMostDuplicateNode(AVLNode root)
        {
            List<Student> list = new List<Student>();
            CollectNodes(root, list);

            if (list.Count == 0) return null;

            var group = list
                .GroupBy(s => (int)s.MathScore)
                .OrderByDescending(g => g.Count())
                .First();

            return new DuplicateResult
            {
                Key = group.Key,
                Count = group.Count()
            };
        }

        // Nút trùng ít nhất
        public DuplicateResult FindLeastDuplicateNode(AVLNode root)
        {
            List<Student> list = new List<Student>();
            CollectNodes(root, list);

            if (list.Count == 0) return null;

            var group = list
                .GroupBy(s => (int)s.MathScore)
                .OrderBy(g => g.Count())
                .First();

            return new DuplicateResult
            {
                Key = group.Key,
                Count = group.Count()
            };
        }
        public class DuplicateGroup
        {
            public int Key { get; set; }                 // MathScore (làm tròn)
            public List<Student> Students { get; set; }  // Danh sách SV trùng
        }
        public List<DuplicateGroup> GetAllDuplicateGroups(AVLNode root)
        {
            List<Student> all = new List<Student>();
            CollectNodes(root, all);

            var groups = all
                .GroupBy(s => (int)s.MathScore)
                .Where(g => g.Count() >= 2)
                .Select(g => new DuplicateGroup
                {
                    Key = g.Key,
                    Students = g.ToList()
                })
                .ToList();

            return groups;
        }
        public AVLNode InsertAllowDuplicate(AVLNode node, Student s)
        {
            if (node == null)
                return new AVLNode(s);

            if (s.MathScore < node.Key)
                node.Left = InsertAllowDuplicate(node.Left, s);
            else if (s.MathScore > node.Key)
                node.Right = InsertAllowDuplicate(node.Right, s);
            else
            {
                // 🔥 TRÙNG → CHÈN VỀ PHẢI (HOẶC TRÁI)
                node.Right = InsertAllowDuplicate(node.Right, s);
            }

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            int balance = GetBalance(node);

            // AVL rotate
            if (balance > 1 && s.MathScore < node.Left.Key)
                return RotateRight(node);

            if (balance < -1 && s.MathScore > node.Right.Key)
                return RotateLeft(node);

            if (balance > 1 && s.MathScore > node.Left.Key)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && s.MathScore < node.Right.Key)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }
        public void BuildAVLTreeAllowDuplicate(List<Student> students)
        {
            Root = null;

            if (students == null || students.Count == 0)
                return;

            foreach (var s in students)
            {
                Root = InsertAllowDuplicate(Root, s);
            }
        }

    }
}

    