using Microsoft.Data.Sqlite;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace forms_database
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            RefreshTabella();

        }

        private void RefreshTabella()
        {
            var conn = CreateConnection();

            DataTable dt = new DataTable();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Users";

            SqliteDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }

        private static void AggiungiUtente(string nome, int anni)
        {
            SqliteConnection conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Users (Name,Age) VALUES (@name,@age)";
            cmd.Parameters.AddWithValue("@name", nome);
            cmd.Parameters.AddWithValue("@age", anni);
            cmd.ExecuteNonQuery();
        }

        private static void CreaTabellaUsers()
        {
            SqliteConnection conn = CreateConnection();
            SqliteCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Users(
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT,
                                Age INTEGER);";
            cmd.ExecuteNonQuery();
        }

        private static SqliteConnection CreateConnection()
        {
            string connectionString = "Data Source=mydatabase.db";
            SqliteConnection conn = new SqliteConnection(connectionString);
            conn.Open();
            return conn;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AggiungiUtente(textBox1.Text, (int)numericUpDown1.Value);
            RefreshTabella();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SvuotaTabella();
            RefreshTabella();
        }
        private static void SvuotaTabella()
        {
            SqliteConnection conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Users";

            cmd.ExecuteNonQuery();
        }
    
    }
}


