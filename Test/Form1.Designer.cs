namespace Test
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            label1 = new Label();
            tbserver1 = new TextBox();
            tbuser1 = new TextBox();
            label2 = new Label();
            tbpwd1 = new TextBox();
            label3 = new Label();
            tbdb1 = new TextBox();
            label4 = new Label();
            btnCdb1 = new Button();
            btnCta1 = new Button();
            btnAdd1 = new Button();
            btnEdit1 = new Button();
            btnQuery1 = new Button();
            btnDel1 = new Button();
            btnDel2 = new Button();
            btnQuery2 = new Button();
            btnEdit2 = new Button();
            btnAdd2 = new Button();
            btnCta2 = new Button();
            btnCdb2 = new Button();
            tbdb2 = new TextBox();
            label5 = new Label();
            tbpwd2 = new TextBox();
            label6 = new Label();
            tbuser2 = new TextBox();
            label7 = new Label();
            tbserver2 = new TextBox();
            label8 = new Label();
            btnDel3 = new Button();
            btnQuery3 = new Button();
            btnEdit3 = new Button();
            btnAdd3 = new Button();
            btnCta3 = new Button();
            btnCdb3 = new Button();
            tbdb3 = new TextBox();
            label9 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(35, 21);
            button1.Name = "button1";
            button1.Size = new Size(90, 35);
            button1.TabIndex = 0;
            button1.Text = "fileConfig";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(131, 21);
            button2.Name = "button2";
            button2.Size = new Size(90, 35);
            button2.TabIndex = 1;
            button2.Text = "jsonConfig";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnDel1);
            groupBox1.Controls.Add(btnQuery1);
            groupBox1.Controls.Add(btnEdit1);
            groupBox1.Controls.Add(btnAdd1);
            groupBox1.Controls.Add(btnCta1);
            groupBox1.Controls.Add(btnCdb1);
            groupBox1.Controls.Add(tbdb1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(tbpwd1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(tbuser1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tbserver1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(35, 68);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(208, 423);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "sqlserver";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnDel2);
            groupBox2.Controls.Add(tbserver2);
            groupBox2.Controls.Add(btnQuery2);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(btnEdit2);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(btnAdd2);
            groupBox2.Controls.Add(tbuser2);
            groupBox2.Controls.Add(btnCta2);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(btnCdb2);
            groupBox2.Controls.Add(tbpwd2);
            groupBox2.Controls.Add(tbdb2);
            groupBox2.Controls.Add(label5);
            groupBox2.Location = new Point(249, 68);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(208, 423);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "mysql";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tbdb3);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(btnDel3);
            groupBox3.Controls.Add(btnCdb3);
            groupBox3.Controls.Add(btnQuery3);
            groupBox3.Controls.Add(btnCta3);
            groupBox3.Controls.Add(btnEdit3);
            groupBox3.Controls.Add(btnAdd3);
            groupBox3.Location = new Point(463, 68);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(208, 423);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "sqlite";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 34);
            label1.Name = "label1";
            label1.Size = new Size(44, 17);
            label1.TabIndex = 0;
            label1.Text = "server";
            // 
            // tbserver1
            // 
            tbserver1.Location = new Point(75, 31);
            tbserver1.Name = "tbserver1";
            tbserver1.Size = new Size(100, 23);
            tbserver1.TabIndex = 1;
            tbserver1.Text = ".";
            // 
            // tbuser1
            // 
            tbuser1.Location = new Point(75, 60);
            tbuser1.Name = "tbuser1";
            tbuser1.Size = new Size(100, 23);
            tbuser1.TabIndex = 3;
            tbuser1.Text = "sa";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 63);
            label2.Name = "label2";
            label2.Size = new Size(33, 17);
            label2.TabIndex = 2;
            label2.Text = "user";
            // 
            // tbpwd1
            // 
            tbpwd1.Location = new Point(75, 89);
            tbpwd1.Name = "tbpwd1";
            tbpwd1.Size = new Size(100, 23);
            tbpwd1.TabIndex = 5;
            tbpwd1.Text = "sa";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 92);
            label3.Name = "label3";
            label3.Size = new Size(33, 17);
            label3.TabIndex = 4;
            label3.Text = "pwd";
            // 
            // tbdb1
            // 
            tbdb1.Location = new Point(75, 118);
            tbdb1.Name = "tbdb1";
            tbdb1.Size = new Size(100, 23);
            tbdb1.TabIndex = 7;
            tbdb1.Text = "test";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 121);
            label4.Name = "label4";
            label4.Size = new Size(24, 17);
            label4.TabIndex = 6;
            label4.Text = "db";
            // 
            // btnCdb1
            // 
            btnCdb1.Location = new Point(26, 157);
            btnCdb1.Name = "btnCdb1";
            btnCdb1.Size = new Size(149, 35);
            btnCdb1.TabIndex = 5;
            btnCdb1.Text = "创建数据库";
            btnCdb1.UseVisualStyleBackColor = true;
            // 
            // btnCta1
            // 
            btnCta1.Location = new Point(26, 198);
            btnCta1.Name = "btnCta1";
            btnCta1.Size = new Size(149, 35);
            btnCta1.TabIndex = 8;
            btnCta1.Text = "生成表";
            btnCta1.UseVisualStyleBackColor = true;
            // 
            // btnAdd1
            // 
            btnAdd1.Location = new Point(26, 239);
            btnAdd1.Name = "btnAdd1";
            btnAdd1.Size = new Size(149, 35);
            btnAdd1.TabIndex = 9;
            btnAdd1.Text = "新增";
            btnAdd1.UseVisualStyleBackColor = true;
            // 
            // btnEdit1
            // 
            btnEdit1.Location = new Point(26, 280);
            btnEdit1.Name = "btnEdit1";
            btnEdit1.Size = new Size(149, 35);
            btnEdit1.TabIndex = 10;
            btnEdit1.Text = "修改";
            btnEdit1.UseVisualStyleBackColor = true;
            // 
            // btnQuery1
            // 
            btnQuery1.Location = new Point(26, 321);
            btnQuery1.Name = "btnQuery1";
            btnQuery1.Size = new Size(149, 35);
            btnQuery1.TabIndex = 11;
            btnQuery1.Text = "查询";
            btnQuery1.UseVisualStyleBackColor = true;
            // 
            // btnDel1
            // 
            btnDel1.Location = new Point(26, 362);
            btnDel1.Name = "btnDel1";
            btnDel1.Size = new Size(149, 35);
            btnDel1.TabIndex = 12;
            btnDel1.Text = "删除";
            btnDel1.UseVisualStyleBackColor = true;
            // 
            // btnDel2
            // 
            btnDel2.Location = new Point(26, 362);
            btnDel2.Name = "btnDel2";
            btnDel2.Size = new Size(149, 35);
            btnDel2.TabIndex = 26;
            btnDel2.Text = "删除";
            btnDel2.UseVisualStyleBackColor = true;
            // 
            // btnQuery2
            // 
            btnQuery2.Location = new Point(26, 321);
            btnQuery2.Name = "btnQuery2";
            btnQuery2.Size = new Size(149, 35);
            btnQuery2.TabIndex = 25;
            btnQuery2.Text = "查询";
            btnQuery2.UseVisualStyleBackColor = true;
            // 
            // btnEdit2
            // 
            btnEdit2.Location = new Point(26, 280);
            btnEdit2.Name = "btnEdit2";
            btnEdit2.Size = new Size(149, 35);
            btnEdit2.TabIndex = 24;
            btnEdit2.Text = "修改";
            btnEdit2.UseVisualStyleBackColor = true;
            // 
            // btnAdd2
            // 
            btnAdd2.Location = new Point(26, 239);
            btnAdd2.Name = "btnAdd2";
            btnAdd2.Size = new Size(149, 35);
            btnAdd2.TabIndex = 23;
            btnAdd2.Text = "新增";
            btnAdd2.UseVisualStyleBackColor = true;
            // 
            // btnCta2
            // 
            btnCta2.Location = new Point(26, 198);
            btnCta2.Name = "btnCta2";
            btnCta2.Size = new Size(149, 35);
            btnCta2.TabIndex = 22;
            btnCta2.Text = "生成表";
            btnCta2.UseVisualStyleBackColor = true;
            // 
            // btnCdb2
            // 
            btnCdb2.Location = new Point(26, 157);
            btnCdb2.Name = "btnCdb2";
            btnCdb2.Size = new Size(149, 35);
            btnCdb2.TabIndex = 18;
            btnCdb2.Text = "创建数据库";
            btnCdb2.UseVisualStyleBackColor = true;
            // 
            // tbdb2
            // 
            tbdb2.Location = new Point(75, 118);
            tbdb2.Name = "tbdb2";
            tbdb2.Size = new Size(100, 23);
            tbdb2.TabIndex = 21;
            tbdb2.Text = "test";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 121);
            label5.Name = "label5";
            label5.Size = new Size(24, 17);
            label5.TabIndex = 20;
            label5.Text = "db";
            // 
            // tbpwd2
            // 
            tbpwd2.Location = new Point(75, 89);
            tbpwd2.Name = "tbpwd2";
            tbpwd2.Size = new Size(100, 23);
            tbpwd2.TabIndex = 19;
            tbpwd2.Text = "sa";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(25, 92);
            label6.Name = "label6";
            label6.Size = new Size(33, 17);
            label6.TabIndex = 17;
            label6.Text = "pwd";
            // 
            // tbuser2
            // 
            tbuser2.Location = new Point(75, 60);
            tbuser2.Name = "tbuser2";
            tbuser2.Size = new Size(100, 23);
            tbuser2.TabIndex = 16;
            tbuser2.Text = "sa";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(25, 63);
            label7.Name = "label7";
            label7.Size = new Size(33, 17);
            label7.TabIndex = 15;
            label7.Text = "user";
            // 
            // tbserver2
            // 
            tbserver2.Location = new Point(75, 31);
            tbserver2.Name = "tbserver2";
            tbserver2.Size = new Size(100, 23);
            tbserver2.TabIndex = 14;
            tbserver2.Text = ".";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(25, 34);
            label8.Name = "label8";
            label8.Size = new Size(44, 17);
            label8.TabIndex = 13;
            label8.Text = "server";
            // 
            // btnDel3
            // 
            btnDel3.Location = new Point(33, 362);
            btnDel3.Name = "btnDel3";
            btnDel3.Size = new Size(149, 35);
            btnDel3.TabIndex = 32;
            btnDel3.Text = "删除";
            btnDel3.UseVisualStyleBackColor = true;
            // 
            // btnQuery3
            // 
            btnQuery3.Location = new Point(33, 321);
            btnQuery3.Name = "btnQuery3";
            btnQuery3.Size = new Size(149, 35);
            btnQuery3.TabIndex = 31;
            btnQuery3.Text = "查询";
            btnQuery3.UseVisualStyleBackColor = true;
            // 
            // btnEdit3
            // 
            btnEdit3.Location = new Point(33, 280);
            btnEdit3.Name = "btnEdit3";
            btnEdit3.Size = new Size(149, 35);
            btnEdit3.TabIndex = 30;
            btnEdit3.Text = "修改";
            btnEdit3.UseVisualStyleBackColor = true;
            // 
            // btnAdd3
            // 
            btnAdd3.Location = new Point(33, 239);
            btnAdd3.Name = "btnAdd3";
            btnAdd3.Size = new Size(149, 35);
            btnAdd3.TabIndex = 29;
            btnAdd3.Text = "新增";
            btnAdd3.UseVisualStyleBackColor = true;
            // 
            // btnCta3
            // 
            btnCta3.Location = new Point(33, 198);
            btnCta3.Name = "btnCta3";
            btnCta3.Size = new Size(149, 35);
            btnCta3.TabIndex = 28;
            btnCta3.Text = "生成表";
            btnCta3.UseVisualStyleBackColor = true;
            // 
            // btnCdb3
            // 
            btnCdb3.Location = new Point(33, 157);
            btnCdb3.Name = "btnCdb3";
            btnCdb3.Size = new Size(149, 35);
            btnCdb3.TabIndex = 27;
            btnCdb3.Text = "创建数据库";
            btnCdb3.UseVisualStyleBackColor = true;
            // 
            // tbdb3
            // 
            tbdb3.Location = new Point(82, 118);
            tbdb3.Name = "tbdb3";
            tbdb3.Size = new Size(100, 23);
            tbdb3.TabIndex = 28;
            tbdb3.Text = "test";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(33, 121);
            label9.Name = "label9";
            label9.Size = new Size(24, 17);
            label9.TabIndex = 27;
            label9.Text = "db";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(709, 503);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button btnDel1;
        private Button btnQuery1;
        private Button btnEdit1;
        private Button btnAdd1;
        private Button btnCta1;
        private Button btnCdb1;
        private TextBox tbdb1;
        private Label label4;
        private TextBox tbpwd1;
        private Label label3;
        private TextBox tbuser1;
        private Label label2;
        private TextBox tbserver1;
        private Label label1;
        private Button btnDel2;
        private TextBox tbserver2;
        private Button btnQuery2;
        private Label label8;
        private Button btnEdit2;
        private Label label7;
        private Button btnAdd2;
        private TextBox tbuser2;
        private Button btnCta2;
        private Label label6;
        private Button btnCdb2;
        private TextBox tbpwd2;
        private TextBox tbdb2;
        private Label label5;
        private Button btnDel3;
        private Button btnCdb3;
        private Button btnQuery3;
        private Button btnCta3;
        private Button btnEdit3;
        private Button btnAdd3;
        private TextBox tbdb3;
        private Label label9;
    }
}
