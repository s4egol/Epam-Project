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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly EntityModel context;

        public UserRoleRepository(EntityModel context)
        {
            this.context = context;
        }

        public void Create(DalUserRole entity)
        {
            context.UserRoles.Add(entity.ToOrmUserRole());
        }

        public void Delete(DalUserRole entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllByUserId(int id)
        {
            context.UserRoles.RemoveRange(context.UserRoles.Where(x => x.UserId == id));
        }

        public IEnumerable<DalUserRole> GetAll()
        {
            return context.UserRoles.ToList().Select(x => x.ToDalUserRole());
        }

        public DalUserRole GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DalUserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
