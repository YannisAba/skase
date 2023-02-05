using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using TextBox = System.Windows.Forms.TextBox;

namespace clownAnimal
{
    public partial class Form1 : Form
    {

        SQLiteConnection connection;
        int times;

        public int combo_play()
        {
            if (times == 5)
            {
                if (timer2.Enabled)
                {
                    label6.Visible = true;
                    label8.Visible = true;
                    label9.Visible = true;
                    pictureBox2.Visible = true;
                    times = 0;
                    return 15;
                }
                else return 0;
            }
            else
            {
                label6.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                pictureBox2.Visible = false;
                return 0;
            }
        }

        Random loc = new Random();
        Random r = new Random();
        int pokemon_x, pokemon_y;
        int counter = 0;

        int x;
        int y;

        int secondsToStart = 3;
        int secondsToEnd = 25;
        int num;
        

        public static Form1 instance;
        public TextBox txb1;

        public string Names;
        public Form1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            instance = this;
            txb1 = textBox1;
        }

        public Form1(string Name)
        {
            Names = Name;
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            x = (loc.Next(panel1.Bounds.Width - pictureBox1.Width));
            y = (loc.Next(panel1.Bounds.Height - pictureBox1.Height - 2));
            pictureBox1.Location = new Point(x, y);
            pokemon_x = r.Next(3, 10);
            pokemon_y = r.Next(3, 10);


            //Connecting database
            connection = new SQLiteConnection("Data source= database1.db;Version=3");
            connection.Open();
            //Create table if it doesnt exist, sql script with datagrip. The key is int id, and the columns are name, easyscore,  
            String createSQL = "create table if not exists EasyScores(id integer primary key autoincrement, Name varchar(100), EasyScore  integer);";
            SQLiteCommand command = new SQLiteCommand(createSQL, connection);
            command.ExecuteNonQuery();
            connection.Close();
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = "Game is over in:";
            label2.Text = secondsToEnd--.ToString();
            if (Convert.ToInt32(label2.Text) < 1)
            {
                timer2.Stop();
                timer3.Stop();
                label7.Visible = true;
                button3.Visible = true;


                connection.Open();
                String insertSQL = "Insert into EasyScores(Name, EasyScore) values(@name,@easyscore)";//enter values into table EasyScores 
                SQLiteCommand command = new SQLiteCommand(insertSQL, connection);
                command.Parameters.AddWithValue("Name", Names);//Making parameters
                command.Parameters.AddWithValue("Easy Score", num);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label2.Text) > 0 && timer2.Enabled == true)
            {
                times += 1;
                num = Convert.ToInt32(textBox1.Text) + 10 + combo_play();
                textBox1.Text = num.ToString();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //text1 = Form3.instance2.txb2.Text;
           /*num = Convert.ToInt32(text1);
            if (Convert.ToInt32(label2.Text) > 0 && timer2.Enabled == true && Convert.ToInt32(textBox1.Text) != num)
            {
                num = Convert.ToInt32(textBox1.Text) + 10;
                textBox1.Text = num.ToString();
            }*/


            pictureBox1.Location = new Point(pictureBox1.Location.X + pokemon_x, pictureBox1.Location.Y + pokemon_y);
            if (pictureBox1.Location.X < 0 || pictureBox1.Location.X + pictureBox1.Width > panel1.Width)
            {
                pokemon_x = -pokemon_x;
                counter++;
                if (counter > 4) { counter = -1; }

            }
            if (pictureBox1.Location.Y < 0 || pictureBox1.Location.Y + pictureBox1.Height + 2 > panel1.Height)
            {
                pokemon_y = -pokemon_y;
                counter++;
                if (counter > 4) { counter = -1; }

            }


            //pictureBox1.Location = new Point(x,y);
            /*int temp = r.Next(4);
            if(temp == 0)
            {
                x += 20;
            }
            else if(temp == 1)
            {
                x -= 20;
            }
            else if (temp == 2)
            {
                y -= 20;
            }
            else if (temp == 3)
            {
                y += 20;
            }
            pictureBox1.Location = new Point(x, y);*/
            //pictureBox1.Invalidate();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            times = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
