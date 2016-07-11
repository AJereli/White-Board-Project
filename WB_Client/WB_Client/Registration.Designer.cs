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
            this.name = new System.Windows.Forms.Label();
            this.passwrod = new System.Windows.Forms.Label();
            this.email_lable = new System.Windows.Forms.Label();
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
            this.Email.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Email.Location = new System.Drawing.Point(67, 110);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(150, 30);
            this.Email.TabIndex = 4;
            this.Email.Text = "";
            this.Email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // Login
            // 
            this.Login.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Login.Location = new System.Drawing.Point(67, 38);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(150, 30);
            this.Login.TabIndex = 3;
            this.Login.Text = "";
            this.Login.TextChanged += new System.EventHandler(this.Login_TextChanged);
            // 
            // Password
            // 
            this.Password.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Password.Location = new System.Drawing.Point(67, 74);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(150, 30);
            this.Password.TabIndex = 6;
            this.Password.Text = "";
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name.Location = new System.Drawing.Point(3, 45);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(38, 17);
            this.name.TabIndex = 7;
            this.name.Text = "Login";
            // 
            // passwrod
            // 
            this.passwrod.AutoSize = true;
            this.passwrod.Font = new System.Drawing.Font("Franklin Gothic Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.passwrod.Location = new System.Drawing.Point(3, 82);
            this.passwrod.Name = "passwrod";
            this.passwrod.Size = new System.Drawing.Size(55, 16);
            this.passwrod.TabIndex = 8;
            this.passwrod.Text = "Password";
            // 
            // email_lable
            // 
            this.email_lable.AutoSize = true;
            this.email_lable.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.email_lable.Location = new System.Drawing.Point(3, 117);
            this.email_lable.Name = "email_lable";
            this.email_lable.Size = new System.Drawing.Size(39, 17);
            this.email_lable.TabIndex = 9;
            this.email_lable.Text = "Email";
            // 
            // Registration
            // 
            this.AcceptButton = this.Enter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.email_lable);
            this.Controls.Add(this.passwrod);
            this.Controls.Add(this.name);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Enter);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "Registration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация";
            this.Load += new System.EventHandler(this.Registration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Enter;
        private System.Windows.Forms.RichTextBox Email;
        private System.Windows.Forms.RichTextBox Login;
        private System.Windows.Forms.RichTextBox Password;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label passwrod;
        private System.Windows.Forms.Label email_lable;
    }
}