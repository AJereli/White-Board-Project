namespace WB_Client
{
    partial class Board
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
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Select = new System.Windows.Forms.PictureBox();
            this.Pen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Select)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pen)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(475, 324);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(158, 96);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Select
            // 
            this.Select.Location = new System.Drawing.Point(8, 11);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(24, 25);
            this.Select.TabIndex = 1;
            this.Select.TabStop = false;
            this.Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // Pen
            // 
            this.Pen.Location = new System.Drawing.Point(46, 11);
            this.Pen.Name = "Pen";
            this.Pen.Size = new System.Drawing.Size(31, 25);
            this.Pen.TabIndex = 2;
            this.Pen.TabStop = false;
            this.Pen.Click += new System.EventHandler(this.Pen_Click);
            // 
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(645, 432);
            this.Controls.Add(this.Pen);
            this.Controls.Add(this.Select);
            this.Controls.Add(this.richTextBox1);
            this.DoubleBuffered = true;
            this.Name = "Board";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Board";
            this.Load += new System.EventHandler(this.Board_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Board_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Board_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Board_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.Select)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox Pen;
        private System.Windows.Forms.PictureBox Select;
    }
}