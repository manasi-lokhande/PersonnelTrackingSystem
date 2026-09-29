using PersonnnelTrackingSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PersonnelTracking.BLL;
using PersonnelTracking.DAL;

namespace PersonnelTracking.UI
{
    public partial class DepartmentForm : Form
    {
        Department detail = new Department();
        public DepartmentForm()
        {
            InitializeComponent();
        }
        List<Department> list = new List<Department>();

        public void ShowData()
        {
            list = DepartmentBLL.GetDepartments();
            dgDepartment.DataSource = list;
            dgDepartment.Columns[0].HeaderText = "Department ID";
            dgDepartment.Columns[1].HeaderText = "Department Name";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            DepartmentAdd frm= new DepartmentAdd();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            list = DepartmentBLL.GetDepartments();
            dgDepartment.DataSource = list;
        }

        private void DepartmentForm_Load(object sender, EventArgs e)
        {
            ShowData();
        }

        private void dgDepartment_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.DepartmentID = Convert.ToInt32(dgDepartment.Rows[e.RowIndex].Cells[0].Value);
            detail.DepartmentName = dgDepartment.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.DepartmentID == 0)
                MessageBox.Show("Please select Department from table");
            else
            {
                DepartmentAdd frm = new DepartmentAdd();
                frm.isUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                list = DepartmentBLL.GetDepartments();
                dgDepartment.DataSource = list;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                DepartmentBLL.deleteDepartment(detail.DepartmentID);
                MessageBox.Show("department deleted");
                list = DepartmentBLL.GetDepartments();
                dgDepartment.DataSource = list;
            }
        }
    }
}
