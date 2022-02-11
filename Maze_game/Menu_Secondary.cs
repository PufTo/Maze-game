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
    public partial class Menu_Secondary : Form
    {
        private First_Page mainForm = null;
        public Menu_Secondary(Form callingForm)
        {
            mainForm = callingForm as First_Page;
            InitializeComponent();
        }

        private void quit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You will return to desktop, are you sure you want that?","Quit?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();
        }
        private void leaderboard_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form4"] as Leaderboard == null)
            {
                Leaderboard f = new Leaderboard();
                f.Show();
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You will return to the main menu and lose all your progress, are you sure you want that?", "Quit?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                mainForm.revenire_meniu();
                this.Close();
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

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.timer1.Enabled = true;
        }
    }
}
