namespace WB_Client
{
    partial class Registration
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
            this.Enter = new System.Windows.Forms.Button();
            this.Email = new System.Windows.Forms.RichTextBox();
            this.Login = new System.Windows.Forms.RichTextBox();
            this.Password = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Enter
            // 
            this.Enter.Location = new System.Drawing.Point(67, 183);
            this.Enter.Name = "Enter";
            this.Enter.Size = new System.Drawing.Size(150, 23);
            this.Enter.TabIndex = 5;
            this.Enter.Text = "Зарегистрироваться";
            this.Enter.UseVisualStyleBackColor = true;
            this.Enter.Click += new System.EventHandler(this.Enter_Click);
            // 
            // Email
            // 
            this.Email.Location = new System.Drawing.Point(67, 87);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(150, 30);
            this.Email.TabIndex = 4;
            this.Email.Text = "";
            this.Email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(67, 38);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(150, 30);
            this.Login.TabIndex = 3;
            this.Login.Text = "";
            this.Login.TextChanged += new System.EventHandler(this.Login_TextChanged);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(67, 135);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(150, 30);
            this.Password.TabIndex = 6;
            this.Password.Text = "";
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Enter);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.Login);
            this.Name = "Registration";
            this.Text = "Registration";
            this.Load += new System.EventHandler(this.Registration_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Enter;
        private System.Windows.Forms.RichTextBox Email;
        private System.Windows.Forms.RichTextBox Login;
        private System.Windows.Forms.RichTextBox Password;
    }
}