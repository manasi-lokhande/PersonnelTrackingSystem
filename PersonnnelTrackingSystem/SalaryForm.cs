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
using PersonnelTracking.DAL.DTO;
using PersonnelTracking.DAL;

namespace PersonnelTracking.UI
{
    public partial class SalaryForm : Form
    {
        SalaryDTO dto = new SalaryDTO();
        public bool comboFull = false;
        Salary salary = new Salary();
        public SalaryDetailDTO details = new SalaryDetailDTO();
        public bool isUpdate = false;
        public SalaryForm()
        {
            InitializeComponent();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        
        private void SalaryForm_Load(object sender, EventArgs e)
        {
            
            dto = SalaryBLL.GetAll();
            dataGridView1.DataSource = dto.Employee;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "UserNo";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].HeaderText = "Salary";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            comboFull = false;
            cmbDepartment.DataSource = dto.Departments;
            cmbPosition.DataSource = dto.Positions;
            cmbMonths.DataSource = dto.SalaryMonths;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "PositionID";
            cmbMonths.DisplayMember = "MonthName";
            cmbMonths.ValueMember = "MonthID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            cmbMonths.SelectedIndex = -1;
            comboFull = true;

            if (isUpdate)
            {
                txtUserNo.Text = details.UserNo.ToString();
                txtName.Text = details.Name;
                txtSurname.Text = details.Surname;
                txtSalary.Text = details.Salary.ToString();
                txtYear.Text = details.Year.ToString();
                cmbMonths.DataSource = dto.SalaryMonths;
                cmbMonths.DisplayMember = "MonthName";
                cmbMonths.ValueMember = "MonthID";
                cmbMonths.SelectedValue = details.MonthID;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtUserNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSalary.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            salary.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = dto.Positions.
                    Where(x => x.DepartmentID == 
                    Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
                List<EmployeeDetailsDTO> list = dto.Employee;
                dataGridView1.DataSource = list.Where(
                    x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)
                    ).ToList();

                
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<EmployeeDetailsDTO> list = dto.Employee;
                dataGridView1.DataSource = list.Where(
                    x => x.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)
                    ).ToList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (salary.EmployeeID == 0)
                MessageBox.Show("Please select Employee From table");
            else if (txtSalary.Text == "")
                MessageBox.Show("Salary is Empty");
            else if (txtYear.Text == "")
                MessageBox.Show("Year is Empty");
            else if (cmbMonths.SelectedIndex == -1)
                MessageBox.Show("select month");
            else
            {
                if (!isUpdate)
                {
                    salary.Amount = Convert.ToInt32(txtSalary.Text);
                    salary.Year = Convert.ToInt32(txtYear.Text);
                    salary.MonthID = Convert.ToInt32(cmbMonths.SelectedValue);
                    SalaryBLL.AddSalary(salary);
                    MessageBox.Show("Salary Added");
                    txtSalary.Clear();
                    txtYear.Clear();
                    cmbMonths.SelectedIndex = -1;
                }
                else if (isUpdate)
                {
                    DialogResult result = MessageBox.Show("Are you sure??", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Salary update = new Salary();
                        update.SalaryID = details.SalaryID;
                        if (Convert.ToInt32(txtUserNo.Text) != details.UserNo)
                            salary.EmployeeID = salary.SalaryID;
                        else
                        {
                            update.EmployeeID = details.EmployeeID;
                            update.Amount = Convert.ToInt32(txtSalary.Text);
                            update.Year = Convert.ToInt32(txtYear.Text);
                            update.MonthID = Convert.ToInt32(cmbMonths.SelectedValue);
                            SalaryBLL.updateSalary(update);
                            MessageBox.Show("Salary Updated");
                        }
                    }
                        

                }
            }
        }
    }
}
