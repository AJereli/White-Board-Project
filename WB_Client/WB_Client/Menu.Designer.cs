namespace WB_Client
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.exitingFromProgram = new System.Windows.Forms.Button();
            this.loadOfBoard = new System.Windows.Forms.Button();
            this.creatingOfBoard = new System.Windows.Forms.Button();
            this.UserName = new System.Windows.Forms.RichTextBox();
            this.connectInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // exitingFromProgram
            // 
            this.exitingFromProgram.Location = new System.Drawing.Point(77, 254);
            this.exitingFromProgram.Name = "exitingFromProgram";
            this.exitingFromProgram.Size = new System.Drawing.Size(120, 23);
            this.exitingFromProgram.TabIndex = 0;
            this.exitingFromProgram.Text = "Выход";
            this.exitingFromProgram.UseVisualStyleBackColor = true;
            this.exitingFromProgram.Click += new System.EventHandler(this.exitingFromBoard_Click);
            // 
            // loadOfBoard
            // 
            this.loadOfBoard.Location = new System.Drawing.Point(77, 160);
            this.loadOfBoard.Name = "loadOfBoard";
            this.loadOfBoard.Size = new System.Drawing.Size(120, 34);
            this.loadOfBoard.TabIndex = 1;
            this.loadOfBoard.Text = "Подключиться";
            this.loadOfBoard.UseVisualStyleBackColor = true;
            this.loadOfBoard.Click += new System.EventHandler(this.loadOfBoard_Click);
            // 
            // creatingOfBoard
            // 
            this.creatingOfBoard.Location = new System.Drawing.Point(77, 21);
            this.creatingOfBoard.Name = "creatingOfBoard";
            this.creatingOfBoard.Size = new System.Drawing.Size(120, 36);
            this.creatingOfBoard.TabIndex = 2;
            this.creatingOfBoard.Text = "Создать новую доску";
            this.creatingOfBoard.UseVisualStyleBackColor = true;
            this.creatingOfBoard.Click += new System.EventHandler(this.creatingOfBoard_Click);
            // 
            // UserName
            // 
            this.UserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserName.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UserName.Location = new System.Drawing.Point(77, 124);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(120, 30);
            this.UserName.TabIndex = 3;
            this.UserName.Text = "";
            // 
            // connectInfo
            // 
            this.connectInfo.AutoSize = true;
            this.connectInfo.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectInfo.ForeColor = System.Drawing.Color.Black;
            this.connectInfo.Location = new System.Drawing.Point(74, 104);
            this.connectInfo.Name = "connectInfo";
            this.connectInfo.Size = new System.Drawing.Size(128, 17);
            this.connectInfo.TabIndex = 4;
            this.connectInfo.Text = "Имя создателя доски";
            // 
            // Menu
            // 
            this.AcceptButton = this.loadOfBoard;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WB_Client.Properties.Resources.pam_pam_menu2;
            this.ClientSize = new System.Drawing.Size(284, 316);
            this.Controls.Add(this.connectInfo);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.creatingOfBoard);
            this.Controls.Add(this.loadOfBoard);
            this.Controls.Add(this.exitingFromProgram);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Good idea! - username";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Menu_FormClosed);
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitingFromProgram;
        private System.Windows.Forms.Button loadOfBoard;
        private System.Windows.Forms.Button creatingOfBoard;
        private System.Windows.Forms.RichTextBox UserName;
        private System.Windows.Forms.Label connectInfo;
    }
}

