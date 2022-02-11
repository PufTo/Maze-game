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

namespace Maze_game
{
    public partial class First_Page : Form
    {
        public First_Page()
        {
            InitializeComponent();
        }

        string username;
        public int n = 10, m, lsir, nivel = 0, dimensiune;
        int X, Y;
        int timp = 0;
        public struct JUCATOR
        {
            public int scor;
            public string nume;
        }
        JUCATOR[] clasament = new JUCATOR[11];
        int nr_jucatori = 0;
        public struct loc//descrie o pozitie in labirint
        {
            public int x, y;
        };
        loc[] sir = new loc[10000];//sirul folosit pentru generarea labirintului
        Random rnd = new Random();
        int[] dl = new int[4] { -1, 0, 1, 0 };
        int[] dc = new int[4] { 0, 1, 0, -1 };
        int[,] labirint = new int[100, 100];//matricea care memoreaza labirintul
        int[,] viz = new int[100, 100];//matrice auxiliara
        int linie_cavaler, coloana_cavaler, linie_magie, coloana_magie;

        bool verifica(int x,int y)//verifica daca miscarea e posibila
        {
            return (in_matrice(x, y) && labirint[x,y] != -1);
        }
        bool victorie()//verifica daca jucatorul a ajuns la magie
        {
            return linie_cavaler == linie_magie && coloana_cavaler == coloana_magie;
        }
        void update_leaderboard()//face update la fisierul leaderboard
        {
            int pos;
            JUCATOR aux;
            pos = nr_jucatori - 1;
            clasament[pos].scor = timp;
            while(pos >= 1 && clasament[pos].scor > clasament[pos - 1].scor)
            {
                aux = clasament[pos];
                clasament[pos] = clasament[pos - 1];
                clasament[pos - 1] = aux;
                pos--;
            }
            Console.WriteLine(pos);
            if (pos <= 9)
            {
                MessageBox.Show("Congratulations, " + username + ", you found all the magic fragments!" + Environment.NewLine + "You finished with " + Convert.ToString(timp) + " seconds left, which placed you in top 10, on position " + Convert.ToString(pos+1));
                using (StreamWriter file = new StreamWriter(Path.GetFullPath("leaderboard.txt")))
                {
                    int i = 0;
                    while (i < nr_jucatori && i <= 9)
                    {
                        file.Write(clasament[i].nume);
                        file.Write(" ");
                        file.WriteLine(Convert.ToString(clasament[i].scor));
                        i++;
                    }
                }
            }
            else MessageBox.Show("Congratulations, " + username + ", you found all the magic fragments!" + Environment.NewLine + "You finished with " + Convert.ToString(timp) + " seconds left, which is not good enough to be in top 10. Try again!");
        }
        void move(int k)//deplaseaza cavalerul
        {
            if (verifica(linie_cavaler + dl[k], coloana_cavaler + dc[k]))
            {
                Graphics g = CreateGraphics();
                g.DrawImage(Properties.Resources.dirt, X + (coloana_cavaler - 1) * dimensiune,Y + (linie_cavaler - 1) * dimensiune, dimensiune, dimensiune);
                labirint[linie_cavaler, coloana_cavaler] = 0;
                linie_cavaler = linie_cavaler + dl[k];
                coloana_cavaler = coloana_cavaler + dc[k];
                labirint[linie_cavaler, coloana_cavaler] = 1;
                g.DrawImage(Properties.Resources.k2, X + (coloana_cavaler - 1) * dimensiune, Y + (linie_cavaler - 1) * dimensiune, dimensiune, dimensiune);
                if(victorie())
                {
                    timer1.Enabled = false;
                    if (nivel <= 9)
                    {
                        MessageBox.Show("Congratulations, you found a magic fragment!" + Environment.NewLine + "You only need " + Convert.ToString(10-nivel) + " more.");
                        start();
                    }
                    else
                    {
                        update_leaderboard();
                        revenire_meniu();
                    }
                }
            }
        }
        private void sterge(int x)//sterge un element din lista de noduri
        {
            int i;
            for (i = x + 1; i < lsir; i++)
                sir[i - 1] = sir[i];
            lsir--;
        }
        public void revenire_meniu()
        {
            Menu f = new Menu();
            f.Show();
            this.Close();
        }

        public bool in_matrice(int x, int y)//verifica daca un element e in matrice
        {
            return ((1 <= x && x <= n) && (1 <= y && y <= m));
        }

        public void add(int x, int y)//adauga toti vecinii nevizitati ai nodului curent
        {
            int nx, ny, k;
            for (k = 0; k < 4; k++)
            {
                nx = x + 2 * dl[k];
                ny = y + 2 * dc[k];
                if (in_matrice(nx, ny) && labirint[nx, ny] == -1 && viz[nx, ny] == 0)
                {
                    viz[nx, ny] = 1;
                    sir[lsir].x = nx;
                    sir[lsir].y = ny;
                    lsir++;
                }
            }
        }

        public void del(int x, int y)//stergerea unei muchii adica deschiderea unui drum 
        {
            int nx, ny, k, nr, pos;
            loc[] lista = new loc[5];
            nr = 0;
            for (k = 0; k < 4; k++)
            {
                nx = x + 2 * dl[k];
                ny = y + 2 * dc[k];
                if (in_matrice(nx, ny) && labirint[nx, ny] == 0)
                {
                    lista[nr].x = x + dl[k];
                    lista[nr].y = y + dc[k];
                    nr++;
                }
            }
            pos = rnd.Next(nr);
            labirint[lista[pos].x, lista[pos].y] = 0;
        }
        void lee(int l, int c)//algoritmul lui lee
        {
            int i, j, p, u, k, nl, nc;
            loc[] coada = new loc[10000];
            loc aux;
            for (i = 0; i <= n + 1; i++)
                for (j = 0; j <= m + 1; j++)
                    viz[i, j] = labirint[i, j];
            p = u = 0;
            coada[0].x = l;
            coada[0].y = c;
            while (p <= u)
            {
                aux = coada[p++];
                for (k = 0; k < 4; k++)
                {
                    nl = aux.x + dl[k];
                    nc = aux.y + dc[k];
                    if (in_matrice(nl, nc) && viz[nl, nc] == 0)
                    {
                        u++;
                        coada[u].x = nl;
                        coada[u].y = nc;
                        viz[nl, nc] = viz[aux.x, aux.y] + 1;
                    }
                }
            }
            viz[l, c] = 0;
        }
        void generare_labirint()//genereaza labirintul
        {
            int i, j, nr, max, ls, cs, ok;//ls-linie start, cs-coloana start
            loc[] lista = new loc[1000];//lista pentru pozitiile posibile ale magiei
            for (i = 0; i <= n + 1; i++)
                for (j = 0; j < m + 1; j++)
                {
                    labirint[i, j] = -1;
                    viz[i, j] = 0;
                }
            int pos;
            ok = rnd.Next(2);
            if (ok == 0)
                ls = 1;
            else
                ls = n;
            ok = rnd.Next(2);
            if (ok == 0)
                cs = 1;
            else
                cs = m;
            labirint[ls, cs] = 0;
            viz[ls, cs] = 1;
            lsir = 0;
            add(ls, cs);
            while (lsir > 0)
            {
                pos = rnd.Next(lsir) % lsir;
                del(sir[pos].x, sir[pos].y);
                add(sir[pos].x, sir[pos].y);
                labirint[sir[pos].x, sir[pos].y] = 0;
                sterge(pos);
            }
            ls = rnd.Next(n) + 1;
            cs = rnd.Next(m) + 1;
            while(labirint[ls,cs] == -1)
            {
                ls = rnd.Next(n) + 1;
                cs = rnd.Next(m) + 1;
            }
            lee(ls, cs);
            max = 0;
            for (i = 1; i <= n; i++)
                for (j = 1; j <= m; j++)
                    if (viz[i, j] > max)
                        max = viz[i, j];
            nr = 0;
            max = max / 4 * 3;
            for (i = 1; i <= n; i++)
                for (j = 1; j <= m; j++)
                    if (viz[i, j] == max)
                    {
                        lista[nr].x = i;
                        lista[nr].y = j;
                        nr++;
                    }
            pos = rnd.Next(nr);
            linie_magie = lista[pos].x;
            coloana_magie = lista[pos].y;
            linie_cavaler = ls;
            coloana_cavaler = cs;
            labirint[linie_cavaler, coloana_cavaler] = 1;
            labirint[lista[pos].x, lista[pos].y] = 2;
        }
        void start() //incepe un nou nivel
        {
            timp += 30;
            label1.Text = Convert.ToString(timp);
            label1.Visible = true;
            nivel++;
            n += 3;
            dimensiune = this.Height / n;
            m = (this.Width) / dimensiune;
            X = (this.Width - dimensiune * m) / 2;
            Y = (this.Height - dimensiune * n) / 2;
            generare_labirint();
            Refresh();
            timer1.Enabled = true;
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    move(0);
                    break;
                case Keys.Right:
                    move(1);
                    break;
                case Keys.Down:
                    move(2);
                    break;
                case Keys.Left:
                    move(3);
                    break;
                case Keys.Escape:
                    Menu_Secondary f = new Menu_Secondary(this);
                    f.Show();
                    timer1.Enabled = false; 
                    break;
            }
        }

        private void gStart_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            if (username.Length >= 3)
            {
                bool ok = true;
                foreach (char c in username)
                {
                    if (c == ' ')
                    {
                        MessageBox.Show("Your username must be one word only, it can't contain spaces.");
                        ok = false;
                    }

                }
                if (ok)
                {
                    clasament[nr_jucatori].nume = username;
                    nr_jucatori++;
                    textBox1.Visible = false;
                    textBox1.Enabled = false;
                    gStart.Enabled = false;
                    gStart.Visible = false;
                    label2.Visible = false;
                    label2.Enabled = false;
                    label3.Visible = false;
                    label3.Enabled = false;
                    start();
                }
            }
            else
                MessageBox.Show("Your username needs to be at least 3 characters long.");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timp--;
            label1.Text = Convert.ToString(timp);
            if (timp == 0)
            {
                timer1.Enabled = false;
                MessageBox.Show("Sadly, time's out so you lost. Better luck next time!");
                revenire_meniu();
            }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            int i, j;
            Graphics g = CreateGraphics();
            for (i = 0; i <= n + 1; i++)
                for (j = 0; j <= m + 1; j++)
                {
                    if (in_matrice(i, j))
                    {
                        if (labirint[i, j] == -1)
                        {
                            g.DrawImage(Properties.Resources.wall, X + (j - 1) * dimensiune, Y + (i - 1) * dimensiune, dimensiune, dimensiune);
                        }
                        else 
                        {
                            g.DrawImage(Properties.Resources.dirt, X + (j - 1) * dimensiune, Y + (i - 1) * dimensiune, dimensiune, dimensiune);
                            if (labirint[i, j] == 1)
                                g.DrawImage(Properties.Resources.k2, X + (j - 1) * dimensiune, Y + (i - 1) * dimensiune, dimensiune, dimensiune);
                            else if (labirint[i, j] == 2)
                                g.DrawImage(Properties.Resources.magie, X + (j - 1) * dimensiune, Y + (i - 1) * dimensiune, dimensiune, dimensiune);
                        }
                    }
                    else
                        g.DrawImage(Properties.Resources.wall, X + (j - 1) * dimensiune, Y + (i - 1) * dimensiune, dimensiune, dimensiune);
                }
        }                

        private void Form2_Load(object sender, EventArgs e)
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
        }
    }
}
