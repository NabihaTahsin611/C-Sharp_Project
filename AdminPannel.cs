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

namespace TrioDairyFarm
{
    public partial class AdminPannel : Form
    {
        private DataAccess Da { get; set; }
        public AdminPannel()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Da = new DataAccess();

            this.CowGridView();

        }

        private void CowGridView(string sql = "select * from products;")
        {
            var ds = this.Da.ExecuteQuery(sql);

            this.Cow.AutoGenerateColumns = false;
            this.Cow.DataSource = ds.Tables[0];
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginAs a = new LoginAs ();
            a.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Finance a = new Finance();
            a.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtid.Text = this.Cow.CurrentRow.Cells[0].Value.ToString();
            this.cmbtype.Text = this.Cow.CurrentRow.Cells[1].Value.ToString();
            this.txtQuantity.Text = this.Cow.CurrentRow.Cells[2].Value.ToString();
            this.txtprice.Text = this.Cow.CurrentRow.Cells[3].Value.ToString();
            
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToSave())
                {
                    MessageBox.Show("Please fill all the information");
                    return;
                }

                var query = "select * from products where ProductID = '" + this.txtid.Text + "';";
                var ds = this.Da.ExecuteQuery(query);

                /*if (ds.Tables[0].Rows.Count == 1)
                {
                    //update
                    var sql = @"update product1
                                set Name = '" + this.cowname.Text + @"',
                                Weight = " + this.cowweight.Text + @"
                                where Id = '" + this.cowId.Text + "'; ";
                    int count = this.Da.ExecuteDMLQuery(sql);

                    if (count == 1)
                        MessageBox.Show("Data Updated");
                    else
                        MessageBox.Show("Data Upgradation Failure");
                }*/
                //else
                //{
                    // insert
                    var sql = "insert into Products values(" + this.txtid.Text + ", '" + this.cmbtype.Text + "', '" + this.txtQuantity.Text + "'," + this.txtprice.Text + ");";
                    int count = this.Da.ExecuteDMLQuery(sql);

                    if (count == 1)
                        MessageBox.Show("Data Added");
                    else
                        MessageBox.Show("Data Addition Failure");
                //}

                this.CowGridView();
                this.ClearAll();
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is an error in your input: " + exc.Message);
            }

        }

        private bool IsValidToSave()
        {
            if (String.IsNullOrEmpty(this.txtid.Text) || String.IsNullOrEmpty(this.cmbtype.Text) ||
                String.IsNullOrEmpty(this.txtQuantity.Text) || String.IsNullOrEmpty(this.txtprice.Text))
                return false;
            else
                return true;
        }

        private void ClearAll()
        {
            this.txtid.Clear();
            this.cmbtype.SelectedIndex = -1;
            this.txtQuantity.Clear();
            this.txtprice.Clear();
            /* this.txtIncome.Clear();
             this.dtpReleaseDate.Text = "";
             this.cmbGenre.SelectedIndex = -1;

             this.txtSearch.Clear();
             this.txtAutoSearch.Clear();

             this.dgvMovie.ClearSelection();
             this.AutoIdGenerate();*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Cow.SelectedRows.Count < 1)
                {
                    MessageBox.Show("Please select a row first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    return;

                var id = this.Cow.CurrentRow.Cells[0].Value.ToString();
                //var Name = this.Cow.CurrentRow.Cells[1].Value.ToString();
                //MessageBox.Show(id);
                var sql = "delete from products where ProductID = '" + id + "';";
                int count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                    MessageBox.Show("Product ID"+ id.ToUpper() + " has been removed from the product list");
                else
                    MessageBox.Show("Data Deletion Failure");

                this.CowGridView();
                this.ClearAll();
            }
            catch (Exception exc)
            {
                MessageBox.Show("There is an error in your input: " + exc.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var query = "select * from products where ProductID = " + this.txtid.Text + ";";
            var ds = this.Da.ExecuteQuery(query);

            if (ds.Tables[0].Rows.Count == 1)
            {
                //update
                var sql = @"update products
                                Type = '" + this.cmbtype.Text + @"',
                                Quantity =' " + this.txtQuantity.Text + @"',
                                Price = " + this.txtprice.Text + @"
                                where ProductID = " + this.txtid.Text + "; ";
                int count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                    MessageBox.Show("Data Updated");
                else
                    MessageBox.Show("Data Upgradation Failure");

                this.CowGridView();
                this.ClearAll();
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginAs a = new LoginAs();
            a.Show();
            this.Hide();
        }

        private void txtid_leave(object sender, EventArgs e)
        {

        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {

        }

        private void txtprice_Leave(object sender, EventArgs e)
        {

        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AdminPannel_Load(object sender, EventArgs e)
        {

        }
    }
}
