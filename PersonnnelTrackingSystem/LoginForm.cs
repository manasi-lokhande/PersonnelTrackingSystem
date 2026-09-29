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
using PersonnnelTrackingSystem;

namespace PersonnelTracking.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "" || txtPassword.Text.Trim() == "")
                MessageBox.Show("Please fill Userno and Password");
            else
            {
                List<Employee> employeeList = EmployeeBLL.GetEmployee(Convert.ToInt32(txtUserNo.Text), txtPassword.Text);
                if (employeeList.Count > 0)
                {
                    Employee employee = new Employee();
                    employee = employeeList.First();
                    UserStatic.EmployeeID = employee.EmployeeID;
                    UserStatic.UserNo = employee.UserNo;
                    UserStatic.isAdmin =Convert.ToBoolean(employee.IsAdmin);
                    MainForm main = new MainForm();
                    this.Hide();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("User Does not Exits");
                }

            }
                
        }       
    }
}
