using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelTracking.DAL;
using System.Net.Http.Headers;
using PersonnelTracking.DAL.DAO;

namespace PersonnelTracking.BLL
{
    public class PermissionBLL
    {
        public static void AddPermission(Permission permission)
        {
            PermissionDAO.AddPermission(permission);
        }

        public static void deletePermission(int permissionId)
        {
            PermissionDAO.deletePermission(permissionId);
        }

        public static PermissionDTO GetAll()
        {
            PermissionDTO dto = new PermissionDTO();
            dto.Department = DepartmentDAO.GetDepartments();
            dto.Position = PositionDAO.GetPosition();
            dto.PermissionState = PermissionDAO.GetPermissionState();
            dto.Permission = PermissionDAO.GetPermission();
            return dto;
        }

        public static void UpdatePermission(Permission permission)
        {
            PermissionDAO.UpdatePermission(permission);
        }

        public static void UpdatePermission(int permissionId, int approved)
        {
            PermissionDAO.UpdatePermission(permissionId, approved);
        }
    }
}
