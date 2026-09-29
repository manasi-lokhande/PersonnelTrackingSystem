using PersonnelTracking.BLL;
using PersonnelTracking.DAL.DAO;
using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PersonnelTracking.DAL;


namespace PersonnelTracking.UI
{
    public partial class TaskForm : Form
    {
        TaskDTO dto = new TaskDTO();
        public bool comboFull;
        Task task = new Task();
        public bool isUpdate = false;
        public TaskDetailDTO detail = new TaskDetailDTO();
        public TaskForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }             
        private void TaskForm_Load(object sender, EventArgs e)
        {
                label6.Visible = true;
                cmbState.Visible = true;
                dto = TaskBLL.GetAll();
                dataGridView1.DataSource = dto.Employees;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "User No";
                dataGridView1.Columns[2].HeaderText = "Name";
                dataGridView1.Columns[3].HeaderText = "Surname";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                comboFull = false;
                cmbDepartment.DataSource = dto.departments;
                cmbDepartment.DisplayMember = "DepartmentName";
                cmbDepartment.ValueMember = "DepartmentID";
                cmbPosition.DataSource = dto.Positions;
                cmbPosition.DisplayMember = "PositionName";
                cmbPosition.ValueMember = "PositionID";
                cmbDepartment.SelectedIndex = -1;
                cmbPosition.SelectedIndex = -1;
                comboFull = true;

                cmbState.DataSource = dto.taskStates;
                cmbState.DisplayMember = "StateName";
                cmbState.ValueMember = "TaskStateID";
                cmbDepartment.SelectedIndex = -1;
            
            if (isUpdate)
            {
                label6.Visible = true;
                cmbState.Visible = true;                             
                txtUserNo.Text = detail.UserNo.ToString();
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtTitle.Text = detail.TaskTitle;
                txtContent.Text = detail.TaskContent;
                cmbState.DataSource = dto.taskStates;
                cmbState.DisplayMember = "StateName";
                cmbState.ValueMember = "TaskStateID";
                cmbState.SelectedValue = detail.TaskStateID;
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                cmbPosition.DataSource = dto.Positions
                    .Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue))
                    .ToList();
                List<EmployeeDetailsDTO> list = dto.Employees;
                dataGridView1.DataSource = list.Where(x => x.DepartmentID ==
                Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtUserNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            task.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<EmployeeDetailsDTO> list = dto.Employees;
                dataGridView1.DataSource = list.Where(x => x.PositionID ==
                Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (task.EmployeeID == 0)
                MessageBox.Show("please select employee from employee table");
            else if (txtTitle.Text.Trim() == "")
                MessageBox.Show("Title is Empty");
            else if (txtContent.Text.Trim() == "")
                MessageBox.Show("Content is Empty");
            else
            {
                if (!isUpdate)
                {
                    task.TaskTitle = txtTitle.Text;
                    task.TaskContent = txtContent.Text;
                    task.TaskStartDate = DateTime.Today;
                    task.TaskStateID = 1;
                    TaskBLL.AddTask(task);
                    MessageBox.Show("Task Added");
                    txtTitle.Clear();
                    txtContent.Clear();
                    task = new Task();
                }
                else if(isUpdate)
                {
                    DialogResult result = MessageBox.Show("Are You Sure", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Task update = new Task();
                        update.TaskID = detail.TaskID;
                        if (Convert.ToInt32(txtUserNo.Text) != detail.UserNo)
                            update.EmployeeID = task.EmployeeID;
                        else 
                        {
                            update.EmployeeID = detail.EmployeeId;
                            update.TaskTitle = txtTitle.Text;
                            update.TaskContent = txtContent.Text;
                            update.TaskStateID = Convert.ToInt32(cmbState.SelectedValue);
                            TaskBLL.updateTask(update);
                            MessageBox.Show("Task Updated");
                        }
                    }
                }
            }


        }
    }
}
