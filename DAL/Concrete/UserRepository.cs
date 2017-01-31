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
    public class UserRepository : IUserRepository
    {
        private readonly EntityModel context;

        public UserRepository(EntityModel context)
        {
            this.context = context;
        }

        public void Create(DalUser entity)
        {
            context.Users.Add(entity.ToOrmUser());
        }

        public void Delete(DalUser entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Users.ToList().Select(x => x.ToDalUser());
        }

        public DalUser GetById(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id)?.ToDalUser();
        }

        public DalUser GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(x => x.Email == email)?.ToDalUser();
        }

        public DalUser GetUserByNickname(string nickname)
        {
            return context.Users.FirstOrDefault(x => x.Nickname == nickname)?.ToDalUser();
        }

        public void Update(DalUser entity)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == entity.Id);
            if (user != null)
            {
                user.Name = entity.Name;
                user.Surname = entity.Surname;
                if (entity.Password != String.Empty)
                    user.Password = entity.Password;
            }
        }
    }
}
