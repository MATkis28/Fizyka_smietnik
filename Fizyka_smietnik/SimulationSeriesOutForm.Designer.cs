﻿namespace Fizyka_smietnik
{
    partial class SimulationSeriesOutForm
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
            this.TextArea = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // TextArea
            // 
            this.TextArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextArea.Location = new System.Drawing.Point(0, 0);
            this.TextArea.Margin = new System.Windows.Forms.Padding(0);
            this.TextArea.Name = "TextArea";
            this.TextArea.ReadOnly = true;
            this.TextArea.Size = new System.Drawing.Size(784, 561);
            this.TextArea.TabIndex = 0;
            this.TextArea.Text = "";
            this.TextArea.WordWrap = false;
            // 
            // SimulationSeriesOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.TextArea);
            this.Name = "SimulationSeriesOutForm";
            this.ShowIcon = false;
            this.Text = "Simulation Series";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox TextArea;
    }
}