using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DTO
{
    public class PermissionDetailsDTO
    {
        public int PermissionId { get; set; }
        public int EmployeeID { get; set; }
        public int UserNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepartmentID { get; set; }
        public int PositionID { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }        
        public DateTime PermissionStartDate { get; set; }
        public DateTime PermissionEndDate { get; set; }
        public string PermissionExplaination { get; set; }
        public int? DaysAmount { get; set; }
        public int PermissionStateId { get; set; }
        public string StateName { get; set; }
    }
}
