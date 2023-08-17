namespace Rendering
{
    partial class AnimatedMap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.debugWindow = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // debugWindow
            // 
            this.debugWindow.Location = new System.Drawing.Point(1030, 12);
            this.debugWindow.Name = "debugWindow";
            this.debugWindow.Size = new System.Drawing.Size(426, 847);
            this.debugWindow.TabIndex = 0;
            this.debugWindow.Text = "";
            // 
            // AnimatedMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1468, 861);
            this.Controls.Add(this.debugWindow);
            this.DoubleBuffered = true;
            this.Name = "AnimatedMap";
            this.Text = "AnimatedMap";
            this.Load += new System.EventHandler(this.LoadEvent);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawEvent);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox debugWindow;
    }
}

