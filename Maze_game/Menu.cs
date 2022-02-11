using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze_game
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void start_Click(object sender, EventArgs e)
        {
            First_Page f = new First_Page();
            f.Show();
            this.Hide(); 
        }
        
        private void leaderboard_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms["Form4"] as Leaderboard == null)
            {
                Leaderboard f = new Leaderboard();
                f.Show();
            }

        }

        private void howToPlay_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form5"] as Tutorial == null)
            {
                Tutorial f = new Tutorial();
                f.Show();
            }
        }
    }
}
