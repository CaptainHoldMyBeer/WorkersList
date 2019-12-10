using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp3
{
    public  partial class Form1 : Form
    {

        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Workers;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
       
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public Form1()
        {
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button3, "Для удаления выделите Id ");

            ToolTip p = new ToolTip();
            p.SetToolTip(button5, "Пустой поиск выводит исходную таблицу");

            dataGridView1.DataSource = GetWorkers();
            

        }
        private DataTable GetWorkers()
        {
            DataTable dtWorkers = new DataTable();

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM List",sqlConnection);

            try
            {
                sqlConnection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                dtWorkers.Load(reader);
            }

            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                sqlConnection.Close();
            }

            return dtWorkers;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 ob = new Form2();
            this.Hide();
            ob.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int id = (int)dataGridView1.CurrentCell.Value;

            SqlCommand sqlCommand = new SqlCommand("DELETE FROM List WHERE Id="+id, sqlConnection);

            try
            {
                sqlConnection.Open();
                int number = sqlCommand.ExecuteNonQuery();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                sqlConnection.Close();
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = GetWorkers();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dtWorkers = new DataTable();

            if (this.textBox1.Text=="")
            {
                dataGridView1.DataSource = GetWorkers();
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM List WHERE Должность =N'" + @textBox1.Text+"'", sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                dtWorkers.Load(reader);
                sqlConnection.Close();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dtWorkers;

            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }
    }
}
