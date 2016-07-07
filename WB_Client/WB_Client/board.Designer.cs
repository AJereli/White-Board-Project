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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.anyColor = new System.Windows.Forms.PictureBox();
            this.yellow = new System.Windows.Forms.PictureBox();
            this.blue = new System.Windows.Forms.PictureBox();
            this.green = new System.Windows.Forms.PictureBox();
            this.pink = new System.Windows.Forms.PictureBox();
            this.red = new System.Windows.Forms.PictureBox();
            this.black = new System.Windows.Forms.PictureBox();
            this.rect = new System.Windows.Forms.PictureBox();
            this.line = new System.Windows.Forms.PictureBox();
            this.Pencil = new System.Windows.Forms.PictureBox();
            this.Select = new System.Windows.Forms.PictureBox();
            this.select_thickness = new System.Windows.Forms.TrackBar();
            this.timerFoServ = new System.Windows.Forms.Timer(this.components);
            this.user_name = new System.Windows.Forms.Label();
            this.debug_lable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.anyColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yellow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.black)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.line)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pencil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Select)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.select_thickness)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(696, 448);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(158, 96);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // anyColor
            // 
            this.anyColor.BackgroundImage = global::WB_Client.Properties.Resources.select_color;
            this.anyColor.InitialImage = global::WB_Client.Properties.Resources.select_color;
            this.anyColor.Location = new System.Drawing.Point(467, 12);
            this.anyColor.Name = "anyColor";
            this.anyColor.Size = new System.Drawing.Size(30, 30);
            this.anyColor.TabIndex = 11;
            this.anyColor.TabStop = false;
            this.anyColor.Click += new System.EventHandler(this.anyColor_Click);
            // 
            // yellow
            // 
            this.yellow.BackgroundImage = global::WB_Client.Properties.Resources.yellow;
            this.yellow.Image = global::WB_Client.Properties.Resources.yellow;
            this.yellow.InitialImage = global::WB_Client.Properties.Resources.yellow;
            this.yellow.Location = new System.Drawing.Point(431, 12);
            this.yellow.Name = "yellow";
            this.yellow.Size = new System.Drawing.Size(30, 30);
            this.yellow.TabIndex = 10;
            this.yellow.TabStop = false;
            this.yellow.Click += new System.EventHandler(this.yellow_Click);
            // 
            // blue
            // 
            this.blue.BackgroundImage = global::WB_Client.Properties.Resources.blue;
            this.blue.InitialImage = global::WB_Client.Properties.Resources.blue;
            this.blue.Location = new System.Drawing.Point(395, 12);
            this.blue.Name = "blue";
            this.blue.Size = new System.Drawing.Size(30, 30);
            this.blue.TabIndex = 9;
            this.blue.TabStop = false;
            this.blue.Click += new System.EventHandler(this.blue_Click);
            // 
            // green
            // 
            this.green.BackgroundImage = global::WB_Client.Properties.Resources.green;
            this.green.InitialImage = global::WB_Client.Properties.Resources.green;
            this.green.Location = new System.Drawing.Point(359, 12);
            this.green.Name = "green";
            this.green.Size = new System.Drawing.Size(30, 30);
            this.green.TabIndex = 8;
            this.green.TabStop = false;
            this.green.Click += new System.EventHandler(this.green_Click);
            // 
            // pink
            // 
            this.pink.BackgroundImage = global::WB_Client.Properties.Resources.pink;
            this.pink.InitialImage = global::WB_Client.Properties.Resources.pink;
            this.pink.Location = new System.Drawing.Point(323, 12);
            this.pink.Name = "pink";
            this.pink.Size = new System.Drawing.Size(30, 30);
            this.pink.TabIndex = 7;
            this.pink.TabStop = false;
            this.pink.Click += new System.EventHandler(this.pink_Click);
            // 
            // red
            // 
            this.red.BackgroundImage = global::WB_Client.Properties.Resources.red;
            this.red.InitialImage = global::WB_Client.Properties.Resources.red;
            this.red.Location = new System.Drawing.Point(287, 12);
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(30, 30);
            this.red.TabIndex = 6;
            this.red.TabStop = false;
            this.red.Click += new System.EventHandler(this.red_Click);
            // 
            // black
            // 
            this.black.BackgroundImage = global::WB_Client.Properties.Resources.black;
            this.black.InitialImage = global::WB_Client.Properties.Resources.black;
            this.black.Location = new System.Drawing.Point(251, 12);
            this.black.Name = "black";
            this.black.Size = new System.Drawing.Size(30, 30);
            this.black.TabIndex = 5;
            this.black.TabStop = false;
            this.black.Click += new System.EventHandler(this.black_Click);
            // 
            // rect
            // 
            this.rect.BackgroundImage = global::WB_Client.Properties.Resources.rect;
            this.rect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rect.InitialImage = global::WB_Client.Properties.Resources.rect;
            this.rect.Location = new System.Drawing.Point(118, 12);
            this.rect.Name = "rect";
            this.rect.Size = new System.Drawing.Size(30, 30);
            this.rect.TabIndex = 4;
            this.rect.TabStop = false;
            // 
            // line
            // 
            this.line.BackgroundImage = global::WB_Client.Properties.Resources.line;
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line.InitialImage = global::WB_Client.Properties.Resources.line;
            this.line.Location = new System.Drawing.Point(82, 12);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(30, 30);
            this.line.TabIndex = 3;
            this.line.TabStop = false;
            // 
            // Pencil
            // 
            this.Pencil.BackgroundImage = global::WB_Client.Properties.Resources.pencil;
            this.Pencil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pencil.InitialImage = global::WB_Client.Properties.Resources.pencil;
            this.Pencil.Location = new System.Drawing.Point(46, 12);
            this.Pencil.Name = "Pencil";
            this.Pencil.Size = new System.Drawing.Size(30, 30);
            this.Pencil.TabIndex = 2;
            this.Pencil.TabStop = false;
            this.Pencil.Click += new System.EventHandler(this.Pen_Click);
            // 
            // Select
            // 
            this.Select.BackgroundImage = global::WB_Client.Properties.Resources.select;
            this.Select.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Select.InitialImage = global::WB_Client.Properties.Resources.select;
            this.Select.Location = new System.Drawing.Point(8, 12);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(30, 30);
            this.Select.TabIndex = 1;
            this.Select.TabStop = false;
            this.Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // select_thickness
            // 
            this.select_thickness.AutoSize = false;
            this.select_thickness.Cursor = System.Windows.Forms.Cursors.Hand;
            this.select_thickness.LargeChange = 2;
            this.select_thickness.Location = new System.Drawing.Point(538, 12);
            this.select_thickness.MaximumSize = new System.Drawing.Size(150, 30);
            this.select_thickness.Minimum = 1;
            this.select_thickness.MinimumSize = new System.Drawing.Size(150, 30);
            this.select_thickness.Name = "select_thickness";
            this.select_thickness.Size = new System.Drawing.Size(150, 30);
            this.select_thickness.TabIndex = 12;
            this.select_thickness.TabStop = false;
            this.select_thickness.Tag = "Тощина кисти";
            this.select_thickness.Value = 2;
            this.select_thickness.Scroll += new System.EventHandler(this.select_thickness_Scroll);
            // 
            // timerFoServ
            // 
            this.timerFoServ.Tick += new System.EventHandler(this.timerFoServ_Tick);
            // 
            // user_name
            // 
            this.user_name.AutoSize = true;
            this.user_name.Location = new System.Drawing.Point(192, 20);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(35, 13);
            this.user_name.TabIndex = 14;
            this.user_name.Text = "label2";
            this.user_name.Click += new System.EventHandler(this.user_name_Click);
            // 
            // debug_lable
            // 
            this.debug_lable.AutoSize = true;
            this.debug_lable.Location = new System.Drawing.Point(392, 490);
            this.debug_lable.MinimumSize = new System.Drawing.Size(200, 25);
            this.debug_lable.Name = "debug_lable";
            this.debug_lable.Size = new System.Drawing.Size(200, 25);
            this.debug_lable.TabIndex = 15;
            this.debug_lable.Text = "label1";
            this.debug_lable.Click += new System.EventHandler(this.debug_lable_Click);
            // 
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(866, 556);
            this.Controls.Add(this.debug_lable);
            this.Controls.Add(this.user_name);
            this.Controls.Add(this.select_thickness);
            this.Controls.Add(this.anyColor);
            this.Controls.Add(this.yellow);
            this.Controls.Add(this.blue);
            this.Controls.Add(this.green);
            this.Controls.Add(this.pink);
            this.Controls.Add(this.red);
            this.Controls.Add(this.black);
            this.Controls.Add(this.rect);
            this.Controls.Add(this.line);
            this.Controls.Add(this.Pencil);
            this.Controls.Add(this.Select);
            this.Controls.Add(this.richTextBox1);
            this.DoubleBuffered = true;
            this.Name = "Board";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Board";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Board_FormClosing);
            this.Load += new System.EventHandler(this.Board_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Board_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Board_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Board_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.anyColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yellow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.black)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.line)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pencil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Select)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.select_thickness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox Pencil;
        private System.Windows.Forms.PictureBox Select;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox line;
        private System.Windows.Forms.PictureBox rect;
        private System.Windows.Forms.PictureBox black;
        private System.Windows.Forms.PictureBox red;
        private System.Windows.Forms.PictureBox pink;
        private System.Windows.Forms.PictureBox green;
        private System.Windows.Forms.PictureBox blue;
        private System.Windows.Forms.PictureBox yellow;
        private System.Windows.Forms.PictureBox anyColor;
        private System.Windows.Forms.TrackBar select_thickness;
        private System.Windows.Forms.Timer timerFoServ;
        private System.Windows.Forms.Label user_name;
        private System.Windows.Forms.Label debug_lable;
    }
}