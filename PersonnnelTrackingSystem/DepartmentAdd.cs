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
namespace PersonnnelTrackingSystem
{
    public partial class DepartmentAdd : Form
    {
        public Department detail = new Department();
        public bool isUpdate = false;
        public DepartmentAdd()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepartmentName.Text== "")
            {
                MessageBox.Show("Please Enter Department Name:");
                return;
            }
            Department dep = new Department();
            
            if(!isUpdate)
            {
                dep.DepartmentName = txtDepartmentName.Text;
                DepartmentBLL.AddDepartment(dep);
                MessageBox.Show("Department Name Added successfully");
                txtDepartmentName.Clear();
            }
            else
            {
                dep.DepartmentID = detail.DepartmentID;
                dep.DepartmentName = txtDepartmentName.Text;
                DepartmentBLL.UpdateDepartment(dep);
                MessageBox.Show("Department updated successfully");
                txtDepartmentName.Clear();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DepartmentAdd_Load(object sender, EventArgs e)
        {
            txtDepartmentName.Text = detail.DepartmentName;
        }
    }
}
