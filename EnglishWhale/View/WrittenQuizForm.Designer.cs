using EnglishWhale.Controller;
using EnglishWhale.View;

namespace EnglishWhale
{
    partial class WrittenQuizForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WrittenQuizForm));

            writtenQuizPanel = new WrittenQuizPanel(mContr, 
                delegate (MainController mController) 
                { 
                    return mController.GetAnyRandomWordsPair(); 
                });
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 365);
            this.Controls.Add(this.writtenQuizPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "WrittenQuizForm";
            this.Text = "Written Quiz";
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.CenterToScreen();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private WrittenQuizPanel writtenQuizPanel;
    }
}