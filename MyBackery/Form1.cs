using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SQLite;


namespace MyBackery
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            update_SQL();
        }

        private int get_product()
        {
            int id = 1;

            using (var connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT id
                    FROM product
                    WHERE name = $name
                ";

                command.Parameters.AddWithValue("name", listBox1.SelectedItem.ToString());
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
            }

            return id;
        }

        private void update_SQL()
        {
            using (var connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();

                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT name, category, price
                    FROM product
                    WHERE name like $name
                    OR category like $cat
                ";

                command.Parameters.AddWithValue("$name", "%" + textBox1.Text + "%");
                command.Parameters.AddWithValue("$cat", "%" + textBox1.Text + "%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader["name"].ToString());
                        listBox2.Items.Add(reader["price"].ToString());
                        listBox3.Items.Add(reader["category"].ToString());
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE 'Product' (
                        'id'    INTEGER NOT NULL UNIQUE,
                        'name'  TEXT,
                        'price'   INTEGER,
	                    'category'  TEXT,
	                    PRIMARY KEY('id' AUTOINCREMENT)
                    );
                    CREATE TABLE 'Check' (
                        'id'    INTEGER NOT NULL UNIQUE,
                        'user'  TEXT,
	                    'product'  INTEGER,
	                    PRIMARY KEY('id' AUTOINCREMENT)
                    );
                ";

                command.ExecuteNonQuery();

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            update_SQL();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT INTO 'Check'('user', 'product')
                VALUES($user, $prod)
                ";

                command.Parameters.AddWithValue("user", textBox2.Text);
                command.Parameters.AddWithValue("prod", get_product());
                for (int i = 0; i < Int32.Parse(textBox3.Text); i++)
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
        }
    }
}
