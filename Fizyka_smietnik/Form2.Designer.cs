﻿namespace Fizyka_smietnik
{
    partial class SimulationOutForm
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
            this.TextArea.Size = new System.Drawing.Size(284, 461);
            this.TextArea.TabIndex = 0;
            this.TextArea.Text = "";
            // 
            // SimulationOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 461);
            this.Controls.Add(this.TextArea);
            this.Name = "SimulationOutForm";
            this.Text = "Simulation";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox TextArea;
    }
}