using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clownAnimal
{
    public partial class Home : Form
    {

        int seconds = 3;
        bool b = true;
        private SoundPlayer s;

        /*Model_ASK2_CSHARPContainer context = new Model_ASK2_CSHARPContainer();
        Player p1 = new Player();*/

        public Home()
        {
            InitializeComponent();

        }

        private void InitializeSound1()
        {
            s = new SoundPlayer("scary.wav");
        }

        private void InitializeSound2()
        {
            s = new SoundPlayer("Pokemon Black⧸White Music - Pokemon Center.wav");
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Visible = false;
            label2.Visible = true;
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Visible = false;
            label3.Visible = true;
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Visible = false;
            label4.Visible = true;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Visible = false;
            label5.Visible = true;
            toolTip1.Active = true;
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            label5.Visible = false;
            label6.Visible = true;
        }

        private void label6_DoubleClick(object sender, EventArgs e)
        {
            label6.Visible = false;
            label7.Visible = true;
           // timer1.Enabled = true;
        }

       /* private void timer1_Tick(object sender, EventArgs e)
        {
            if(b == true)
            {
                InitializeSound1();
                s.Play();
                b= false;
            }

            reactionTimer.Text = seconds--.ToString();
            if (Convert.ToInt32(reactionTimer.Text) < 1)
            {
                timer1.Stop();
                seconds=1;
                label7.Visible = true;
            }
        }*/

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            label7.Visible=false;
            textBox1.Visible = true;
            toolTip1.Active = false;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Name = textBox1.Text;
                //p1.easyScore = "0";
                //p1.hardScore = "0";
                textBox1.Visible=false;
                label8.Text = "Welcome " +Name + "!";
                label8.Visible=true;
                toolTip1.Active=true;
            }
        }

        private void label8_DoubleClick(object sender, EventArgs e)
        {
            toolTip1.Active = false;
            label8.Visible=false;
            InitializeSound2();
            s.PlayLooping();
            startButton.Visible = true;
            infoButton.Visible = true;
            personalButton.Visible = true;
            leaveButton.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
        }

        private void leaveButton_Click(object sender, EventArgs e)
        {
            //context.SaveChanges();
            Application.Exit();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                s.Stop();
                this.Hide();
                Form1 f1 = new Form1(Name);
                f1.ShowDialog();
                f1 = null;
                this.Show();
               /* if (p1.hardScore == "0")
                {
                    p1.easyScore = Form1.instance.txb1.Text;
                    context.Players.Add(p1);
                }
                p1.easyScore = Form1.instance.txb1.Text;
                context.Players.Add(p1);*/
                s.Play();
            }
            else if (checkBox2.Checked)
            {
                s.Stop();
                this.Hide();
                Form2 f2 = new Form2(Name);
                f2.ShowDialog();
                f2 = null;
                this.Show();
                /*p1.hardScore = Form2.instance.txb1.Text;
                context.Players.Add(p1);
                context.SaveChanges();*/
                s.Play();
            }
            else
            {
                label9.Visible = true;
                startButton.Visible = false;
                infoButton.Visible = false;
                personalButton.Visible = false;
                leaveButton.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                toolTip2.Active = true;
                toolTip1.Active = true;
            }
        }

        private void label9_DoubleClick(object sender, EventArgs e)
        {
            label9.Visible = false;
            startButton.Visible = true;
            infoButton.Visible = true;
            personalButton.Visible = true;
            leaveButton.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            toolTip2.Active = false;
            toolTip1.Active = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            //checkBox1.Checked = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            //checkBox2.Checked = true;
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            /*var query = from c in context.Players
                        select new{ c.Name, c.easyScore , c.hardScore };
            StringBuilder builder = new StringBuilder();
            foreach (var item in query)
            {
                builder.Append(item.ToString()).Append(Environment.NewLine);
            }

            MessageBox.Show(builder.ToString());*/

            Form3 f3 = new Form3();
            f3.ShowDialog();
            this.Hide();
        }

        private void personalButton_Click(object sender, EventArgs e)
        {
           
        }

        private void Home_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label4.Parent = pictureBox1;
            /*label5.Parent = pictureBox1;
            label6.Parent = pictureBox1;
            label7.Parent = pictureBox1;
            label8.Parent = pictureBox1;
            label9.Parent = pictureBox1;*/

            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            /*label5.BackColor = Color.Transparent;
            label6.BackColor = Color.Transparent;
            label7.BackColor = Color.Transparent;
            label8.BackColor = Color.Transparent;
            label9.BackColor = Color.Transparent;*/


            /*textBox1.Parent= pictureBox1;

            textBox1.BackColor= Color.Transparent;*/


            checkBox1.Parent = pictureBox1;
            checkBox2.Parent = pictureBox1;

            checkBox1.BackColor = Color.Transparent;
            checkBox2.BackColor = Color.Transparent;


            infoButton.Parent = pictureBox1;
            startButton.Parent = pictureBox1;
            leaveButton.Parent = pictureBox1;
            personalButton.Parent = pictureBox1;

            infoButton.BackColor = Color.Transparent;
            startButton.BackColor = Color.Transparent;
            leaveButton.BackColor = Color.Transparent;
            personalButton.BackColor = Color.Transparent;
        }
    }
}
