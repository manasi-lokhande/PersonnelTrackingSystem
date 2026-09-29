using PersonnelTracking.BLL;
using PersonnelTracking.DAL.DTO;
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

namespace PersonnelTracking.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            if (!UserStatic.isAdmin)
            {
                EmployeeDTO dto = EmployeeBLL.GetAll();
                EmployeeDetailsDTO detail = dto.Employees.First(x => x.EmployeeID == UserStatic.EmployeeID);
                EmplyeeForm frm = new EmplyeeForm();
                frm.detail = detail;
                frm.isUpadte = true;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
            }
            else
            {
                EmployeeListForm emp = new EmployeeListForm();
                this.Hide();
                emp.ShowDialog();
                this.Visible = true;
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(!UserStatic.isAdmin)
            {
                btnDepartment.Visible = false;
                btnposition.Visible = false;
                btnExit.Location = new Point(133, 105);
                btnLogout.Location = new Point(255, 105);
            }
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            TaskList taskList = new TaskList();
            this.Hide();
            taskList.ShowDialog();
            this.Visible = true;
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            SalaryListForm salaryfrm = new SalaryListForm();
            this.Hide();
            salaryfrm.ShowDialog();
            this.Visible = true;
        }

        private void btnPermission_Click(object sender, EventArgs e)
        {
            PermissionListForm permissionfrm = new PermissionListForm();
            this.Hide();
            permissionfrm.ShowDialog();
            this.Visible = true;
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            DepartmentForm departmentForm = new DepartmentForm();
            this.Hide();
            departmentForm.ShowDialog();
            this.Visible = true;
        }

        private void btnposition_Click(object sender, EventArgs e)
        {
            PositionListForm positionfrm = new PositionListForm();
            this.Hide();
            positionfrm.ShowDialog();
            this.Visible = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginfrm = new LoginForm();
            this.Hide();
            loginfrm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
