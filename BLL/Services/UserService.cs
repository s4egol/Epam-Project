using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.Repository;
using BLL.Mappers;
using BLL.Interface.Entities.FullModel;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork uow;

        public UserService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateUser(FullUserEntity userEntity)
        {
            if (userEntity == null)
                return;

            var newUser = new UserEntity
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Surname = userEntity.Surname,
                NickName = userEntity.NickName,
                Email = userEntity.Email,
                Password = userEntity.Password,
                JoinTime = userEntity.JoinTime
            };

            uow.UserRepository.Create(newUser.ToDalUser());
            userEntity.Roles.Select(x => new UserRoleEntity { UserId = userEntity.Id, RoleId = x.Id }.ToDalUserRole()).ToList().ForEach(x => uow.UserRoleRepository.Create(x));
            uow.Commit();
        }

        public void CreateUser(UserEntity userEntity)
        {
            uow.UserRepository.Create(userEntity.ToDalUser());
            uow.Commit();
        }

        public void DeleteUser(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return uow.UserRepository.GetAll().Select(x => x.ToBllUser());
        }

        public FullUserEntity GetFullUserEntity(UserEntity e)
        {
            if (e == null)
                return null;

            return new FullUserEntity
            {
                Id = e.Id,
                Name = e.Name,
                Surname = e.Surname,
                NickName = e.NickName,
                Email = e.Email,
                JoinTime = e.JoinTime,
                Password = e.Password,
                Roles = uow.UserRoleRepository.GetAll().Where(x => x.UserId == e.Id).Select(x => uow.RoleRepository.GetById(x.RoleId)?.ToBllRole())
            };
        }

        public UserEntity GetUserByEmail(string email)
        {
            return uow.UserRepository.GetUserByEmail(email)?.ToBllUser();
        }

        public UserEntity GetUserById(int id)
        {
            return uow.UserRepository.GetById(id)?.ToBllUser();
        }

        public UserEntity GetUserByNickname(string nickname)
        {
            return uow.UserRepository.GetUserByNickname(nickname)?.ToBllUser();
        }

        public void UpdateUser(FullUserEntity fullUserEntity)
        {
            uow.UserRoleRepository.DeleteAllByUserId(fullUserEntity.Id);
            fullUserEntity.Roles.ToList().ForEach(x => uow.UserRoleRepository.Create(new UserRoleEntity { UserId = fullUserEntity.Id, RoleId = x.Id }.ToDalUserRole()));
            uow.Commit();
        }

        public void UpdateUser(UserEntity userEntity)
        {
            uow.UserRepository.Update(userEntity.ToDalUser());
            uow.Commit();
        }
    }
}
