using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStore
{
    public partial class Form1 : Form
    {
        int BookId = -1;
        public Form1()
        {
            InitializeComponent();
            SetGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string BookName = textBox1.Text;
            string AuthorName = textBox2.Text;

            SqlConnection con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            SqlCommand com = new SqlCommand("Insert into Book values (@0,@1)", con);
            com.Parameters.AddWithValue("0", BookName);
            com.Parameters.AddWithValue("1", AuthorName);

            com.ExecuteNonQuery();
            con.Close();
            SetGrid();
        }

        private void SetGrid()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            SqlCommand com = new SqlCommand("Select * from Book", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow dataRow = dataGridView1.Rows[rowIndex];

                BookId = Convert.ToInt32(dataRow.Cells[0].Value);
                string BookName = Convert.ToString(dataRow.Cells[1].Value);
                string AuthorName = Convert.ToString(dataRow.Cells[2].Value);

                textBox1.Text = BookName;
                textBox2.Text = AuthorName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            SqlCommand com = new SqlCommand("Delete from Book where BookId Like @0", con);
            com.Parameters.AddWithValue("0", BookId);

            com.ExecuteNonQuery();
            con.Close();
            SetGrid();
        }
    }
}
