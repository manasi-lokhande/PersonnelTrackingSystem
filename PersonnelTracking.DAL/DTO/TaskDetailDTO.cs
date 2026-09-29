using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DTO
{
    public class TaskDetailDTO
    {
        public int TaskID { get; set; }
        public string TaskTitle { get; set; }
        public int EmployeeId { get; set; }
        public int  UserNo { get; set; }
        public string Name { get; set; }
        public string Surname{ get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public int DepartmentID { get; set; }
        public int PositionID { get; set; }
        public string TaskContent { get; set; }
        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskDeliveryDate { get; set; }
        public int TaskStateID { get; set; }
        public string StateName { get; set; }

    }
}
