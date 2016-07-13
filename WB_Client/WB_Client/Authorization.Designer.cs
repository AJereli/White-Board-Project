namespace WB_Client
{
    partial class Authorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Authorization));
            this.Enter = new System.Windows.Forms.Button();
            this.registration = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.TextBox();
            this.name_a = new System.Windows.Forms.Label();
            this.password_a = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Enter
            // 
            this.Enter.Location = new System.Drawing.Point(107, 187);
            this.Enter.Name = "Enter";
            this.Enter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Enter.Size = new System.Drawing.Size(75, 23);
            this.Enter.TabIndex = 2;
            this.Enter.Text = "Войти";
            this.Enter.UseVisualStyleBackColor = true;
            this.Enter.Click += new System.EventHandler(this.Enter_Click);
            // 
            // registration
            // 
            this.registration.Location = new System.Drawing.Point(70, 281);
            this.registration.Name = "registration";
            this.registration.Size = new System.Drawing.Size(150, 23);
            this.registration.TabIndex = 3;
            this.registration.Text = "Регистрация";
            this.registration.UseVisualStyleBackColor = true;
            this.registration.Click += new System.EventHandler(this.registration_Click);
            // 
            // Password
            // 
            this.Password.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Password.Location = new System.Drawing.Point(87, 138);
            this.Password.Multiline = true;
            this.Password.Name = "Password";
            this.Password.PasswordChar = '•';
            this.Password.Size = new System.Drawing.Size(150, 30);
            this.Password.TabIndex = 2;
            // 
            // Login
            // 
            this.Login.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Login.Location = new System.Drawing.Point(87, 82);
            this.Login.Multiline = true;
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(150, 30);
            this.Login.TabIndex = 1;
            this.Login.TextChanged += new System.EventHandler(this.Login_TextChanged_1);
            // 
            // name_a
            // 
            this.name_a.AutoSize = true;
            this.name_a.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name_a.Location = new System.Drawing.Point(8, 89);
            this.name_a.Name = "name_a";
            this.name_a.Size = new System.Drawing.Size(38, 17);
            this.name_a.TabIndex = 6;
            this.name_a.Text = "Login";
            // 
            // password_a
            // 
            this.password_a.AutoSize = true;
            this.password_a.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password_a.Location = new System.Drawing.Point(8, 145);
            this.password_a.Name = "password_a";
            this.password_a.Size = new System.Drawing.Size(62, 17);
            this.password_a.TabIndex = 7;
            this.password_a.Text = "Password";
            // 
            // Authorization
            // 
            this.AcceptButton = this.Enter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WB_Client.Properties.Resources.pam_pam_501_note_offsset;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(284, 316);
            this.Controls.Add(this.password_a);
            this.Controls.Add(this.name_a);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.registration);
            this.Controls.Add(this.Enter);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Authorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Good idea! - Авторизация";
            this.Load += new System.EventHandler(this.Authorization_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Enter;
        private System.Windows.Forms.Button registration;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.Label name_a;
        private System.Windows.Forms.Label password_a;
    }
}