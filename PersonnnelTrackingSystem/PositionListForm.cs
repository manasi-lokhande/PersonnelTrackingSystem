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
    public partial class PositionListForm : Form
    {
        PositionDTO detail = new PositionDTO();
        public PositionListForm()
        {
            InitializeComponent();
        }
        List<PositionDTO> positionList = new List<PositionDTO>();

        public void showPosition()
        {
            positionList = PositionBLL.GetPosition();
            dgPositionList.DataSource = positionList;
        }
        private void PositionListForm_Load(object sender, EventArgs e)
        {
            showPosition();
            dgPositionList.Columns["Department"].Visible = false;
            dgPositionList.Columns[0].HeaderText = "Department Name";
            dgPositionList.Columns[1].Visible = false;
            dgPositionList.Columns[2].Visible = false;
            dgPositionList.Columns[3].HeaderText = "Position Name";
            dgPositionList.Columns[4].Visible = false;                       
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            PositionForm frm = new PositionForm();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            showPosition();

        }

        private void dgPositionList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.PositionID = Convert.ToInt32(dgPositionList.Rows[e.RowIndex].Cells[2].Value);
            detail.PositionName = dgPositionList.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.DepartmentID = Convert.ToInt32(dgPositionList.Rows[e.RowIndex].Cells[4].Value);
            detail.OldDepartmentID = Convert.ToInt32(dgPositionList.Rows[e.RowIndex].Cells[4].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.PositionID == 0)
                MessageBox.Show("Please select Postion from table");
            else
            {
                PositionForm frm = new PositionForm();
                frm.isUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                showPosition();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                PositionBLL.deletePosition(detail.PositionID);
                MessageBox.Show("Position Deleted");
                showPosition();
            }
        }
    }
}
