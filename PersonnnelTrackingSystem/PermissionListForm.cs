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
    public partial class PermissionListForm : Form
    {
        PermissionDTO dto = new PermissionDTO();
        public bool comboFull = false;
        PermissionDetailsDTO detail = new PermissionDetailsDTO();
        public PermissionListForm()
        {
            InitializeComponent();
        }
        public void ShowDetails()
        {
            dto = PermissionBLL.GetAll();
            if (!UserStatic.isAdmin)
                dto.Permission = dto.Permission.Where(x => x.EmployeeID == UserStatic.EmployeeID).ToList();
            dataGridView1.DataSource = dto.Permission;
            comboFull = false;
            cmbDepartment.DataSource = dto.Department;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Position;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "PositionID";
            cmbPosition.SelectedIndex = -1;
            cmbState.DataSource = dto.PermissionState;
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "PermissionStateId";
            cmbState.SelectedIndex = -1;
            comboFull = true;
        }
        public void ClearFiltter()
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtDaysAmount.Clear();
            comboFull = false;
            cmbDepartment.DataSource = dto.Department;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Position;
            cmbPosition.SelectedIndex = -1;
            cmbState.DataSource = dto.PermissionState;
            cmbState.SelectedIndex = -1;
            comboFull = true;
            rbStartDate.Checked = false;
            rbFinishDate.Checked = false;
            dataGridView1.DataSource = dto.Permission;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            PermissionForm frm = new PermissionForm();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            ShowDetails();
            ClearFiltter();

        }       
        private void PermissionListForm_Load(object sender, EventArgs e)
        {
            ShowDetails(); ;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "UserNo";
            dataGridView1.Columns[3].HeaderText = "Name";
            dataGridView1.Columns[4].HeaderText = "Surname";            
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Department";
            dataGridView1.Columns[8].HeaderText = "Position";
            dataGridView1.Columns[9].HeaderText = "Start Date";
            dataGridView1.Columns[10].HeaderText = "End Date";
            dataGridView1.Columns[11].HeaderText = "Explaination";
            dataGridView1.Columns[12].HeaderText = "Days Amount";
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].HeaderText = "State";
            if (!UserStatic.isAdmin)
            {
                pnlForAdmin.Hide();
                btnApprove.Hide();
                btnDisApprove.Hide();                
                btnDelete.Hide();
                btnClose.Location = new Point(430, 20);
            }
           
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = dto.Position.Where(
                    x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PermissionDetailsDTO> list = dto.Permission;
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
            if (cmbState.SelectedIndex != -1)
                list = list.Where(x => x.PermissionStateId == Convert.ToInt32(cmbState.SelectedValue)).ToList();
            if (rbStartDate.Checked)
                list = list.Where(x => x.PermissionStartDate < Convert.ToDateTime(dtpEnd.Value) &&
                x.PermissionStartDate > Convert.ToDateTime(dtpStart.Value)).ToList();
            if (rbFinishDate.Checked)
                list = list.Where(x => x.PermissionStartDate < Convert.ToDateTime(dtpEnd.Value) &&
                x.PermissionStartDate > Convert.ToDateTime(dtpStart.Value)).ToList();
            if (txtDaysAmount.Text.Trim() != "")
                list = list.Where(x => x.DaysAmount == Convert.ToInt32(txtDaysAmount.Text)).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFiltter();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.PermissionId = 
                Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.UserNo =
                 Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            detail.PermissionStartDate =
                Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.PermissionEndDate =
                Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detail.PermissionExplaination =
                dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            detail.DaysAmount =
                Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.PermissionStateId =
                Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[13].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {            
            if (detail.PermissionId == 0)
                MessageBox.Show("select a permission from table");            
            else if (detail.PermissionStateId == PermissionStatesDTO.Approved || detail.PermissionStateId == PermissionStatesDTO.Disapprove)
                MessageBox.Show("Can not update permission once it is approve or disapprove");
            else
            {
                PermissionForm frm = new PermissionForm();
                frm.isUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                ShowDetails();
                ClearFiltter();
            }           
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionId,PermissionStatesDTO.Approved);
            MessageBox.Show("Permission Approved");
            ShowDetails();
            ClearFiltter();
        }

        private void btnDisApprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionId, PermissionStatesDTO.Disapprove);
            MessageBox.Show("Permission Disapproved");
            ShowDetails();
            ClearFiltter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                if (detail.PermissionStateId == PermissionStatesDTO.Approved || detail.PermissionStateId == PermissionStatesDTO.Disapprove)
                    MessageBox.Show("Can not delete Permission when it is approved or Dispproved");
                else
                {
                    PermissionBLL.deletePermission(detail.PermissionId);
                    MessageBox.Show("Permission Deleted");
                    ShowDetails();
                    ClearFiltter();
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportExcel(dataGridView1);
        }
    }
}
