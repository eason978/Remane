namespace Rename
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox_selected_folder = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView_selected_list = new System.Windows.Forms.TreeView();
            this.textBox_Mname = new System.Windows.Forms.TextBox();
            this.textBox_Ename = new System.Windows.Forms.TextBox();
            this.textBox_Year = new System.Windows.Forms.TextBox();
            this.textBox_folder_exp = new System.Windows.Forms.TextBox();
            this.textBox_file_exp = new System.Windows.Forms.TextBox();
            this.label_folder_name = new System.Windows.Forms.Label();
            this.label_file_name = new System.Windows.Forms.Label();
            this.label_LMname = new System.Windows.Forms.Label();
            this.label_LEname = new System.Windows.Forms.Label();
            this.label_LYear = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_keyword = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(13, 370);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(779, 88);
            this.listBox1.TabIndex = 0;
            // 
            // textBox_selected_folder
            // 
            this.textBox_selected_folder.Location = new System.Drawing.Point(13, 13);
            this.textBox_selected_folder.Name = "textBox_selected_folder";
            this.textBox_selected_folder.Size = new System.Drawing.Size(698, 22);
            this.textBox_selected_folder.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(717, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView_selected_list
            // 
            this.treeView_selected_list.Location = new System.Drawing.Point(13, 42);
            this.treeView_selected_list.Name = "treeView_selected_list";
            this.treeView_selected_list.Size = new System.Drawing.Size(283, 322);
            this.treeView_selected_list.TabIndex = 3;
            this.treeView_selected_list.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // textBox_Mname
            // 
            this.textBox_Mname.Location = new System.Drawing.Point(388, 213);
            this.textBox_Mname.Name = "textBox_Mname";
            this.textBox_Mname.Size = new System.Drawing.Size(404, 22);
            this.textBox_Mname.TabIndex = 4;
            this.textBox_Mname.Text = "中文名稱";
            this.textBox_Mname.TextChanged += new System.EventHandler(this.textBox_MatchPattern_TextChanged);
            // 
            // textBox_Ename
            // 
            this.textBox_Ename.Enabled = false;
            this.textBox_Ename.Location = new System.Drawing.Point(388, 242);
            this.textBox_Ename.Name = "textBox_Ename";
            this.textBox_Ename.Size = new System.Drawing.Size(404, 22);
            this.textBox_Ename.TabIndex = 5;
            this.textBox_Ename.Text = "English name";
            this.textBox_Ename.TextChanged += new System.EventHandler(this.textBox_MatchPattern_TextChanged);
            // 
            // textBox_Year
            // 
            this.textBox_Year.Location = new System.Drawing.Point(388, 271);
            this.textBox_Year.Name = "textBox_Year";
            this.textBox_Year.Size = new System.Drawing.Size(100, 22);
            this.textBox_Year.TabIndex = 6;
            this.textBox_Year.Text = "2016";
            this.textBox_Year.TextChanged += new System.EventHandler(this.textBox_MatchPattern_TextChanged);
            // 
            // textBox_folder_exp
            // 
            this.textBox_folder_exp.Location = new System.Drawing.Point(388, 299);
            this.textBox_folder_exp.Name = "textBox_folder_exp";
            this.textBox_folder_exp.Size = new System.Drawing.Size(100, 22);
            this.textBox_folder_exp.TabIndex = 7;
            this.textBox_folder_exp.Text = "%M (%Y)";
            this.textBox_folder_exp.TextChanged += new System.EventHandler(this.textBox_MatchPattern_TextChanged);
            // 
            // textBox_file_exp
            // 
            this.textBox_file_exp.Location = new System.Drawing.Point(388, 327);
            this.textBox_file_exp.Name = "textBox_file_exp";
            this.textBox_file_exp.Size = new System.Drawing.Size(100, 22);
            this.textBox_file_exp.TabIndex = 8;
            this.textBox_file_exp.Text = "%M.%Y";
            this.textBox_file_exp.TextChanged += new System.EventHandler(this.textBox_MatchPattern_TextChanged);
            // 
            // label_folder_name
            // 
            this.label_folder_name.AutoSize = true;
            this.label_folder_name.Location = new System.Drawing.Point(494, 302);
            this.label_folder_name.Name = "label_folder_name";
            this.label_folder_name.Size = new System.Drawing.Size(33, 12);
            this.label_folder_name.TabIndex = 9;
            this.label_folder_name.Text = "label1";
            // 
            // label_file_name
            // 
            this.label_file_name.AutoSize = true;
            this.label_file_name.Location = new System.Drawing.Point(495, 327);
            this.label_file_name.Name = "label_file_name";
            this.label_file_name.Size = new System.Drawing.Size(33, 12);
            this.label_file_name.TabIndex = 10;
            this.label_file_name.Text = "label2";
            // 
            // label_LMname
            // 
            this.label_LMname.AutoSize = true;
            this.label_LMname.Location = new System.Drawing.Point(303, 223);
            this.label_LMname.Name = "label_LMname";
            this.label_LMname.Size = new System.Drawing.Size(67, 12);
            this.label_LMname.TabIndex = 11;
            this.label_LMname.Text = "Mname(%M)";
            // 
            // label_LEname
            // 
            this.label_LEname.AutoSize = true;
            this.label_LEname.Location = new System.Drawing.Point(303, 252);
            this.label_LEname.Name = "label_LEname";
            this.label_LEname.Size = new System.Drawing.Size(61, 12);
            this.label_LEname.TabIndex = 11;
            this.label_LEname.Text = "Ename(%E)";
            // 
            // label_LYear
            // 
            this.label_LYear.AutoSize = true;
            this.label_LYear.Location = new System.Drawing.Point(303, 281);
            this.label_LYear.Name = "label_LYear";
            this.label_LYear.Size = new System.Drawing.Size(52, 12);
            this.label_LYear.TabIndex = 11;
            this.label_LYear.Text = "Year(%Y)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(303, 309);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "Folder exp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 330);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "file exp";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(717, 341);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_keyword
            // 
            this.textBox_keyword.Location = new System.Drawing.Point(388, 41);
            this.textBox_keyword.Name = "textBox_keyword";
            this.textBox_keyword.Size = new System.Drawing.Size(323, 22);
            this.textBox_keyword.TabIndex = 15;
            this.textBox_keyword.Text = "丹麥女孩";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(717, 42);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(303, 71);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(489, 100);
            this.listBox2.TabIndex = 17;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 473);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox_keyword);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_LYear);
            this.Controls.Add(this.label_LEname);
            this.Controls.Add(this.label_LMname);
            this.Controls.Add(this.label_file_name);
            this.Controls.Add(this.label_folder_name);
            this.Controls.Add(this.textBox_file_exp);
            this.Controls.Add(this.textBox_folder_exp);
            this.Controls.Add(this.textBox_Year);
            this.Controls.Add(this.textBox_Ename);
            this.Controls.Add(this.textBox_Mname);
            this.Controls.Add(this.treeView_selected_list);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_selected_folder);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox_selected_folder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeView_selected_list;
        private System.Windows.Forms.TextBox textBox_Mname;
        private System.Windows.Forms.TextBox textBox_Ename;
        private System.Windows.Forms.TextBox textBox_Year;
        private System.Windows.Forms.TextBox textBox_folder_exp;
        private System.Windows.Forms.TextBox textBox_file_exp;
        private System.Windows.Forms.Label label_folder_name;
        private System.Windows.Forms.Label label_file_name;
        private System.Windows.Forms.Label label_LMname;
        private System.Windows.Forms.Label label_LEname;
        private System.Windows.Forms.Label label_LYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox_keyword;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox2;
    }
}

