namespace DoAnTinHoc2025_2026
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Dọn dẹp tài nguyên đang dùng.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Hàm khởi tạo giao diện Form.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnTaoCay = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnShowTopN = new System.Windows.Forms.Button();
            this.btnShowLevelK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 64);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1173, 560);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(13, 16);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(115, 32);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "Đọc CSV";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(272, 16);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(115, 32);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "Ghi CSV";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnTaoCay
            // 
            this.btnTaoCay.Location = new System.Drawing.Point(154, 18);
            this.btnTaoCay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTaoCay.Name = "btnTaoCay";
            this.btnTaoCay.Size = new System.Drawing.Size(101, 30);
            this.btnTaoCay.TabIndex = 3;
            this.btnTaoCay.Text = "Tạo cây AVL";
            this.btnTaoCay.UseVisualStyleBackColor = true;
            this.btnTaoCay.Click += new System.EventHandler(this.btnTaoCay_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(414, 22);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(221, 24);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "AVL";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnShowTopN
            // 
            this.btnShowTopN.Location = new System.Drawing.Point(682, 16);
            this.btnShowTopN.Name = "btnShowTopN";
            this.btnShowTopN.Size = new System.Drawing.Size(100, 30);
            this.btnShowTopN.TabIndex = 5;
            this.btnShowTopN.Text = "Lọc n số dòng";
            this.btnShowTopN.UseVisualStyleBackColor = true;
            this.btnShowTopN.Click += new System.EventHandler(this.btnShowTopN_Click);
            // 
            // btnShowLevelK
            // 
            this.btnShowLevelK.Location = new System.Drawing.Point(803, 16);
            this.btnShowLevelK.Name = "btnShowLevelK";
            this.btnShowLevelK.Size = new System.Drawing.Size(91, 32);
            this.btnShowLevelK.TabIndex = 7;
            this.btnShowLevelK.Text = "Lọc tầng";
            this.btnShowLevelK.UseVisualStyleBackColor = true;
            this.btnShowLevelK.Click += new System.EventHandler(this.btnShowLevelK_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 646);
            this.Controls.Add(this.btnShowLevelK);
            this.Controls.Add(this.btnShowTopN);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnTaoCay);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "Form1";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnTaoCay;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnShowTopN;
        private System.Windows.Forms.Button btnShowLevelK;
    }
}
