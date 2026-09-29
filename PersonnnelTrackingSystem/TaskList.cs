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
using PersonnnelTrackingSystem;

namespace PersonnelTracking.UI
{
    public partial class TaskList : Form
    {
        TaskDTO dto = new TaskDTO();
        public bool comboFull = false;
        TaskDetailDTO detail = new TaskDetailDTO();
        public TaskList()
        {
            InitializeComponent();
        }
        public void showData()
        {
            dto = TaskBLL.GetAll();
            if (!UserStatic.isAdmin)
            {
                dto.task = dto.task.Where(x => x.EmployeeId == UserStatic.EmployeeID).ToList();
            }
            dataGridView1.DataSource = dto.task;
            comboFull = false;
            cmbDepartment.DataSource = dto.departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "PositionID";
            cmbPosition.SelectedIndex = -1;
            cmbTaskState.DataSource = dto.taskStates;
            cmbTaskState.DisplayMember = "StateName";
            cmbTaskState.ValueMember = "TaskStateID";
            cmbTaskState.SelectedIndex = -1;
            comboFull = true;
        }
        public void clearFilter()
        {
            txtName.Clear();
            txtSurname.Clear();
            txtUserNo.Clear();
            comboFull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbDepartment.DataSource = dto.departments;
            cmbPosition.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            comboFull = true;
            cmbTaskState.SelectedIndex = -1;
            rbFinishDate.Checked = false;
            rbStartDate.Checked = false;
            dataGridView1.DataSource = dto.task;
            
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) 
            { 
                e.Handled = true; 
            }
        }
        

        private void TaskList_Load(object sender, EventArgs e)
        {
            showData();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Task Title";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "User No";
            dataGridView1.Columns[4].HeaderText = "Name";
            dataGridView1.Columns[5].HeaderText = "Surname";
            dataGridView1.Columns[6].HeaderText = "Department";
            dataGridView1.Columns[7].HeaderText = "Position";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;            
            dataGridView1.Columns[10].HeaderText = "Content";
            dataGridView1.Columns[11].HeaderText = "Start Date";
            dataGridView1.Columns[12].HeaderText = "Delivery Date";
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].HeaderText = "State";
            
            if (!UserStatic.isAdmin)
            {
                btnNew.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                pnlForAdmin.Hide();
                btnApprove.Text = "Delivery";
                btnApprove.Location = new Point(225, 20);
                btnClose.Location = new Point(330, 20);
            }


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
            List<TaskDetailDTO> list = dto.task;
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
            if (cmbTaskState.SelectedIndex != -1)
                list = list.Where(x => x.TaskStateID == Convert.ToInt32(cmbTaskState.SelectedValue)).ToList();
            if (rbStartDate.Checked)
                list = list.Where(x => x.TaskStartDate > Convert.ToDateTime(dtpStart.Value) &&
                x.TaskDeliveryDate < Convert.ToDateTime(dtpFinish.Value)).ToList();
            if(rbFinishDate.Checked)
                list = list.Where(x => x.TaskStartDate > Convert.ToDateTime(dtpStart.Value) &&
                x.TaskDeliveryDate < Convert.ToDateTime(dtpFinish.Value)).ToList();
            dataGridView1.DataSource = list;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFilter();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            TaskForm frm = new TaskForm();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            showData();
            clearFilter();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            detail.TaskTitle = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.TaskContent = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            detail.TaskID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.EmployeeId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            detail.TaskStateID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[13].Value);
            detail.TaskStartDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
            detail.TaskDeliveryDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[12].Value);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.TaskID == 0)
                MessageBox.Show("Please select task from table");
            else
            {
                TaskForm frm = new TaskForm();
                frm.isUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                showData();
                clearFilter();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                    TaskBLL.deleteTask(detail.TaskID);                    
                    MessageBox.Show("Permission Deleted");
                    showData();
                    clearFilter();
                
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (UserStatic.isAdmin && detail.TaskStateID == TaskStateDTO.OnEmployee && detail.EmployeeId == UserStatic.EmployeeID )            
                MessageBox.Show("Before Approve the Task Employee need to deliver the task");            
            else if(UserStatic.isAdmin && detail.TaskStateID==TaskStateDTO.Approved)            
                MessageBox.Show("This Task is already Approved");            
            else if (!UserStatic.isAdmin && detail.TaskStateID == TaskStateDTO.Delivered)            
                MessageBox.Show("This Task is already Delivered");            
            else if (!UserStatic.isAdmin && detail.TaskStateID == TaskStateDTO.Approved)            
                MessageBox.Show("This Task is already Approved");            
            else
            {
                TaskBLL.ApproveTask(detail.TaskID, UserStatic.isAdmin);
                MessageBox.Show("Task Was Updated");
                showData();
                clearFilter();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportExcel(dataGridView1);
        }
    }
}
