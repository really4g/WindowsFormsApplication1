namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.DomainTree = new System.Windows.Forms.TreeView();
            this.uMobile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.uTelNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.uName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.uSurname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this._prof_basesDataSet = new WindowsFormsApplication1._prof_basesDataSet();
            this.profbasesDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.uOuDn = new System.Windows.Forms.TextBox();
            this.ouType = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._prof_basesDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profbasesDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "m.semenov";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(143, 14);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "prof.loc";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(24, 69);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(739, 20);
            this.textBox3.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(239, 311);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Выбрать подразделение:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(576, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "Добавить пользователя";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // DomainTree
            // 
            this.DomainTree.Location = new System.Drawing.Point(242, 352);
            this.DomainTree.Name = "DomainTree";
            this.DomainTree.Size = new System.Drawing.Size(310, 313);
            this.DomainTree.TabIndex = 25;
            this.DomainTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DomainTree_AfterSelect);
            this.DomainTree.DoubleClick += new System.EventHandler(this.DomainTree_DoubleClick);
            // 
            // uMobile
            // 
            this.uMobile.Location = new System.Drawing.Point(242, 278);
            this.uMobile.Name = "uMobile";
            this.uMobile.Size = new System.Drawing.Size(310, 20);
            this.uMobile.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Телефон мобильный (корпоративный)";
            // 
            // uTelNumber
            // 
            this.uTelNumber.Location = new System.Drawing.Point(242, 238);
            this.uTelNumber.Name = "uTelNumber";
            this.uTelNumber.Size = new System.Drawing.Size(310, 20);
            this.uTelNumber.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Телефон мобильный (личный)";
            // 
            // uLogin
            // 
            this.uLogin.Location = new System.Drawing.Point(242, 186);
            this.uLogin.Name = "uLogin";
            this.uLogin.Size = new System.Drawing.Size(310, 20);
            this.uLogin.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Login";
            // 
            // uName
            // 
            this.uName.Location = new System.Drawing.Point(242, 146);
            this.uName.Name = "uName";
            this.uName.Size = new System.Drawing.Size(310, 20);
            this.uName.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(239, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Имя";
            // 
            // uSurname
            // 
            this.uSurname.Location = new System.Drawing.Point(242, 106);
            this.uSurname.Name = "uSurname";
            this.uSurname.Size = new System.Drawing.Size(310, 20);
            this.uSurname.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(239, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Фамилия";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(22, 98);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(162, 323);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 28;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // _prof_basesDataSet
            // 
            this._prof_basesDataSet.DataSetName = "_prof_basesDataSet";
            this._prof_basesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // profbasesDataSetBindingSource
            // 
            this.profbasesDataSetBindingSource.DataSource = this._prof_basesDataSet;
            this.profbasesDataSetBindingSource.Position = 0;
            // 
            // uOuDn
            // 
            this.uOuDn.Location = new System.Drawing.Point(243, 326);
            this.uOuDn.Name = "uOuDn";
            this.uOuDn.Size = new System.Drawing.Size(309, 20);
            this.uOuDn.TabIndex = 29;
            // 
            // ouType
            // 
            this.ouType.Location = new System.Drawing.Point(558, 326);
            this.ouType.Name = "ouType";
            this.ouType.Size = new System.Drawing.Size(205, 20);
            this.ouType.TabIndex = 30;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 753);
            this.Controls.Add(this.ouType);
            this.Controls.Add(this.uOuDn);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.DomainTree);
            this.Controls.Add(this.uMobile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.uTelNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.uLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.uName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uSurname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._prof_basesDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profbasesDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TreeView DomainTree;
        private System.Windows.Forms.TextBox uMobile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox uTelNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox uLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox uName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uSurname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private _prof_basesDataSet _prof_basesDataSet;
        private System.Windows.Forms.BindingSource profbasesDataSetBindingSource;
        private System.Windows.Forms.TextBox uOuDn;
        private System.Windows.Forms.TextBox ouType;
    }
}

