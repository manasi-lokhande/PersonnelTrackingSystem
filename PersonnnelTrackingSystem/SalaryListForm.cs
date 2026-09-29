using PersonnelTracking.UI;
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
using PersonnelTracking.DAL.DTO;

namespace PersonnnelTrackingSystem
{
    public partial class SalaryListForm : Form
    {
        SalaryDTO dto = new SalaryDTO();
        public bool comboFull = false;
        SalaryDetailDTO detail = new SalaryDetailDTO();
        public SalaryListForm()
        {
            InitializeComponent();
        }
        
        public void ShowDetails()
        {
            dto = SalaryBLL.GetAll();
            if (!UserStatic.isAdmin)
                dto.Salary = dto.Salary.Where(x => x.EmployeeID == UserStatic.EmployeeID).ToList();
            dataGridView1.DataSource = dto.Salary;
            comboFull = false;
            cmbDepartment.DataSource = dto.Departments;
            cmbPosition.DataSource = dto.Positions;
            cmbMonth.DataSource = dto.SalaryMonths;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "PositionID";
            cmbMonth.DisplayMember = "MonthName";
            cmbMonth.ValueMember = "MonthID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
            comboFull = true;
        }
        public void clearFilter()
        {

            txtName.Clear();
            txtSalary.Clear();
            txtSurname.Clear();
            txtUserNo.Clear();
            txtYear.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbDepartment.DataSource = dto.Departments;
            cmbPosition.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            cmbMonth.SelectedIndex = -1;
            cmbMonth.DataSource = dto.SalaryMonths;
            comboFull = true;
            rbEqual.Checked = false;
            rbLess.Checked = false;
            rbMore.Checked = false;
            dataGridView1.DataSource = dto.Salary;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SalaryForm frm = new SalaryForm();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            ShowDetails();
            clearFilter();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SalaryListForm_Load(object sender, EventArgs e)
        {
            ShowDetails();
            dataGridView1.Columns[0].Visible = false;            
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "UserNo";
            dataGridView1.Columns[3].HeaderText = "Name";
            dataGridView1.Columns[4].HeaderText = "Surname";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Departmnet";
            dataGridView1.Columns[8].HeaderText = "Position";
            dataGridView1.Columns[9].HeaderText = "Salary";
            dataGridView1.Columns[10].HeaderText = "Year";
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].HeaderText = "Month";
            if (!UserStatic.isAdmin)
            {
                pnlForAdmin.Hide();
                btnUpdate.Hide();
                btnDelete.Hide();
                btnNew.Location = new Point(170, 17);
                btnClose.Location = new Point(280, 17);
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = dto.Positions.
                    Where(x => x.DepartmentID ==
                    Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();        
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SalaryDetailDTO> list = dto.Salary;
            if (txtUserNo.Text.Trim() != "")
                list = list.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (txtName.Text.Trim() != "")
                list = list.Where(x => x.Name == txtName.Text).ToList();
            if (txtSurname.Text.Trim() != "")
                list = list.Where(x => x.Surname == txtSurname.Text).ToList();
            if (cmbDepartment.SelectedIndex != -1)
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            if (cmbPosition.SelectedIndex != -1)
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            if (cmbMonth.SelectedIndex != -1)
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonth.SelectedValue)).ToList();
            if (txtYear.Text.Trim() != "")
                list = list.Where(x => x.Year == Convert.ToInt32(txtYear.Text)).ToList();
            if (rbMore.Checked)
                list = list.Where(x => x.Salary > Convert.ToInt32(txtSalary.Text)).ToList();
            if (rbEqual.Checked)
                list = list.Where(x => x.Salary == Convert.ToInt32(txtSalary.Text)).ToList();
            if (rbLess.Checked)
                list = list.Where(x => x.Salary < Convert.ToInt32(txtSalary.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFilter();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.SalaryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            detail.Salary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.Year = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.SalaryID == 0)
                MessageBox.Show("Please select salary from table");
            else
            {
                SalaryForm frm = new SalaryForm();
                frm.isUpdate = true;
                frm.details = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                ShowDetails();
                clearFilter();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SalaryBLL.deleteSalary(detail.SalaryID);
                MessageBox.Show("Salary Deleted");
                ShowDetails();
                clearFilter();

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportExcel(dataGridView1);
        }
    }
}
