using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.Repository;
using BLL.Mappers;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork uow;

        public RoleService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateRole(RoleEntity roleEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(RoleEntity roleEntity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoleEntity> GetAllRoles()
        {
            return uow.RoleRepository.GetAll().Select(x => x.ToBllRole());
        }

        public RoleEntity GetRole(int id)
        {
            return uow.RoleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetUserRoles(int id)
        {
            return uow.RoleRepository.GetUserRoles(id).Select(x => x.ToBllRole());
        }

        public void UpdateRole(RoleEntity roleEntity)
        {
            throw new NotImplementedException();
        }
    }
}
