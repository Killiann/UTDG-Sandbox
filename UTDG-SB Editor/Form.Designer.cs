namespace UTDG_SB_Editor
{
    partial class Main
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
            this.test_width = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.test_height = new System.Windows.Forms.TrackBar();
            this.MG_window = new UTDG_SB_Editor.MG_Main();
            ((System.ComponentModel.ISupportInitialize)(this.test_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.test_height)).BeginInit();
            this.SuspendLayout();
            // 
            // test_width
            // 
            this.test_width.Location = new System.Drawing.Point(12, 30);
            this.test_width.Name = "test_width";
            this.test_width.Size = new System.Drawing.Size(181, 45);
            this.test_width.TabIndex = 1;
            this.test_width.Scroll += new System.EventHandler(this.test_width_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Width";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Height";
            // 
            // test_height
            // 
            this.test_height.Location = new System.Drawing.Point(12, 91);
            this.test_height.Name = "test_height";
            this.test_height.Size = new System.Drawing.Size(181, 45);
            this.test_height.TabIndex = 3;
            this.test_height.Scroll += new System.EventHandler(this.test_height_Scroll);
            // 
            // MG_window
            // 
            this.MG_window.Location = new System.Drawing.Point(208, 2);
            this.MG_window.MouseHoverUpdatesOnly = false;
            this.MG_window.Name = "MG_window";
            this.MG_window.Size = new System.Drawing.Size(589, 446);
            this.MG_window.TabIndex = 0;
            this.MG_window.Text = "mG_Main1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.test_height);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.test_width);
            this.Controls.Add(this.MG_window);
            this.Name = "Main";
            this.Text = "UTDG-SB Editor";
            ((System.ComponentModel.ISupportInitialize)(this.test_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.test_height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MG_Main MG_window;
        private System.Windows.Forms.TrackBar test_width;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar test_height;
    }
}

