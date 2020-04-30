namespace UTDG_SB_Editor
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoCtrlZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoShiftCtrlZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToAllIndexesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tileSelect1 = new UTDG_SB_Editor.TileSelect();
            this.MG_window = new UTDG_SB_Editor.MG_Main();
            this.entityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.layerToolStripMenuItem,
            this.tileSetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save As";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoCtrlZToolStripMenuItem,
            this.redoShiftCtrlZToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoCtrlZToolStripMenuItem
            // 
            this.undoCtrlZToolStripMenuItem.Name = "undoCtrlZToolStripMenuItem";
            this.undoCtrlZToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.undoCtrlZToolStripMenuItem.Text = "Undo (Ctrl, Z)";
            // 
            // redoShiftCtrlZToolStripMenuItem
            // 
            this.redoShiftCtrlZToolStripMenuItem.Name = "redoShiftCtrlZToolStripMenuItem";
            this.redoShiftCtrlZToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.redoShiftCtrlZToolStripMenuItem.Text = "Redo (Shift, Ctrl, Z)";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileMapToolStripMenuItem,
            this.collisionToolStripMenuItem,
            this.depthToolStripMenuItem,
            this.entityToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // tileMapToolStripMenuItem
            // 
            this.tileMapToolStripMenuItem.Name = "tileMapToolStripMenuItem";
            this.tileMapToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tileMapToolStripMenuItem.Text = "TileMap";
            this.tileMapToolStripMenuItem.Click += new System.EventHandler(this.tileMapToolStripMenuItem_Click);
            // 
            // collisionToolStripMenuItem
            // 
            this.collisionToolStripMenuItem.Name = "collisionToolStripMenuItem";
            this.collisionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.collisionToolStripMenuItem.Text = "Collision";
            this.collisionToolStripMenuItem.Click += new System.EventHandler(this.collisionToolStripMenuItem_Click);
            // 
            // depthToolStripMenuItem
            // 
            this.depthToolStripMenuItem.Name = "depthToolStripMenuItem";
            this.depthToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.depthToolStripMenuItem.Text = "Depth";
            this.depthToolStripMenuItem.Click += new System.EventHandler(this.depthToolStripMenuItem_Click);
            // 
            // tileSetToolStripMenuItem
            // 
            this.tileSetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToAllIndexesToolStripMenuItem});
            this.tileSetToolStripMenuItem.Name = "tileSetToolStripMenuItem";
            this.tileSetToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.tileSetToolStripMenuItem.Text = "TileSet";
            // 
            // addToAllIndexesToolStripMenuItem
            // 
            this.addToAllIndexesToolStripMenuItem.Name = "addToAllIndexesToolStripMenuItem";
            this.addToAllIndexesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addToAllIndexesToolStripMenuItem.Text = "Add to all indexes";
            this.addToAllIndexesToolStripMenuItem.Click += new System.EventHandler(this.addToAllIndexesToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tileSelect1
            // 
            this.tileSelect1.ForeColor = System.Drawing.Color.White;
            this.tileSelect1.Location = new System.Drawing.Point(9, 123);
            this.tileSelect1.MouseHoverUpdatesOnly = false;
            this.tileSelect1.Name = "tileSelect1";
            this.tileSelect1.Size = new System.Drawing.Size(192, 322);
            this.tileSelect1.TabIndex = 2;
            this.tileSelect1.Text = "tileSelect1";
            // 
            // MG_window
            // 
            this.MG_window.Location = new System.Drawing.Point(208, 31);
            this.MG_window.MouseHoverUpdatesOnly = false;
            this.MG_window.Name = "MG_window";
            this.MG_window.Size = new System.Drawing.Size(589, 417);
            this.MG_window.TabIndex = 0;
            this.MG_window.Text = "mG_Main1";
            // 
            // entityToolStripMenuItem
            // 
            this.entityToolStripMenuItem.Name = "entityToolStripMenuItem";
            this.entityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.entityToolStripMenuItem.Text = "Entity";
            this.entityToolStripMenuItem.Click += new System.EventHandler(this.entityToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tileSelect1);
            this.Controls.Add(this.MG_window);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "UTDG-SB Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MG_Main MG_window;
        private TileSelect tileSelect1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoCtrlZToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoShiftCtrlZToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem tileSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToAllIndexesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entityToolStripMenuItem;
    }
}

