using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DAO
{
    public class PermissionDAO : EmployeeContext
    {
        public static void AddPermission(Permission permission)
        {
            try
            {
                db.Permissions.InsertOnSubmit(permission);
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void deletePermission(int permissionId)
        {
            try
            {
                Permission pr = db.Permissions.First(x => x.PermissionID == permissionId);
                db.Permissions.DeleteOnSubmit(pr);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PermissionDetailsDTO> GetPermission()
        {
            List<PermissionDetailsDTO> permissionList = new List<PermissionDetailsDTO>();
            var list = (from pm in db.Permissions
                        join
                         ps in db.PermissionStates on
                         pm.PermissionStateID equals ps.PermissionStateID
                        join
                         e in db.Employees on pm.EmployeeID equals e.EmployeeID
                        join
                         d in db.Departments on e.DepartmentID equals d.DepartmentID
                        join p in db.Positions on e.PositionID equals p.PositionID
                        select new
                        {
                            permissionID = pm.PermissionID,
                            employeeID = pm.EmployeeID,
                            userNo = e.UserNo,
                            name = e.FirstName,
                            surname = e.LastName,
                            departmentID = e.DepartmentID,
                            positionID = e.PositionID,
                            departmentName = d.DepartmentName,
                            positionName = p.PositionName,
                            startDate = pm.PermissionStartDate,
                            endDate = pm.PermissionEndDate,
                            explaination = pm.PermissionExplanation,
                            dayAmount = pm.PermissionDay,
                            stateID = pm.PermissionStateID,
                            stateName = ps.StateName
                        }).OrderBy(x => x.permissionID).ToList();

            foreach (var item in list)
            {
                PermissionDetailsDTO dto = new PermissionDetailsDTO();
                dto.PermissionId = item.permissionID;
                dto.EmployeeID = item.employeeID;
                dto.UserNo = item.userNo;
                dto.Name = item.name;
                dto.Surname = item.surname;
                dto.DepartmentID = item.departmentID;
                dto.PositionID = item.positionID;
                dto.DepartmentName = item.departmentName;
                dto.PositionName = item.positionName;
                dto.PermissionStartDate = item.startDate;
                dto.PermissionEndDate = item.endDate;
                dto.PermissionExplaination = item.explaination;
                dto.DaysAmount = item.dayAmount;
                dto.PermissionStateId = item.stateID;
                dto.StateName = item.stateName;
                permissionList.Add(dto);
            }
            return permissionList;
        }

        public static List<PermissionState> GetPermissionState()
        {
            return db.PermissionStates.ToList();
        }

        public static void UpdatePermission(Permission permission)
        {
            try
            {
                Permission pr = db.Permissions.First(x => x.PermissionID == permission.PermissionID);
                pr.PermissionStartDate = permission.PermissionStartDate;
                pr.PermissionEndDate = permission.PermissionEndDate;
                pr.PermissionExplanation = permission.PermissionExplanation;
                pr.PermissionDay = permission.PermissionDay;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdatePermission(int permissionId, int approved)
        {
            try
            {
                Permission pr = db.Permissions.First(x => x.PermissionID == permissionId);
                pr.PermissionStateID = approved;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
