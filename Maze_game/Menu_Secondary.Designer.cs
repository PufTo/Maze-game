namespace Maze_game
{
    partial class Menu_Secondary
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
            this.back = new System.Windows.Forms.Button();
            this.quit = new System.Windows.Forms.Button();
            this.leaderboard = new System.Windows.Forms.Button();
            this.howToPlay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // back
            // 
            this.back.Location = new System.Drawing.Point(78, 139);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(87, 23);
            this.back.TabIndex = 0;
            this.back.Text = "Back to menu";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // quit
            // 
            this.quit.Location = new System.Drawing.Point(78, 191);
            this.quit.Name = "quit";
            this.quit.Size = new System.Drawing.Size(87, 23);
            this.quit.TabIndex = 1;
            this.quit.Text = "Quit";
            this.quit.UseVisualStyleBackColor = true;
            this.quit.Click += new System.EventHandler(this.quit_Click);
            // 
            // leaderboard
            // 
            this.leaderboard.Location = new System.Drawing.Point(78, 35);
            this.leaderboard.Name = "leaderboard";
            this.leaderboard.Size = new System.Drawing.Size(87, 23);
            this.leaderboard.TabIndex = 2;
            this.leaderboard.Text = "Leaderboard";
            this.leaderboard.UseVisualStyleBackColor = true;
            this.leaderboard.Click += new System.EventHandler(this.leaderboard_Click);
            // 
            // howToPlay
            // 
            this.howToPlay.Location = new System.Drawing.Point(78, 87);
            this.howToPlay.Name = "howToPlay";
            this.howToPlay.Size = new System.Drawing.Size(87, 23);
            this.howToPlay.TabIndex = 3;
            this.howToPlay.Text = "How to play";
            this.howToPlay.UseVisualStyleBackColor = true;
            this.howToPlay.Click += new System.EventHandler(this.howToPlay_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Maze_game.Properties.Resources.menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(245, 261);
            this.Controls.Add(this.howToPlay);
            this.Controls.Add(this.leaderboard);
            this.Controls.Add(this.quit);
            this.Controls.Add(this.back);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form3";
            this.Text = "Menu";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form3_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button quit;
        private System.Windows.Forms.Button leaderboard;
        private System.Windows.Forms.Button howToPlay;
    }
}