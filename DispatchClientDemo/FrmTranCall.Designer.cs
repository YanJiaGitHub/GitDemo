namespace DispatchClientDemo
{
    partial class FrmTranCall
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
            this.txtEmployee_C = new System.Windows.Forms.TextBox();
            this.btnConfsTran = new System.Windows.Forms.Button();
            this.btnTranCall = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmployee_A = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEmployee_C
            // 
            this.txtEmployee_C.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEmployee_C.Location = new System.Drawing.Point(83, 72);
            this.txtEmployee_C.Name = "txtEmployee_C";
            this.txtEmployee_C.Size = new System.Drawing.Size(223, 35);
            this.txtEmployee_C.TabIndex = 1;
            // 
            // btnConfsTran
            // 
            this.btnConfsTran.Location = new System.Drawing.Point(202, 123);
            this.btnConfsTran.Name = "btnConfsTran";
            this.btnConfsTran.Size = new System.Drawing.Size(104, 39);
            this.btnConfsTran.TabIndex = 3;
            this.btnConfsTran.Text = "协商转接";
            this.btnConfsTran.UseVisualStyleBackColor = true;
            this.btnConfsTran.Click += new System.EventHandler(this.btnConfsTran_Click);
            // 
            // btnTranCall
            // 
            this.btnTranCall.Location = new System.Drawing.Point(83, 123);
            this.btnTranCall.Name = "btnTranCall";
            this.btnTranCall.Size = new System.Drawing.Size(104, 39);
            this.btnTranCall.TabIndex = 2;
            this.btnTranCall.Text = "单步转接";
            this.btnTranCall.UseVisualStyleBackColor = true;
            this.btnTranCall.Click += new System.EventHandler(this.btnTranCall_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 105;
            this.label3.Text = "Employee_C";
            // 
            // txtEmployee_A
            // 
            this.txtEmployee_A.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEmployee_A.Location = new System.Drawing.Point(83, 22);
            this.txtEmployee_A.Name = "txtEmployee_A";
            this.txtEmployee_A.Size = new System.Drawing.Size(223, 35);
            this.txtEmployee_A.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 109;
            this.label2.Text = "Employee_A";
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.BackgroundImage = global::DispatchClientDemo.Properties.Resources.Close;
            this.pbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pbClose.Location = new System.Drawing.Point(333, -1);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(29, 20);
            this.pbClose.TabIndex = 114;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            this.pbClose.MouseEnter += new System.EventHandler(this.pbClose_MouseEnter);
            this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
            // 
            // FrmTranCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(363, 174);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEmployee_A);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEmployee_C);
            this.Controls.Add(this.btnTranCall);
            this.Controls.Add(this.btnConfsTran);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTranCall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmTranCall";
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmployee_C;
        private System.Windows.Forms.Button btnConfsTran;
        private System.Windows.Forms.Button btnTranCall;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmployee_A;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbClose;
    }
}