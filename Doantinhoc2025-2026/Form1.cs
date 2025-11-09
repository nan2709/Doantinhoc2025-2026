using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace DoAnTinHoc2025_2026
{
    public partial class Form1 : Form
    {
        string filePath = "data.csv";
        AVLTree avl = new AVLTree();
        private List<Student> filteredStudents = new List<Student>();
        List<Student> currentStudents = new List<Student>();

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] {
             "Chiều cao cây",
             "Đếm node lá",
              "Giá trị nhỏ nhất",
             "Giá trị lớn nhất",
             "Tìm giá trị X"
             });
            comboBox1.SelectedIndex = 0;


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
            MessageBox.Show($"Đã đọc {Math.Max(0, lines.Length - 1)} dòng dữ liệu thành công!");
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để ghi!");
                return;
            }

            string outputPath = "output.csv";

            if (dataGridView1.DataSource is DataTable dt)
            {
                var lines = new List<string>();

                string[] columnNames = dt.Columns.Cast<DataColumn>()
                                                 .Select(column => column.ColumnName)
                                                 .ToArray();
                lines.Add(string.Join(",", columnNames));

                foreach (DataRow row in dt.Rows)
                {
                    string[] fields = row.ItemArray.Select(field => field?.ToString() ?? "").ToArray();
                    lines.Add(string.Join(",", fields));
                }

                File.WriteAllLines(outputPath, lines);
                MessageBox.Show($"✅ Đã ghi dữ liệu ra file {outputPath}!");
            }
            else
            {
                if (dataGridView1.DataSource is IEnumerable<Student> studentsEnum)
                {
                    using (StreamWriter sw = new StreamWriter(outputPath))
                    {
                        sw.WriteLine("Gender,RaceEthnicity,ParentalLevelOfEducation,Lunch,TestPreparationCourse,MathScore,ReadingScore,WrittingScore");
                        foreach (var s in studentsEnum)
                        {
                            sw.WriteLine($"{EscapeCsv(s.Gender)},{EscapeCsv(s.RaceEthnicity)},{EscapeCsv(s.ParentalLevelOfEducation)},{EscapeCsv(s.Lunch)},{EscapeCsv(s.TestPreparationCourse)},{s.MathScore},{s.ReadingScore},{s.WrittingScore}");
                        }
                    }
                    MessageBox.Show($"✅ Đã ghi dữ liệu ra file {outputPath}!");
                }
                else
                {
                    MessageBox.Show("Không hỗ trợ kiểu DataSource hiện tại. Vui lòng đảm bảo DataSource là DataTable hoặc danh sách Student.");
                }
            }
        }
        private string EscapeCsv(string input)
        {
            if (input == null) return "";
            if (input.Contains(",") || input.Contains("\"") || input.Contains("\n"))
            {
                return "\"" + input.Replace("\"", "\"\"") + "\"";
            }
            return input;
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
                    int maxIndex = row.Cells.Count - 1;
                    var getCell = new Func<int, string>(idx => idx <= maxIndex ? row.Cells[idx].Value?.ToString() : "");

                    students.Add(new Student
                    {
                        Gender = getCell(0),
                        RaceEthnicity = getCell(1),
                        ParentalLevelOfEducation = getCell(2),
                        Lunch = getCell(3),
                        TestPreparationCourse = getCell(4),
                        MathScore = double.TryParse(getCell(5), out double m) ? m : 0,
                        ReadingScore = double.TryParse(getCell(6), out double r) ? r : 0,
                        WrittingScore = double.TryParse(getCell(7), out double w) ? w : 0
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

            avl = new AVLTree();
            foreach (var s in students)
                avl.Root = avl.Insert(avl.Root, s);

            List<Student> sorted = new List<Student>();
            avl.InOrder(avl.Root, sorted);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = sorted;

            string outputPath = "output.csv";
            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                sw.WriteLine("Gender,RaceEthnicity,ParentalLevelOfEducation,Lunch,TestPreparationCourse,MathScore,ReadingScore,WrittingScore");
                foreach (var s in sorted)
                {
                    sw.WriteLine($"{EscapeCsv(s.Gender)},{EscapeCsv(s.RaceEthnicity)},{EscapeCsv(s.ParentalLevelOfEducation)},{EscapeCsv(s.Lunch)},{EscapeCsv(s.TestPreparationCourse)},{s.MathScore},{s.ReadingScore},{s.WrittingScore}");
                }
            }

            MessageBox.Show("✅ Đã tạo cây AVL, sắp xếp theo MathScore và ghi ra file output.csv!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (avl == null || avl.Root == null)
            {
                MessageBox.Show("Vui lòng tạo cây AVL trước khi chọn thuật toán!");
                return;
            }

            string selected = comboBox1.SelectedItem.ToString();

            switch (selected)
            {
                case "Chiều cao cây":
                    MessageBox.Show($"Chiều cao của cây là: {avl.GetHeight(avl.Root)}");
                    break;

                case "Đếm node lá":
                    MessageBox.Show($"Số lượng node lá là: {avl.CountLeafNodes(avl.Root)}");
                    break;

                case "Giá trị nhỏ nhất":
                    var min = avl.FindMin(avl.Root);
                    if (min != null)
                    {
                        MessageBox.Show($"Giá trị nhỏ nhất (MathScore): {min.MathScore}\n" +
                                        $"Giới tính: {min.Gender}, Thuộc: {min.RaceEthnicity}");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy giá trị nhỏ nhất (cây rỗng).");
                    }
                    break;

                case "Giá trị lớn nhất":
                    var max = avl.FindMax(avl.Root);
                    if (max != null)
                    {
                        MessageBox.Show($"Giá trị lớn nhất (MathScore): {max.MathScore}\n" +
                                        $"Giới tính: {max.Gender}, Thuộc: {max.RaceEthnicity}");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy giá trị lớn nhất (cây rỗng).");
                    }
                    break;
   
                case "Tìm giá trị X":
                    string input = Prompt.ShowDialog("Nhập giá trị MathScore cần tìm:", "Tìm giá trị X");

                    if (double.TryParse(input, out double x))
                    {
                        var found = avl.FindValue(avl.Root, x);
                        if (found != null)
                        {
                            MessageBox.Show($"Đã tìm thấy sinh viên có MathScore = {x}\n" +
                                            $"Giới tính: {found.Gender},Thuộc: {found.RaceEthnicity}");
                        }
                        else
                        {
                            MessageBox.Show($"Không tìm thấy sinh viên có MathScore = {x}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Giá trị nhập không hợp lệ!");
                    }
                    break;
            }
        }


        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 400,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterParent
                };
                Label textLabel = new Label() { Left = 10, Top = 10, Text = text, AutoSize = true };
                TextBox inputBox = new TextBox() { Left = 10, Top = 35, Width = 360 };
                Button confirmation = new Button() { Text = "OK", Left = 220, Width = 70, Top = 65, DialogResult = DialogResult.OK };
                Button cancel = new Button() { Text = "Cancel", Left = 300, Width = 70, Top = 65, DialogResult = DialogResult.Cancel };
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);
                prompt.AcceptButton = confirmation;
                prompt.CancelButton = cancel;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
            }
        }

        private void btnShowTopN_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Không tìm thấy file data.csv!");
                return;
            }

            string input = Prompt.ShowDialog("Nhập số dòng muốn hiển thị:", "Hiển thị N dòng đầu");
            if (!int.TryParse(input, out int n) || n <= 0)
            {
                MessageBox.Show("Giá trị nhập không hợp lệ!");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            if (lines.Length <= 1)
            {
                MessageBox.Show("File không có dữ liệu hợp lệ!");
                return;
            }

            string[] headers = lines[0].Split(',');
            List<Student> students = new List<Student>();
            for (int i = 1; i < Math.Min(n + 1, lines.Length); i++)
            {
                string[] cells = lines[i].Split(',');

                if (cells.Length < 8) continue;
                string Clean(string s) => s.Trim().Trim('"');

                students.Add(new Student
                {
                    Gender = Clean(cells[0]),
                    RaceEthnicity = Clean(cells[1]),
                    ParentalLevelOfEducation = Clean(cells[2]),
                    Lunch = Clean(cells[3]),
                    TestPreparationCourse = Clean(cells[4]),
                    MathScore = double.TryParse(Clean(cells[5]), out double m) ? m : 0,
                    ReadingScore = double.TryParse(Clean(cells[6]), out double r) ? r : 0,
                    WrittingScore = double.TryParse(Clean(cells[7]), out double w) ? w : 0
                });
            }

            if (students.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hợp lệ để hiển thị!");
                return;
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;


            currentStudents = students; 
            string outputPath = "filtered_output.csv";
            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                sw.WriteLine(string.Join(",", headers));
                foreach (var s in students)
                {
                    sw.WriteLine($"{s.Gender},{s.RaceEthnicity},{s.ParentalLevelOfEducation},{s.Lunch},{s.TestPreparationCourse},{s.MathScore},{s.ReadingScore},{s.WrittingScore}");
                }
            }

            MessageBox.Show($"✅ Hiển thị và lưu {students.Count} dòng đầu tiên vào {outputPath}!");
        }

        private void btnShowLevelK_Click(object sender, EventArgs e)
        {
            if (currentStudents == null || currentStudents.Count == 0)
            {
                MessageBox.Show("⚠️ Không có dữ liệu để tạo cây AVL!");
                return;
            }
            string input = Prompt.ShowDialog("Nhập tầng K cần lọc (root = 0):", "Lọc tầng K");
            if (!int.TryParse(input, out int k) || k < 0)
            {
                MessageBox.Show("Giá trị tầng không hợp lệ!");
                return;
            }
            avl = new AVLTree();
            foreach (var s in currentStudents)
                avl.Root = avl.Insert(avl.Root, s);
            var levelList = avl.GetNodesAtLevel(avl.Root, k);
            if (levelList.Count == 0)
            {
                MessageBox.Show($"Không có node nào ở tầng {k}!");
                return;
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = levelList;
            currentStudents = levelList;

            MessageBox.Show($"✅ Đã lọc và hiển thị {levelList.Count} sinh viên ở tầng {k}. Dữ liệu này đã được cập nhật!");

        }
    }
}
