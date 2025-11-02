using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CSV_Demo
{
    public partial class Form1 : Form
    {
        string filePath = "data.csv";

        public Form1()
        {
            InitializeComponent();
        }
        public class Student
        {
            public string Gender { get; set; }
            public string RaceEthnicity { get; set; }
            public string ParentalLevelOfEducation { get; set; }
            public string Lunch { get; set; }
            public string TestPreparationCourse { get; set; }
            public double MathScore { get; set; }
            public double ReadingScore { get; set; }
            public double WrittingScore { get; set; }
        }

        public class AVLNode
        {
            public Student Data;
            public AVLNode Left;
            public AVLNode Right;
            public int Height;

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
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Không tìm thấy file data.csv trong thư mục Debug!");
                return;
            }

            DataTable dt = new DataTable();
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length > 0)
            {

                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');
                foreach (string header in headerLabels)
                {
                    dt.Columns.Add(header.Trim().Trim('"'));
                }

          
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] dataWords = lines[i].Split(',');
                    if (dataWords.Length == dt.Columns.Count)
                    {
                 
                        for (int j = 0; j < dataWords.Length; j++)
                        {
                            dataWords[j] = dataWords[j].Trim().Trim('"');
                        }
                        dt.Rows.Add(dataWords);
                    }
                }
            }

            dataGridView1.DataSource = dt;
            MessageBox.Show($"Đã đọc {lines.Length - 1} dòng dữ liệu thành công!");
        }


        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để ghi!");
                return;
            }

   
            string outputPath = "output.txt";

            DataTable dt = (DataTable)dataGridView1.DataSource;
            var lines = new List<string>();

            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                             .Select(column => column.ColumnName)
                                             .ToArray();
            lines.Add(string.Join(",", columnNames));


            foreach (DataRow row in dt.Rows)
            {
                string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
                lines.Add(string.Join(",", fields));
            }


            File.WriteAllLines(outputPath, lines);

            MessageBox.Show($"✅ Đã ghi dữ liệu ra file output.txt!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTaoCay_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để tạo cây AVL!");
                return;
            }

            List<Student> students = new List<Student>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                try
                {
                    students.Add(new Student
                    {
                        Gender = row.Cells[0].Value?.ToString(),
                        RaceEthnicity = row.Cells[1].Value?.ToString(),
                        ParentalLevelOfEducation = row.Cells[2].Value?.ToString(),
                        Lunch = row.Cells[3].Value?.ToString(),
                        TestPreparationCourse = row.Cells[4].Value?.ToString(),
                        MathScore = double.TryParse(row.Cells[5].Value?.ToString(), out double m) ? m : 0,
                        ReadingScore = double.TryParse(row.Cells[6].Value?.ToString(), out double r) ? r : 0,
                        WrittingScore = double.TryParse(row.Cells[7].Value?.ToString(), out double w) ? w : 0
                    });
                }
                catch
                {
                    continue;
                }
            }

            if (students.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hợp lệ để sắp xếp!");
                return;
            }

            AVLTree avl = new AVLTree();
            foreach (var s in students)
                avl.Root = avl.Insert(avl.Root, s);

            List<Student> sorted = new List<Student>();
            avl.InOrder(avl.Root, sorted);


            dataGridView1.DataSource = null;
            dataGridView1.DataSource = sorted;

            string outputPath = "output.json";
            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                sw.WriteLine("Gender,RaceEthnicity,ParentalLevelOfEducation,Lunch,TestPreparationCourse,MathScore,ReadingScore,WrittingScore");
                foreach (var s in sorted)
                {
                    sw.WriteLine($"{s.Gender},{s.RaceEthnicity},{s.ParentalLevelOfEducation},{s.Lunch},{s.TestPreparationCourse},{s.MathScore},{s.ReadingScore},{s.WrittingScore}");
                }
            }

            MessageBox.Show("✅ Đã tạo cây AVL, sắp xếp theo MathScore và ghi ra file output.json!");
        }


    }
}
