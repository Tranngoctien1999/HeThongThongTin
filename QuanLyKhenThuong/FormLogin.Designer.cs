namespace QuanLyKhenThuong
{
    partial class FormLogin
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEditTenDN = new DevExpress.XtraEditors.TextEdit();
            this.textEditMK = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonDN = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonThoat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTenDN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMK.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(50, 70);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên đăng nhập:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(50, 119);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Mật khẩu:";
            // 
            // textEditTenDN
            // 
            this.textEditTenDN.Location = new System.Drawing.Point(175, 67);
            this.textEditTenDN.Name = "textEditTenDN";
            this.textEditTenDN.Size = new System.Drawing.Size(306, 22);
            this.textEditTenDN.TabIndex = 2;
            // 
            // textEditMK
            // 
            this.textEditMK.Location = new System.Drawing.Point(175, 116);
            this.textEditMK.Name = "textEditMK";
            this.textEditMK.Size = new System.Drawing.Size(306, 22);
            this.textEditMK.TabIndex = 3;
            // 
            // simpleButtonDN
            // 
            this.simpleButtonDN.Location = new System.Drawing.Point(175, 178);
            this.simpleButtonDN.Name = "simpleButtonDN";
            this.simpleButtonDN.Size = new System.Drawing.Size(94, 29);
            this.simpleButtonDN.TabIndex = 4;
            this.simpleButtonDN.Text = "Đăng nhập";
            this.simpleButtonDN.Click += new System.EventHandler(this.simpleButtonDN_Click);
            // 
            // simpleButtonThoat
            // 
            this.simpleButtonThoat.Location = new System.Drawing.Point(387, 178);
            this.simpleButtonThoat.Name = "simpleButtonThoat";
            this.simpleButtonThoat.Size = new System.Drawing.Size(94, 29);
            this.simpleButtonThoat.TabIndex = 5;
            this.simpleButtonThoat.Text = "Thoát";
            this.simpleButtonThoat.Click += new System.EventHandler(this.simpleButtonThoat_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 265);
            this.Controls.Add(this.simpleButtonThoat);
            this.Controls.Add(this.simpleButtonDN);
            this.Controls.Add(this.textEditMK);
            this.Controls.Add(this.textEditTenDN);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLogin";
            ((System.ComponentModel.ISupportInitialize)(this.textEditTenDN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditMK.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditTenDN;
        private DevExpress.XtraEditors.TextEdit textEditMK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonDN;
        private DevExpress.XtraEditors.SimpleButton simpleButtonThoat;
    }
}