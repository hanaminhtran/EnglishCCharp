using EnglishWhale.View;
using System.Drawing;
using System.Runtime.Remoting.Messaging;

namespace EnglishWhale.View
{
    partial class ChooseAnswerQuizForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseAnswerQuizForm));

            this.questionTextBox = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerBar = new System.Windows.Forms.ProgressBar();
            this.timerLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // 
            // questionTextBox
            // 
            this.questionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.questionTextBox.BackColor = System.Drawing.Color.Goldenrod;
            this.questionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.questionTextBox.Location = new System.Drawing.Point(12, 46);
            this.questionTextBox.Name = "questionTextBox";
            this.questionTextBox.ReadOnly = true;
            this.questionTextBox.Size = new System.Drawing.Size(560, 132);
            this.questionTextBox.TabIndex = 5;
            this.questionTextBox.Text = "Text";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EnglishWhale.Properties.Resources.volume;
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseEnter += new System.EventHandler(this.VolumePic_MouseEnter);
            this.pictureBox1.BackColor = Color.FromArgb(0);
            // 
            // timerBar
            // 
            this.timerBar.Location = new System.Drawing.Point(12, 218);
            this.timerBar.Name = "timerBar";
            this.timerBar.Size = new System.Drawing.Size(559, 27);
            this.timerBar.TabIndex = 7;
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.ForeColor = System.Drawing.Color.White;
            this.timerLabel.BackColor = System.Drawing.Color.FromArgb(0);
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timerLabel.Location = new System.Drawing.Point(264, 195);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(48, 20);
            this.timerLabel.TabIndex = 8;
            this.timerLabel.Text = "Timer";
            // 
            // ChooseAnswerQuizForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(584, 762);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.timerBar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.questionTextBox);
            this.Controls.Add(GetButton4());
            this.Controls.Add(GetButton3());
            this.Controls.Add(GetButton2());
            this.Controls.Add(GetButton1());
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.BackgroundImage = global::EnglishWhale.Properties.Resources.background_1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChooseAnswerQuizForm";
            this.Text = "Choose Answer Quiz";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MaximizeBox = false;
            this.CenterToScreen();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button GetButton4()
        {
            if (this.button4 == null)
            {
                this.button4 = new CustomButton();
                this.button4.Font = font;
                this.button4.Location = new System.Drawing.Point(372, 554);
                this.button4.Name = "button4";
                this.button4.Size = new System.Drawing.Size(200, 150);
                this.button4.TabIndex = 4;
                this.button4.Text = "button4";
                this.button4.UseVisualStyleBackColor = true;
            }
            return this.button4;
        }

        private System.Windows.Forms.Button GetButton3()
        {
            if (this.button3 == null)
            {
                this.button3 = new CustomButton();
                this.button3.Font = font;
                this.button3.Location = new System.Drawing.Point(12, 554);
                this.button3.Name = "button3";
                this.button3.Size = new System.Drawing.Size(200, 150);
                this.button3.TabIndex = 3;
                this.button3.Text = "button3";
                this.button3.UseVisualStyleBackColor = true;
            }
            return this.button3;
        }

        private System.Windows.Forms.Button GetButton2()
        {
            if (this.button2 == null)
            {
                this.button2 = new CustomButton();
                this.button2.Font = font;
                this.button2.Location = new System.Drawing.Point(372, 347);
                this.button2.Name = "button2";
                this.button2.Size = new System.Drawing.Size(200, 150);
                this.button2.TabIndex = 2;
                this.button2.Text = "button2";
                this.button2.UseVisualStyleBackColor = true;
            }
            return this.button2;
        }

        private System.Windows.Forms.Button GetButton1()
        {
            if (this.button1 == null)
            {
                this.button1 = new CustomButton();
                this.button1.Font = font;
                this.button1.Location = new System.Drawing.Point(12, 347);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(200, 150);
                this.button1.TabIndex = 1;
                this.button1.Text = "button1";
                this.button1.UseVisualStyleBackColor = true;
            }
            return this.button1;
        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RichTextBox questionTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar timerBar;
        private System.Windows.Forms.Label timerLabel;
        private readonly System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
    }
}