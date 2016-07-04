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
            this.exitingFromProgram = new System.Windows.Forms.Button();
            this.loadOfBoard = new System.Windows.Forms.Button();
            this.creatingOfBoard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exitingFromProgram
            // 
            this.exitingFromProgram.Location = new System.Drawing.Point(77, 134);
            this.exitingFromProgram.Name = "exitingFromProgram";
            this.exitingFromProgram.Size = new System.Drawing.Size(120, 23);
            this.exitingFromProgram.TabIndex = 0;
            this.exitingFromProgram.Text = "Exit";
            this.exitingFromProgram.UseVisualStyleBackColor = true;
            this.exitingFromProgram.Click += new System.EventHandler(this.exitingFromBoard_Click);
            // 
            // loadOfBoard
            // 
            this.loadOfBoard.Location = new System.Drawing.Point(77, 79);
            this.loadOfBoard.Name = "loadOfBoard";
            this.loadOfBoard.Size = new System.Drawing.Size(120, 23);
            this.loadOfBoard.TabIndex = 1;
            this.loadOfBoard.Text = "Load Board ";
            this.loadOfBoard.UseVisualStyleBackColor = true;
            this.loadOfBoard.Click += new System.EventHandler(this.loadOfBoard_Click);
            // 
            // creatingOfBoard
            // 
            this.creatingOfBoard.Location = new System.Drawing.Point(77, 21);
            this.creatingOfBoard.Name = "creatingOfBoard";
            this.creatingOfBoard.Size = new System.Drawing.Size(120, 23);
            this.creatingOfBoard.TabIndex = 2;
            this.creatingOfBoard.Text = "Create new Board";
            this.creatingOfBoard.UseVisualStyleBackColor = true;
            this.creatingOfBoard.Click += new System.EventHandler(this.creatingOfBoard_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.creatingOfBoard);
            this.Controls.Add(this.loadOfBoard);
            this.Controls.Add(this.exitingFromProgram);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exitingFromProgram;
        private System.Windows.Forms.Button loadOfBoard;
        private System.Windows.Forms.Button creatingOfBoard;
    }
}

