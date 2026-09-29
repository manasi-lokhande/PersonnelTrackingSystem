using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DAO
{
    public class EmployeeDAO:EmployeeContext
    {
        public static void AddEmployee(Employee employee)
        {
            try
            {
                db.Employees.InsertOnSubmit(employee);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void deleteEmployee(int employeeID)
        {
            try
            {              
                Employee emp = db.Employees.First(x => x.EmployeeID == employeeID);
                db.Employees.DeleteOnSubmit(emp);
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<EmployeeDetailsDTO> GetEmployee()
        {
            List<EmployeeDetailsDTO> employeeList = new List<EmployeeDetailsDTO>();

            var list = (from e in db.Employees
                        join
                        d in db.Departments on
                        e.DepartmentID equals d.DepartmentID
                        join
                        p in db.Positions on
                        e.PositionID equals p.PositionID
                        select new
                        {
                            employeeId = e.EmployeeID,
                            userNo = e.UserNo,
                            name = e.FirstName,
                            surname = e.LastName,
                            imagepath = e.ImagePath,
                            salary = e.Salary,
                            birthdate = e.BirthDate,
                            address = e.Address,
                            departmentId = e.DepartmentID,
                            positionId = e.PositionID,
                            departmentName = d.DepartmentName,
                            positionName = p.PositionName,
                            password = e.Password,
                            isAdmin = e.IsAdmin
                        }).OrderBy(x => x.userNo).ToList();

            foreach (var item in list)
            {
                EmployeeDetailsDTO dto = new EmployeeDetailsDTO();
                dto.EmployeeID = item.employeeId;
                dto.UserNO = item.userNo;
                dto.Name = item.name;
                dto.Surname = item.surname;
                dto.Image = item.imagepath;
                dto.Salary = item.salary;
                dto.BirthDate = item.birthdate;
                dto.Address = item.address;
                dto.DepartmentID = item.departmentId;
                dto.PositionID = item.positionId;
                dto.DepartmentName = item.departmentName;
                dto.PositionName = item.positionName;
                dto.Password = item.password;
                dto.IsAdmin = item.isAdmin;
                employeeList.Add(dto);
            }

            return employeeList;
        }

        public static List<Employee> GetEmployee(int v, string text)
        {
            try
            {
                List<Employee> list = db.Employees.Where(x => x.UserNo == v && x.Password == text).ToList();
                return list;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<Employee> GetUsers(int v)
        {
            List<Employee> list = db.Employees
                .Where(x => x.UserNo == v)
                .ToList();
            return list;
        }

        public static void updateEmployee(Employee update)
        {
            try
            {
                Employee emp = db.Employees.First(x => x.EmployeeID == update.EmployeeID);
                emp.UserNo = update.UserNo;
                emp.FirstName = update.FirstName;
                emp.LastName = update.LastName;
                emp.ImagePath = update.ImagePath;
                emp.Salary = update.Salary;
                emp.BirthDate = update.BirthDate;
                emp.Address = update.Address;
                emp.DepartmentID = update.DepartmentID;
                emp.PositionID = update.PositionID;
                emp.Password = update.Password;
                emp.IsAdmin = update.IsAdmin;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void updateEmployee(Position position)
        {
            List<Employee> list = db.Employees.Where(x => x.PositionID == position.PositionID).ToList();
            foreach (var item in list)
            {
                item.DepartmentID = position.DepartmentID;
            }
            db.SubmitChanges();
        }
    }
}
