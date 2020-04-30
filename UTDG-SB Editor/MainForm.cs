using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTDG_SB_Editor
{
    public partial class MainForm : Form
    {
        private string currentFileName;
        AddXToAllTilemap xToTMForm;
        public MainForm()
        {
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "Select a map (.txt)",
                Filter = "Text Files (*.txt) |*.txt",
                Title = "Open Map"
            };
            saveFileDialog1 = new SaveFileDialog()
            {
                FileName = "Save Map As",
                Filter = "Text Files (*.txt) |*.txt",
                Title = "Save Map",
                DefaultExt = "txt",
                RestoreDirectory = true                
            };

            tileMapToolStripMenuItem.Checked = true;
            collisionToolStripMenuItem.Checked = false;
            depthToolStripMenuItem.Checked = false;
            saveToolStripMenuItem1.Enabled = false;

            xToTMForm = new AddXToAllTilemap(this);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void test_width_Scroll(object sender, EventArgs e)
        {
        }

        private void test_height_Scroll(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var filePath = openFileDialog1.FileName;
                    MG_window.OpenMap(filePath);
                    currentFileName = filePath;
                    saveToolStripMenuItem1.Enabled = true;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void tileMapToolStripMenuItem_Click(object sender, EventArgs e) {
            MG_window.currentLayer = MG_Main.Layers.Tilemap;
            tileSelect1.ChangeLayer(TileSelect.Layers.Tilemap);
            tileMapToolStripMenuItem.Checked = true;
            collisionToolStripMenuItem.Checked = false;
            depthToolStripMenuItem.Checked = false;
        }    
        private void collisionToolStripMenuItem_Click(object sender, EventArgs e){
            MG_window.currentLayer = MG_Main.Layers.Collision;
            tileSelect1.ChangeLayer(TileSelect.Layers.Collision);
            tileMapToolStripMenuItem.Checked = false;
            collisionToolStripMenuItem.Checked = true;
            depthToolStripMenuItem.Checked = false;
        }
        private void depthToolStripMenuItem_Click(object sender, EventArgs e){
            MG_window.currentLayer = MG_Main.Layers.Depth;
            tileSelect1.ChangeLayer(TileSelect.Layers.Depth);
            tileMapToolStripMenuItem.Checked = false;
            collisionToolStripMenuItem.Checked = false;
            depthToolStripMenuItem.Checked = true;
        }
        private void entityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MG_window.currentLayer = MG_Main.Layers.Entity;
            tileSelect1.ChangeLayer(TileSelect.Layers.Entity);
            tileMapToolStripMenuItem.Checked = false;
            collisionToolStripMenuItem.Checked = false;
            depthToolStripMenuItem.Checked = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MG_window.SaveMap(saveFileDialog1.FileName);
                    currentFileName = saveFileDialog1.FileName;
                    saveToolStripMenuItem1.Enabled = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MG_window.SaveMap(currentFileName);
        }

        private void addToAllIndexesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xToTMForm.Show();
            xToTMForm.Focus();
        }

        public void AddXToTM(int x)
        {
            MG_window.AddXToAll(x);
            xToTMForm.Hide();
            this.Focus();
        }        
    }
}
