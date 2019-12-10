using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            string surname = this.textBox2.Text;
            string post = this.textBox3.Text;
            int date = int.Parse(this.textBox4.Text);
            double salary = double.Parse(this.textBox5.Text);

            SqlConnection sqlConnection = new SqlConnection(Form1.connectionString);
            
            string tmp = "INSERT INTO List([Имя], [Фамилия], [Должность], [Год рождения], Зарплата) VALUES(N'"+name+"', N'"+surname+"',N'"+post+"',"+date+","+salary+")";

            SqlCommand sqlCommand = new SqlCommand(tmp, sqlConnection);
            try
            {
                sqlConnection.Open();

                int n = sqlCommand.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.textBox4.Clear();
            this.textBox5.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 ob = new Form1();
            ob.Hide();
            ob.Show();
        }

        
    }
}
