namespace WB_Client
{
    partial class Authorization
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoginTextBox = new System.Windows.Forms.RichTextBox();
            this.PasswordTextBox = new System.Windows.Forms.RichTextBox();
            this.EnterBatton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.Location = new System.Drawing.Point(60, 69);
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(150, 30);
            this.LoginTextBox.TabIndex = 0;
            this.LoginTextBox.Text = "";
            this.LoginTextBox.TextChanged += new System.EventHandler(this.LoginTextBox_TextChanged);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(60, 126);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(150, 30);
            this.PasswordTextBox.TabIndex = 1;
            this.PasswordTextBox.Text = "";
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // EnterBatton
            // 
            this.EnterBatton.Location = new System.Drawing.Point(96, 182);
            this.EnterBatton.Name = "EnterBatton";
            this.EnterBatton.Size = new System.Drawing.Size(75, 23);
            this.EnterBatton.TabIndex = 2;
            this.EnterBatton.Text = "Войти";
            this.EnterBatton.UseVisualStyleBackColor = true;
            this.EnterBatton.Click += new System.EventHandler(this.EnterBatton_Click);
            // 
            // Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.EnterBatton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.LoginTextBox);
            this.Name = "Authorization";
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox LoginTextBox;
        private System.Windows.Forms.RichTextBox PasswordTextBox;
        private System.Windows.Forms.Button EnterBatton;
    }
}

