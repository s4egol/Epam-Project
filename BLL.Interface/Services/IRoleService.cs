using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IRoleService
    {
        RoleEntity GetRole(int id);
        IEnumerable<RoleEntity> GetAllRoles();
        void CreateRole(RoleEntity roleEntity);
        void DeleteRole(RoleEntity roleEntity);
        void UpdateRole(RoleEntity roleEntity);
        IEnumerable<RoleEntity> GetUserRoles(int id);
    }
}
