using PersonnelTracking.BLL;
using PersonnelTracking.DAL;
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

namespace PersonnelTracking.UI
{
    public partial class PositionForm : Form
    {
        public PositionDTO detail = new PositionDTO();
        public bool isUpdate = false;
        public bool control = false;
        public PositionForm()
        {
            InitializeComponent();
        }
        List<Department> department = new List<Department>();

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text.Trim() == "")
            {
                MessageBox.Show("please fill Position Name");
            }
            else if(cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("please select Department"); ;
            }
            else
            {
                if (!isUpdate)
                {
                    Position position = new Position();
                    position.PositionName = txtPosition.Text;
                    position.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    PositionBLL.AddPosition(position);
                    MessageBox.Show("Position added successfully");
                    txtPosition.Clear();
                    cmbDepartment.SelectedItem = -1;
                }
                else
                {

                    Position position = new Position();
                    position.PositionID = detail.PositionID;
                    position.PositionName = txtPosition.Text;
                    position.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    control = false;
                    if (Convert.ToInt32(cmbDepartment.SelectedValue) != detail.OldDepartmentID)
                        control = true;
                    PositionBLL.updatePosition(position,control);
                    MessageBox.Show("Position Updated successfully");
                    txtPosition.Clear();
                    cmbDepartment.SelectedItem = -1;
                    this.Close();
                }
            }

        }

        private void PositionForm_Load(object sender, EventArgs e)
        {
            
            department = DepartmentBLL.GetDepartments();
            cmbDepartment.DataSource = department;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbDepartment.SelectedIndex = -1;
            if (isUpdate)
            {
                txtPosition.Text = detail.PositionName;
                cmbDepartment.SelectedValue = detail.DepartmentID;
            }
        }
    }
}
