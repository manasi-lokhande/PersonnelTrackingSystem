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
using PersonnelTracking.DAL.DTO;

namespace PersonnelTracking.UI
{
    public partial class PermissionForm : Form
    {
        TimeSpan PermissionDay;
        Permission permission = new Permission();
        public bool isUpdate = false;
        public PermissionDetailsDTO detail = new PermissionDetailsDTO();
        public PermissionForm()
        {
            InitializeComponent();
        }        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void PermissionForm_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserStatic.UserNo.ToString();
            if (isUpdate)
            {
                dpStart.Value = detail.PermissionStartDate;
                dpEnd.Value = detail.PermissionEndDate;
                txtDaysAmount.Text = detail.DaysAmount.ToString();
                txtExplaination.Text = detail.PermissionExplaination;
                txtUserNo.Text = detail.UserNo.ToString();
            }
        }

        private void dpEnd_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dpEnd.Value.Date - dpStart.Value.Date;
            txtDaysAmount.Text = PermissionDay.TotalDays.ToString();
        }

        private void dpStart_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dpEnd.Value.Date - dpStart.Value.Date;
            txtDaysAmount.Text = PermissionDay.TotalDays.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("UserNo is Empty");
            else if(txtExplaination.Text.Trim()=="")
                MessageBox.Show("Explaination is Empty");
            else if (Convert.ToInt32(txtDaysAmount.Text) <= 0)
                MessageBox.Show("Total Days must be more than 0");
            else
            {
                if (!isUpdate)
                {
                    permission.EmployeeID = UserStatic.EmployeeID;
                    permission.PermissionStateID = 1;
                    permission.PermissionStartDate = dpStart.Value.Date;
                    permission.PermissionEndDate = dpEnd.Value.Date;
                    permission.PermissionExplanation = txtExplaination.Text;
                    permission.PermissionDay = Convert.ToInt32(txtDaysAmount.Text);
                    PermissionBLL.AddPermission(permission);
                    dpStart.Value = DateTime.Today;
                    dpEnd.Value = DateTime.Today;
                    txtDaysAmount.Clear();
                    txtExplaination.Clear();
                }
                else if (isUpdate)
                {
                    DialogResult result = MessageBox.Show("Are you sure", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        permission.PermissionID = detail.PermissionId;
                        permission.PermissionStartDate = dpStart.Value;
                        permission.PermissionEndDate = dpEnd.Value;
                        permission.PermissionExplanation = txtExplaination.Text;
                        permission.PermissionDay = Convert.ToInt32(txtDaysAmount.Text);
                        PermissionBLL.UpdatePermission(permission);
                        MessageBox.Show("Permission was Upadted");
                    }
                }
            }
        }
    }
}
