using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DTO
{
    public class PermissionDTO
    {    
        public List<Department> Department = new List<Department>();
        public List<PositionDTO> Position = new List<PositionDTO>();
        public List<PermissionState> PermissionState = new List<PermissionState>();
        public List<PermissionDetailsDTO> Permission = new List<PermissionDetailsDTO>();
    }
}
