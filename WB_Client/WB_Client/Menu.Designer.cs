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
            this.labelForChoosingColour = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // exitingFromProgram
            // 
            this.exitingFromProgram.Location = new System.Drawing.Point(644, 427);
            this.exitingFromProgram.Name = "exitingFromProgram";
            this.exitingFromProgram.Size = new System.Drawing.Size(120, 23);
            this.exitingFromProgram.TabIndex = 0;
            this.exitingFromProgram.Text = "Exit";
            this.exitingFromProgram.UseVisualStyleBackColor = true;
            this.exitingFromProgram.Click += new System.EventHandler(this.exitingFromBoard_Click);
            // 
            // loadOfBoard
            // 
            this.loadOfBoard.Location = new System.Drawing.Point(644, 98);
            this.loadOfBoard.Name = "loadOfBoard";
            this.loadOfBoard.Size = new System.Drawing.Size(120, 34);
            this.loadOfBoard.TabIndex = 1;
            this.loadOfBoard.Text = "Load Board ";
            this.loadOfBoard.UseVisualStyleBackColor = true;
            this.loadOfBoard.Click += new System.EventHandler(this.loadOfBoard_Click);
            // 
            // creatingOfBoard
            // 
            this.creatingOfBoard.Location = new System.Drawing.Point(644, 24);
            this.creatingOfBoard.Name = "creatingOfBoard";
            this.creatingOfBoard.Size = new System.Drawing.Size(120, 23);
            this.creatingOfBoard.TabIndex = 2;
            this.creatingOfBoard.Text = "Create new Board";
            this.creatingOfBoard.UseVisualStyleBackColor = true;
            this.creatingOfBoard.Click += new System.EventHandler(this.creatingOfBoard_Click);
            // 
            // labelForChoosingColour
            // 
            this.labelForChoosingColour.AutoSize = true;
            this.labelForChoosingColour.Location = new System.Drawing.Point(12, 167);
            this.labelForChoosingColour.Name = "labelForChoosingColour";
            this.labelForChoosingColour.Size = new System.Drawing.Size(149, 13);
            this.labelForChoosingColour.TabIndex = 3;
            this.labelForChoosingColour.Text = "Choose standart colour of pen";
            this.labelForChoosingColour.Click += new System.EventHandler(this.label1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 462);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelForChoosingColour);
            this.Controls.Add(this.creatingOfBoard);
            this.Controls.Add(this.loadOfBoard);
            this.Controls.Add(this.exitingFromProgram);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitingFromProgram;
        private System.Windows.Forms.Button loadOfBoard;
        private System.Windows.Forms.Button creatingOfBoard;
        private System.Windows.Forms.Label labelForChoosingColour;
        private System.Windows.Forms.Label label1;
    }
}

