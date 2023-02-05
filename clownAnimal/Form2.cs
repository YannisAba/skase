using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clownAnimal
{
    public partial class Form2 : Form
    {

        SQLiteConnection connection;

        Random r = new Random();
        Random loc = new Random();
        int pokemon_x1, pokemon_y1;
        int pokemon_x2, pokemon_y2;
        int pokemon_x3, pokemon_y3;
        //int counter = 0;

        int x1;
        int y1;
        int x2;
        int y2;
        int x3, y3;

        int secondsToStart = 3;
        int secondsToEnd = 10;
        
        int num;

        int formx, formy;

        //private Position picPosition;

        public static Form2 instance;
        public TextBox txb1;

        public string Names;
        public Form2()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            instance = this;
            txb1 = textBox1;
        }

        public Form2(string Name)
        {
            Names = Name;
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = secondsToStart--.ToString();
            if (Convert.ToInt32(label2.Text) < 1)
            {
                timer1.Stop();
                timer2.Enabled = true;
                timer3.Enabled = true;

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible= false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            x1 = (loc.Next(panel1.Bounds.Width - pictureBox1.Width));
            y1 = (loc.Next(panel1.Bounds.Height - pictureBox1.Height - 2));
            pictureBox1.Location = new Point(x1, y1);
            pokemon_x1 = r.Next(10, 16);
            pokemon_y1 = r.Next(10, 16);

            x2 = (loc.Next(panel1.Bounds.Width - pictureBox2.Width));
            y2 = (loc.Next(panel1.Bounds.Height - pictureBox2.Height - 2));
            pictureBox2.Location = new Point(x2, y2);
            pokemon_x2 = r.Next(7, 14);
            pokemon_y2 = r.Next(6, 13);

            x3 = (loc.Next(panel1.Bounds.Width - pictureBox3.Width));
            y3 = (loc.Next(panel1.Bounds.Height - pictureBox3.Height - 2));
            pictureBox1.Location = new Point(x3, y3);
            pokemon_x3 = r.Next(10, 18);
            pokemon_y3 = r.Next(10, 18);

            this.SetDesktopLocation(loc.Next(Screen.PrimaryScreen.Bounds.Width - this.Width), loc.Next(Screen.PrimaryScreen.Bounds.Height - this.Height - 100));
            formx = r.Next(0, 5);
            formy = r.Next(0, 5);


            //Connecting database
            connection = new SQLiteConnection("Data source= database2.db;Version=3");
            connection.Open();
            //Create table if it doesnt exist, sql script with datagrip. The key is int id, and the columns are name, hardscore,  
            String createSQL = "create table if not exists HardScores(id integer primary key autoincrement, Name varchar(100), HardScore  integer);";
            SQLiteCommand command = new SQLiteCommand(createSQL, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = "Game is over in:";
            label2.Text = secondsToEnd--.ToString();
            if (Convert.ToInt32(label2.Text) < 1)
            {
                timer2.Stop();
                timer3.Stop();
                label7.Visible = true;
                button3.Visible= true;



                connection.Open();
                String insertSQL = "Insert into HardScores(Name, HardScore) values(@name,@hardscore)";//enter values into table HardScores 
                SQLiteCommand command = new SQLiteCommand(insertSQL, connection);
                command.Parameters.AddWithValue("name", Names);//Making parameters
                command.Parameters.AddWithValue("hardscore", num);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) > 0 && timer2.Enabled == true)
            {
                num = Convert.ToInt32(textBox1.Text) + 10;
                textBox1.Text = num.ToString();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X + pokemon_x1, pictureBox1.Location.Y + pokemon_y1);
            if (pictureBox1.Location.X < 0 || pictureBox1.Location.X + pictureBox1.Width > panel1.Width)
            {
                pokemon_x1 = -pokemon_x1;
                //counter++;
                //if (counter > 4) { counter = -1; }

            }
            if (pictureBox1.Location.Y < 0 || pictureBox1.Location.Y + pictureBox1.Height + 0 > panel1.Height)
            {
                pokemon_y1 = -pokemon_y1;
                //counter++;
                //if (counter > 4) { counter = -1; }

            }

            /*pictureBox2.Location = new Point(pictureBox2.Location.X + pokemon_x2, pictureBox2.Location.Y + pokemon_y2);
            if (pictureBox2.Location.X < 0 || pictureBox2.Location.X + pictureBox2.Width > panel1.Width)
            {
                pokemon_x2 = -pokemon_x2;
                //counter++;
                //if (counter > 4) { counter = -1; }

            }
            if (pictureBox2.Location.Y < 0 || pictureBox2.Location.Y + pictureBox2.Height + 2 > panel1.Height)
            {
                pokemon_y2 = -pokemon_y2;
                //counter++;
                //if (counter > 4) { counter = -1; }

            }

            pictureBox3.Location = new Point(pictureBox3.Location.X + pokemon_x3, pictureBox3.Location.Y + pokemon_y3);
            if (pictureBox3.Location.X < 0 || pictureBox3.Location.X + pictureBox3.Width > panel1.Width)
            {
                pokemon_x3 = -pokemon_x3;
                //counter++;
                //if (counter > 4) { counter = -1; }

            }
            if (pictureBox3.Location.Y < 0 || pictureBox3.Location.Y + pictureBox3.Height + 2 > panel1.Height)
            {
                pokemon_y3 = -pokemon_y3;
                //counter++;
                //if (counter > 4) { counter = -1; }

            }*/



            this.Location = new Point(this.Location.X + formx, this.Location.Y + formy);
            if (this.Location.X < 0 ||
                this.Location.X + this.Width > Screen.PrimaryScreen.Bounds.Width)
            {
                formx = -formx;
                //counter++;
                //this.BackColor = Color.FromName(colors[counter]);
                //if (counter > 4) { counter = -1; }

            }
            if (this.Location.Y < 0 ||this.Location.Y + this.Height + 10 > Screen.PrimaryScreen.Bounds.Height)
            {
                formy = -formy;
                //counter++;
                //this.BackColor = Color.FromName(colors[counter]);
                //if (counter > 4) { counter = -1; }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
