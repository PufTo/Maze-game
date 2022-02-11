using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Maze_game
{
    public partial class Leaderboard : Form
    {
        public Leaderboard()
        {
            InitializeComponent();
        }
        public struct JUCATOR
        {
            public int scor;
            public string nume;
        }
        JUCATOR[] clasament = new JUCATOR[11];
        int nr_jucatori;
        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                string[] lines = File.ReadAllLines(Path.GetFullPath("leaderboard.txt"));
                string[] s;
                foreach (string linie in lines)
                {
                    s = linie.Split();
                    clasament[nr_jucatori].nume = s[0];
                    clasament[nr_jucatori].scor = int.Parse(s[1]);
                    nr_jucatori++;
                }
            }
            catch
            {
                MessageBox.Show("The leaderboard file is either corupt or can't be found");
                Application.Exit();
            }
            textBox1.Text = Environment.NewLine; 
            for(int i = 0; i < nr_jucatori; i++)
            {
                textBox1.Text += (Convert.ToString(i+1)+". ");
                textBox1.Text += (clasament[i].nume + ": " + Convert.ToString(clasament[i].scor) + Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
