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
using PersonnelTracking.DAL.DAO;
using PersonnelTracking.DAL.DTO;
using System.IO;
using PersonnnelTrackingSystem;

namespace PersonnelTracking.UI
{
    public partial class EmplyeeForm : Form
    {
        EmployeeDTO dto = new EmployeeDTO();
        public bool isUpadte = false;
        public EmployeeDetailsDTO detail = new EmployeeDetailsDTO();
        String imagepath = "";
        public EmplyeeForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void clearFilter()
        {
            txtUserNo.Clear();
            txtPassword.Clear();
            chAdmin.Checked = false;
            txtName.Clear();
            txtSurname.Clear();
            txtSalary.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            dateTimePicker1.Value = DateTime.Today;
            txtAddress.Clear();
            txtImage.Clear();
            pictureBox1.Image = null;
        }
        
        private void EmplyeeForm_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "PositionID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            comboFull = true;
            if (isUpadte)
            {
                txtUserNo.Text = detail.UserNO.ToString();
                txtPassword.Text = detail.Password;
                chAdmin.Checked = Convert.ToBoolean(detail.IsAdmin);
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;                
                txtSalary.Text = detail.Salary.ToString();
                cmbDepartment.SelectedValue = detail.DepartmentID;
                cmbPosition.SelectedValue = detail.PositionID;
                dateTimePicker1.Value = Convert.ToDateTime(detail.BirthDate);
                txtAddress.Text = detail.Address;
                imagepath = Application.StartupPath + "\\images\\" + detail.Image;
                txtImage.Text = imagepath;
                pictureBox1.ImageLocation = imagepath;
                if (!UserStatic.isAdmin)
                {
                    chAdmin.Enabled = false;
                    txtUserNo.Enabled = false;
                    txtSalary.Enabled = false;
                    cmbDepartment.Enabled = false;
                    cmbPosition.Enabled = false;
                }
            }
        }
        bool comboFull = false;

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource =
                dto.Positions.Where
                (x => x.DepartmentID == departmentID).ToList();
            }
        }

        string fileName = "";

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImage.Text = openFileDialog1.FileName;
                string Unique = Guid.NewGuid().ToString();
                fileName += Unique + openFileDialog1.SafeFileName;
            }
        }

        public void Empty()
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("UserNo is Empty");
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("Password is Empty");
            else if (txtName.Text.Trim() == "")
                MessageBox.Show("Name is Empty");            
            else if (txtSurname.Text.Trim() == "")            
                MessageBox.Show("Surname is Empty");            
            else if (txtSalary.Text.Trim() == "")            
                MessageBox.Show("Salary is Empty");            
            else if (cmbDepartment.Text.Trim() == "")        
                MessageBox.Show("Department is Empty");            
            else if (cmbPosition.Text.Trim() == "")            
                MessageBox.Show("Position is Empty");            
            else if (txtImage.Text.Trim() == "")            
                MessageBox.Show("Image is Empty");           
            else if (dateTimePicker1.Text.Trim() == "")            
                MessageBox.Show("BirthDate is Empty");            
            else if (txtAddress.Text.Trim() == "")            
                MessageBox.Show("Address is Empty");            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            Empty();
            if (!isUpadte)
            {
                if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
                {
                    MessageBox.Show("User No is already used");
                }
                else
                {
                    
                    Employee employee = new Employee();                    
                    employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                    employee.Password = txtPassword.Text;
                    employee.IsAdmin = chAdmin.Checked;
                    employee.FirstName = txtName.Text;
                    employee.LastName = txtSurname.Text;
                    employee.Salary = Convert.ToInt32(txtSalary.Text);
                    employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                    employee.BirthDate = dateTimePicker1.Value;
                    employee.Address = txtAddress.Text;
                    employee.ImagePath = fileName;
                    EmployeeBLL.AddEmployee(employee);
                    File.Copy(txtImage.Text, @"images\\" + fileName);
                    MessageBox.Show("Employee Added");
                    clearFilter();                    
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Are You sure??", "warning", MessageBoxButtons.YesNo);
                if(result == DialogResult.Yes)
                {
                    Employee update = new Employee();

                    update.EmployeeID = detail.EmployeeID;
                    update.UserNo = Convert.ToInt32(txtUserNo.Text);
                    update.Password = txtPassword.Text;
                    update.IsAdmin = chAdmin.Checked;
                    update.FirstName = txtName.Text;
                    update.LastName = txtSurname.Text;
                    update.Salary = Convert.ToInt32(txtSalary.Text);
                    update.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    update.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                    update.BirthDate = dateTimePicker1.Value;
                    update.Address = txtAddress.Text;

                    if (txtImage.Text != imagepath)
                    {
                        if (File.Exists(@"images\\" + detail.Image))
                            File.Delete(@"images\\" + detail.Image);

                        File.Copy(txtImage.Text, @"images\\" + fileName);

                        update.ImagePath = fileName;
                    }
                    else
                    {
                        update.ImagePath = detail.Image;
                    }

                    EmployeeBLL.updateEmployee(update);

                    MessageBox.Show("Employee Updated");
                    clearFilter();
                }
            }


        }
        bool IsUnique = false;
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("User No is Empty");
            else
            {
                IsUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!IsUnique)
                    MessageBox.Show("User No is already used");
                else
                    MessageBox.Show("User No is available");
            }
        }
    }
}
