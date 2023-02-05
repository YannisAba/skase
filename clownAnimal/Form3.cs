using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clownAnimal
{
    public partial class Form3 : Form
    {
        SQLiteConnection connection1, connection2;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox2.Clear();
            connection1 = new SQLiteConnection("Data source= database1.db;Version=3");
            connection2 = new SQLiteConnection("Data source= database2.db;Version=3");
            connection1.Open();
            connection2.Open();
            //if table doesnt exist ill create it and it will display nothing
            String createSQL1 = "create table if not exists EasyScores(id integer primary key autoincrement, Name varchar(100), EasyScore  integer);";
            String createSQL2 = "create table if not exists HardScores(id integer primary key autoincrement, Name varchar(100), HardScore  integer);";
            SQLiteCommand command1 = new SQLiteCommand(createSQL1, connection1);
            SQLiteCommand command2 = new SQLiteCommand(createSQL2, connection2);
            command1.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            String selectSQL1 = "Select * from EasyScores";
            String selectSQL2 = "Select * from HardScores";
            SQLiteCommand command11 = new SQLiteCommand(selectSQL1, connection1);
            SQLiteCommand command22 = new SQLiteCommand(selectSQL2, connection2);
            SQLiteDataReader reader1 = command11.ExecuteReader();
            while (reader1.Read())
            {
                richTextBox1.AppendText("•Name: ");
                richTextBox1.AppendText(reader1.GetString(1));
                richTextBox1.AppendText(" •Score: ");
                richTextBox1.AppendText(reader1.GetInt32(2).ToString());
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText(Environment.NewLine);
            }
            SQLiteDataReader reader2 = command22.ExecuteReader();
            while (reader2.Read())
            {
                richTextBox2.AppendText("•Name: ");
                richTextBox2.AppendText(reader2.GetString(1));
                richTextBox2.AppendText(" •Score: ");
                richTextBox2.AppendText(reader2.GetInt32(2).ToString());
                richTextBox2.AppendText(Environment.NewLine);
                richTextBox2.AppendText(Environment.NewLine);
            }
            connection1.Close();
            connection2.Close();
            
        }
    }
}
