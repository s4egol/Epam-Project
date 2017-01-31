using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using ORM;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EntityModel context;

        public RoleRepository(EntityModel context)
        {
            this.context = context;
        }

        public void Create(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Roles.ToList().Select(x => x.ToDalRole());
        }

        public DalRole GetById(int id)
        {
            return context.Roles.FirstOrDefault(x => x.Id == id)?.ToDalRole();
        }

        public IEnumerable<DalRole> GetUserRoles(int Id)
        {
            return context.UserRoles.ToList().Where(x => x.UserId == Id).Select(x => context.Roles.FirstOrDefault(role => role.Id == x.RoleId)?.ToDalRole());
        }

        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
