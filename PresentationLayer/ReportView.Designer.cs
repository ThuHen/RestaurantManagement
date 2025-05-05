namespace PresentationLayer
{
    partial class ReportsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsView));
            this.label1 = new System.Windows.Forms.Label();
            this.btnMenu = new Guna.UI2.WinForms.Guna2Button();
            this.staffBtn = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Reports";
            // 
            // btnMenu
            // 
            this.btnMenu.AutoRoundedCorners = true;
            this.btnMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(89)))));
            this.btnMenu.BorderRadius = 39;
            this.btnMenu.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMenu.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(86)))));
            this.btnMenu.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btnMenu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMenu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMenu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMenu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMenu.FillColor = System.Drawing.Color.Transparent;
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Image = global::PresentationLayer.Properties.Resources.icons8_agreement_100;
            this.btnMenu.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnMenu.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnMenu.Location = new System.Drawing.Point(94, 136);
            this.btnMenu.Margin = new System.Windows.Forms.Padding(2);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(177, 80);
            this.btnMenu.TabIndex = 5;
            this.btnMenu.Text = "Menu list";
            this.btnMenu.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnMenu.TextOffset = new System.Drawing.Point(20, 0);
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // staffBtn
            // 
            this.staffBtn.AutoRoundedCorners = true;
            this.staffBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(89)))));
            this.staffBtn.BorderRadius = 39;
            this.staffBtn.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.staffBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(85)))), ((int)(((byte)(86)))));
            this.staffBtn.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.staffBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.staffBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.staffBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.staffBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.staffBtn.FillColor = System.Drawing.Color.Transparent;
            this.staffBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.staffBtn.ForeColor = System.Drawing.Color.White;
            this.staffBtn.Image = global::PresentationLayer.Properties.Resources.icons8_agreement_100;
            this.staffBtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.staffBtn.ImageOffset = new System.Drawing.Point(10, 0);
            this.staffBtn.Location = new System.Drawing.Point(296, 136);
            this.staffBtn.Margin = new System.Windows.Forms.Padding(2);
            this.staffBtn.Name = "staffBtn";
            this.staffBtn.Size = new System.Drawing.Size(177, 80);
            this.staffBtn.TabIndex = 7;
            this.staffBtn.Text = "Staff list";
            this.staffBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.staffBtn.TextOffset = new System.Drawing.Point(20, 0);
            this.staffBtn.Click += new System.EventHandler(this.staffBtn_Click);
            // 
            // ReportsView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1171, 769);
            this.Controls.Add(this.staffBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReportsView";
            this.Text = "KitchenView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnMenu;
        private Guna.UI2.WinForms.Guna2Button staffBtn;
    }
}