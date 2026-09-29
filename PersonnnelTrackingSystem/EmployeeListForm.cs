using PersonnelTracking.BLL;
using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Microsoft.Office.Interop.Excel;
using PersonnnelTrackingSystem;

namespace PersonnelTracking.UI
{
    public partial class EmployeeListForm : Form
    {
        bool comboFull = false;
        EmployeeDTO dto = new EmployeeDTO();
        EmployeeDetailsDTO details = new EmployeeDetailsDTO();
        public EmployeeListForm()
        {
            InitializeComponent();
        }
        public void showDetails()
        {
            dto = EmployeeBLL.GetAll();
            dataGridView1.DataSource = dto.Employees;
        }
        public void clearFilter()
        {
            txtName.Clear();
            txtSurname.Clear();
            txtUserNo.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbDepartment.DataSource = dto.Department;
            cmbPosition.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Position;
            comboFull = true;
            dataGridView1.DataSource = dto.Employees;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            EmplyeeForm frm = new EmplyeeForm();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            showDetails();
            clearFilter();
        }
        
        private void EmployeeListForm_Load(object sender, EventArgs e)
        {
            showDetails();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User No";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "Image Path";
            dataGridView1.Columns[5].HeaderText = "Salary";
            dataGridView1.Columns[6].HeaderText = "BirthDay";
            dataGridView1.Columns[7].HeaderText = "Department";
            dataGridView1.Columns[8].HeaderText = "Position";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].HeaderText = "Address";
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            comboFull = false;
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "PositionID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;

        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = dto.Positions
                    .Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue))
                    .ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<EmployeeDetailsDTO> list = dto.Employees;
            if (txtUserNo.Text.Trim() != "")            
                list = list.Where(x => x.UserNO == Convert.ToInt32(txtUserNo.Text)).ToList();            
            if(txtName.Text.Trim()!= "")
                list = list.Where(x => x.Name.Contains(txtName.Text)).ToList().ToList();
            if (txtSurname.Text.Trim() != "")
                list = list.Where(x => x.Surname.Contains(txtSurname.Text)).ToList().ToList();
            if (cmbDepartment.SelectedIndex != -1)
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            if (cmbPosition.SelectedIndex != -1)
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFilter();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            details.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            details.UserNO = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            details.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            details.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            details.Image = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            details.Salary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            details.DepartmentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            details.PositionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            details.BirthDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            details.Address = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            details.Password = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            details.IsAdmin = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[13].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (details.EmployeeID == 0)
                MessageBox.Show("Please select Employee from table");
            else
            {
                EmplyeeForm frm = new EmplyeeForm();
                frm.isUpadte = true;
                frm.detail = details;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                showDetails();
                clearFilter();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                EmployeeBLL.deleteEmployee(details.EmployeeID);
                MessageBox.Show("Salary Deleted");
                showDetails();
                clearFilter();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportExcel(dataGridView1);
        }
    }
}
