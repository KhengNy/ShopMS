namespace ShopMS
{
    partial class SetUpLisence
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
            this.btnGenKey = new System.Windows.Forms.Button();
            this.txtGenKey = new System.Windows.Forms.TextBox();
            this.txtSetKey = new System.Windows.Forms.TextBox();
            this.btnSetKey = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenKey
            // 
            this.btnGenKey.Location = new System.Drawing.Point(370, 24);
            this.btnGenKey.Name = "btnGenKey";
            this.btnGenKey.Size = new System.Drawing.Size(98, 23);
            this.btnGenKey.TabIndex = 0;
            this.btnGenKey.Text = "Generate Key";
            this.btnGenKey.UseVisualStyleBackColor = true;
            this.btnGenKey.Click += new System.EventHandler(this.btnGenKey_Click);
            // 
            // txtGenKey
            // 
            this.txtGenKey.Location = new System.Drawing.Point(12, 26);
            this.txtGenKey.Name = "txtGenKey";
            this.txtGenKey.Size = new System.Drawing.Size(352, 20);
            this.txtGenKey.TabIndex = 1;
            // 
            // txtSetKey
            // 
            this.txtSetKey.Location = new System.Drawing.Point(12, 64);
            this.txtSetKey.Name = "txtSetKey";
            this.txtSetKey.Size = new System.Drawing.Size(352, 20);
            this.txtSetKey.TabIndex = 3;
            // 
            // btnSetKey
            // 
            this.btnSetKey.Location = new System.Drawing.Point(370, 62);
            this.btnSetKey.Name = "btnSetKey";
            this.btnSetKey.Size = new System.Drawing.Size(98, 23);
            this.btnSetKey.TabIndex = 2;
            this.btnSetKey.Text = "Set Lisence";
            this.btnSetKey.UseVisualStyleBackColor = true;
            this.btnSetKey.Click += new System.EventHandler(this.btnSetKey_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(176, 90);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(98, 23);
            this.btnCheck.TabIndex = 4;
            this.btnCheck.Text = "Check Lisence";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // SetUpLisence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 150);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txtSetKey);
            this.Controls.Add(this.btnSetKey);
            this.Controls.Add(this.txtGenKey);
            this.Controls.Add(this.btnGenKey);
            this.Name = "SetUpLisence";
            this.Text = "SetUpLisence";
            this.Load += new System.EventHandler(this.SetUpLisence_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenKey;
        private System.Windows.Forms.TextBox txtGenKey;
        private System.Windows.Forms.TextBox txtSetKey;
        private System.Windows.Forms.Button btnSetKey;
        private System.Windows.Forms.Button btnCheck;
    }
}