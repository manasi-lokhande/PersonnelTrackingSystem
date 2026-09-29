using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DAO
{
    public class DepartmentDAO:EmployeeContext
    {
        public static void AddDepartment(Department dep)
        {
            db.Departments.InsertOnSubmit(dep);
            db.SubmitChanges();
        }

        public static void deleteDepartment(int departmentID)
        {
            try
            {
                Department dep = db.Departments.First(x => x.DepartmentID == departmentID);
                db.Departments.DeleteOnSubmit(dep);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }

        public static void updateDepartment(Department dep)
        {
            try
            {
                Department department = db.Departments.First(x => x.DepartmentID == dep.DepartmentID);
                department.DepartmentName = dep.DepartmentName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
