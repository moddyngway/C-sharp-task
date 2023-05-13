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

namespace MyBackery
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    SELECT Count(*) as quantity, p.name as pname, count(*) * p.price as ssum
                    FROM 'check' c
                    INNER JOIN product p
                    ON p.id = c.product
                    GROUP BY p.id
                    ORDER BY -ssum
                ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader["pname"].ToString());
                        listBox2.Items.Add(reader["quantity"].ToString());
                        listBox3.Items.Add(reader["ssum"].ToString());
                    }
                }
            }
        }
    }
}
