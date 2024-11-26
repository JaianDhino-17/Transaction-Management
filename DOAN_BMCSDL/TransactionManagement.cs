using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DOAN_BMCSDL
{
    public partial class TransactionManagement : Form
    {
        //private string connectionString = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=DoAn_Nhom4;Password=thedung4";
        public TransactionManagement()
        {
            InitializeComponent();
            selectAllFromTM();
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
        }

        void selectAllFromTM()
        {
            string query = "SELECT * FROM TRANSACTIONS";

            
            using (OracleDataAdapter data = new OracleDataAdapter(query, Database.conn))
            {
                DataTable dataTable = new DataTable();
                data.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccountInforForm accountInforForm = new AccountInforForm();
            accountInforForm.Show();
            this.Hide();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TopUp topUp = new TopUp();
            topUp.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            selectAllFromTM();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tranfer tranfer = new Tranfer();
            tranfer.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WithDraw withDraw = new WithDraw();
            withDraw.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DecryptForm decryptForm = new DecryptForm();
            decryptForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HybridEcryption hybridEcryption = new HybridEcryption();
            hybridEcryption.ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            HybridDecryption hybridDecryption = new HybridDecryption();
            hybridDecryption.ShowDialog();
            this.Close();
        }
    }
}
