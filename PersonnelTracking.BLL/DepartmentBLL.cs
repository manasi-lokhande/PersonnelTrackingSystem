using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelTracking.DAL;
using PersonnelTracking.DAL.DAO;

namespace PersonnelTracking.BLL
{
    public class DepartmentBLL
    {
        public static void AddDepartment(Department dep)
        {
            DepartmentDAO.AddDepartment(dep);
        }

        public static void deleteDepartment(int departmentID)
        {
            DepartmentDAO.deleteDepartment(departmentID);
        }

        public static List<Department> GetDepartments()
        {
            return DepartmentDAO.GetDepartments();
        }

        public static void UpdateDepartment(Department dep)
        {
            DepartmentDAO.updateDepartment(dep);
        }
    }
}
