namespace Tetris
{
    partial class FormTetris
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LblCurrentScore = new System.Windows.Forms.Label();
            this.PlNextFigure = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.BtnStartGame = new Tetris.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PlNextFigure);
            this.splitContainer1.Panel1.Controls.Add(this.BtnStartGame);
            this.splitContainer1.Size = new System.Drawing.Size(384, 503);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            // 
            // LblCurrentScore
            // 
            this.LblCurrentScore.AutoSize = true;
            this.LblCurrentScore.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LblCurrentScore.Location = new System.Drawing.Point(12, 170);
            this.LblCurrentScore.Name = "LblCurrentScore";
            this.LblCurrentScore.Size = new System.Drawing.Size(67, 19);
            this.LblCurrentScore.TabIndex = 3;
            this.LblCurrentScore.Text = "Очки: 0";
            this.LblCurrentScore.Visible = false;
            // 
            // PlNextFigure
            // 
            this.PlNextFigure.Location = new System.Drawing.Point(13, 55);
            this.PlNextFigure.Name = "PlNextFigure";
            this.PlNextFigure.Size = new System.Drawing.Size(101, 101);
            this.PlNextFigure.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BtnStartGame
            // 
            this.BtnStartGame.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnStartGame.Location = new System.Drawing.Point(3, 3);
            this.BtnStartGame.Name = "BtnStartGame";
            this.BtnStartGame.Size = new System.Drawing.Size(119, 28);
            this.BtnStartGame.TabIndex = 1;
            this.BtnStartGame.Text = "Начать";
            this.BtnStartGame.UseVisualStyleBackColor = true;
            this.BtnStartGame.Click += new System.EventHandler(this.BtnStartGame_Click);
            // 
            // FormTetris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(384, 503);
            this.Controls.Add(this.LblCurrentScore);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FormTetris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тетрис";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.SplitContainer splitContainer1;
        private MyButton BtnStartGame;
        public System.Windows.Forms.Panel PlNextFigure;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label LblCurrentScore;
    }
}

