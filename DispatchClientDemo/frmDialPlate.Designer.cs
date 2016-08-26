namespace DispatchClientDemo
{
    partial class frmDialPlate
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSharp = new System.Windows.Forms.Button();
            this.btnStar = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.txtDialNum = new System.Windows.Forms.TextBox();
            this.btnAllClear = new System.Windows.Forms.Button();
            this.btnNum1 = new System.Windows.Forms.Button();
            this.btnNum2 = new System.Windows.Forms.Button();
            this.btnNum3 = new System.Windows.Forms.Button();
            this.btnNum4 = new System.Windows.Forms.Button();
            this.btnNum5 = new System.Windows.Forms.Button();
            this.btnNum6 = new System.Windows.Forms.Button();
            this.btnNum7 = new System.Windows.Forms.Button();
            this.btnBackspace = new System.Windows.Forms.Button();
            this.btnNum8 = new System.Windows.Forms.Button();
            this.btnNum0 = new System.Windows.Forms.Button();
            this.btnNum9 = new System.Windows.Forms.Button();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(201, 24);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 33);
            this.btnOK.TabIndex = 30;
            this.btnOK.Text = "←┘Enter";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnSharp
            // 
            this.btnSharp.Location = new System.Drawing.Point(199, 249);
            this.btnSharp.Name = "btnSharp";
            this.btnSharp.Size = new System.Drawing.Size(92, 53);
            this.btnSharp.TabIndex = 29;
            this.btnSharp.Text = "#";
            this.btnSharp.UseVisualStyleBackColor = true;
            this.btnSharp.Click += new System.EventHandler(this.btnSharp_Click);
            this.btnSharp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnStar
            // 
            this.btnStar.Location = new System.Drawing.Point(7, 249);
            this.btnStar.Name = "btnStar";
            this.btnStar.Size = new System.Drawing.Size(92, 53);
            this.btnStar.TabIndex = 28;
            this.btnStar.Text = "*";
            this.btnStar.UseVisualStyleBackColor = true;
            this.btnStar.Click += new System.EventHandler(this.btnStar_Click);
            this.btnStar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(103, 309);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(92, 53);
            this.btnSplit.TabIndex = 27;
            this.btnSplit.Text = "|";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            this.btnSplit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // txtDialNum
            // 
            this.txtDialNum.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDialNum.Location = new System.Drawing.Point(7, 22);
            this.txtDialNum.Name = "txtDialNum";
            this.txtDialNum.Size = new System.Drawing.Size(188, 35);
            this.txtDialNum.TabIndex = 0;
            this.txtDialNum.TextChanged += new System.EventHandler(this.txtDialNum_TextChanged);
            this.txtDialNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnAllClear
            // 
            this.btnAllClear.Location = new System.Drawing.Point(7, 309);
            this.btnAllClear.Name = "btnAllClear";
            this.btnAllClear.Size = new System.Drawing.Size(92, 53);
            this.btnAllClear.TabIndex = 23;
            this.btnAllClear.Text = "× AllClear";
            this.btnAllClear.UseVisualStyleBackColor = true;
            this.btnAllClear.Click += new System.EventHandler(this.btnAllClear_Click);
            this.btnAllClear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum1
            // 
            this.btnNum1.Location = new System.Drawing.Point(7, 69);
            this.btnNum1.Name = "btnNum1";
            this.btnNum1.Size = new System.Drawing.Size(92, 53);
            this.btnNum1.TabIndex = 14;
            this.btnNum1.Text = "1";
            this.btnNum1.UseVisualStyleBackColor = true;
            this.btnNum1.Click += new System.EventHandler(this.btnNum1_Click);
            this.btnNum1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum2
            // 
            this.btnNum2.Location = new System.Drawing.Point(103, 69);
            this.btnNum2.Name = "btnNum2";
            this.btnNum2.Size = new System.Drawing.Size(92, 53);
            this.btnNum2.TabIndex = 15;
            this.btnNum2.Text = "2";
            this.btnNum2.UseVisualStyleBackColor = true;
            this.btnNum2.Click += new System.EventHandler(this.btnNum2_Click);
            this.btnNum2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum3
            // 
            this.btnNum3.Location = new System.Drawing.Point(199, 69);
            this.btnNum3.Name = "btnNum3";
            this.btnNum3.Size = new System.Drawing.Size(92, 53);
            this.btnNum3.TabIndex = 16;
            this.btnNum3.Text = "3";
            this.btnNum3.UseVisualStyleBackColor = true;
            this.btnNum3.Click += new System.EventHandler(this.btnNum3_Click);
            this.btnNum3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum4
            // 
            this.btnNum4.Location = new System.Drawing.Point(7, 129);
            this.btnNum4.Name = "btnNum4";
            this.btnNum4.Size = new System.Drawing.Size(92, 53);
            this.btnNum4.TabIndex = 17;
            this.btnNum4.Text = "4";
            this.btnNum4.UseVisualStyleBackColor = true;
            this.btnNum4.Click += new System.EventHandler(this.btnNum4_Click);
            this.btnNum4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum5
            // 
            this.btnNum5.Location = new System.Drawing.Point(103, 129);
            this.btnNum5.Name = "btnNum5";
            this.btnNum5.Size = new System.Drawing.Size(92, 53);
            this.btnNum5.TabIndex = 18;
            this.btnNum5.Text = "5";
            this.btnNum5.UseVisualStyleBackColor = true;
            this.btnNum5.Click += new System.EventHandler(this.btnNum5_Click);
            this.btnNum5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum6
            // 
            this.btnNum6.Location = new System.Drawing.Point(199, 129);
            this.btnNum6.Name = "btnNum6";
            this.btnNum6.Size = new System.Drawing.Size(92, 53);
            this.btnNum6.TabIndex = 19;
            this.btnNum6.Text = "6";
            this.btnNum6.UseVisualStyleBackColor = true;
            this.btnNum6.Click += new System.EventHandler(this.btnNum6_Click);
            this.btnNum6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum7
            // 
            this.btnNum7.Location = new System.Drawing.Point(7, 189);
            this.btnNum7.Name = "btnNum7";
            this.btnNum7.Size = new System.Drawing.Size(92, 53);
            this.btnNum7.TabIndex = 20;
            this.btnNum7.Text = "7";
            this.btnNum7.UseVisualStyleBackColor = true;
            this.btnNum7.Click += new System.EventHandler(this.btnNum7_Click);
            this.btnNum7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnBackspace
            // 
            this.btnBackspace.Location = new System.Drawing.Point(199, 309);
            this.btnBackspace.Name = "btnBackspace";
            this.btnBackspace.Size = new System.Drawing.Size(92, 53);
            this.btnBackspace.TabIndex = 25;
            this.btnBackspace.Text = "←Backspace";
            this.btnBackspace.UseVisualStyleBackColor = true;
            this.btnBackspace.Click += new System.EventHandler(this.btnBackspace_Click);
            this.btnBackspace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum8
            // 
            this.btnNum8.Location = new System.Drawing.Point(103, 189);
            this.btnNum8.Name = "btnNum8";
            this.btnNum8.Size = new System.Drawing.Size(92, 53);
            this.btnNum8.TabIndex = 21;
            this.btnNum8.Text = "8";
            this.btnNum8.UseVisualStyleBackColor = true;
            this.btnNum8.Click += new System.EventHandler(this.btnNum8_Click);
            this.btnNum8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum0
            // 
            this.btnNum0.Location = new System.Drawing.Point(103, 249);
            this.btnNum0.Name = "btnNum0";
            this.btnNum0.Size = new System.Drawing.Size(92, 53);
            this.btnNum0.TabIndex = 24;
            this.btnNum0.Text = "0";
            this.btnNum0.UseVisualStyleBackColor = true;
            this.btnNum0.Click += new System.EventHandler(this.btnNum0_Click);
            this.btnNum0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // btnNum9
            // 
            this.btnNum9.Location = new System.Drawing.Point(199, 189);
            this.btnNum9.Name = "btnNum9";
            this.btnNum9.Size = new System.Drawing.Size(92, 53);
            this.btnNum9.TabIndex = 22;
            this.btnNum9.Text = "9";
            this.btnNum9.UseVisualStyleBackColor = true;
            this.btnNum9.Click += new System.EventHandler(this.btnNum9_Click);
            this.btnNum9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDialNum_KeyPress);
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.BackgroundImage = global::DispatchClientDemo.Properties.Resources.Close;
            this.pbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbClose.Location = new System.Drawing.Point(268, 2);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(29, 20);
            this.pbClose.TabIndex = 63;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            this.pbClose.MouseEnter += new System.EventHandler(this.pbClose_MouseEnter);
            this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnSharp);
            this.panel1.Controls.Add(this.txtDialNum);
            this.panel1.Controls.Add(this.btnStar);
            this.panel1.Controls.Add(this.btnNum9);
            this.panel1.Controls.Add(this.btnSplit);
            this.panel1.Controls.Add(this.btnNum0);
            this.panel1.Controls.Add(this.btnNum8);
            this.panel1.Controls.Add(this.btnAllClear);
            this.panel1.Controls.Add(this.btnBackspace);
            this.panel1.Controls.Add(this.btnNum1);
            this.panel1.Controls.Add(this.btnNum7);
            this.panel1.Controls.Add(this.btnNum2);
            this.panel1.Controls.Add(this.btnNum6);
            this.panel1.Controls.Add(this.btnNum3);
            this.panel1.Controls.Add(this.btnNum5);
            this.panel1.Controls.Add(this.btnNum4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.ForeColor = System.Drawing.Color.Navy;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 371);
            this.panel1.TabIndex = 64;
            // 
            // frmDialPlate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(299, 371);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDialPlate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDialPlate";
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSharp;
        private System.Windows.Forms.Button btnStar;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.TextBox txtDialNum;
        private System.Windows.Forms.Button btnAllClear;
        private System.Windows.Forms.Button btnNum1;
        private System.Windows.Forms.Button btnNum2;
        private System.Windows.Forms.Button btnNum3;
        private System.Windows.Forms.Button btnNum4;
        private System.Windows.Forms.Button btnNum5;
        private System.Windows.Forms.Button btnNum6;
        private System.Windows.Forms.Button btnNum7;
        private System.Windows.Forms.Button btnBackspace;
        private System.Windows.Forms.Button btnNum8;
        private System.Windows.Forms.Button btnNum0;
        private System.Windows.Forms.Button btnNum9;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel1;
    }
}