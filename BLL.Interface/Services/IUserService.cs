using BLL.Interface.Entities;
using BLL.Interface.Entities.FullModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        UserEntity GetUserById(int id);
        IEnumerable<UserEntity> GetAllUsers();
        void CreateUser(UserEntity userEntity);
        void CreateUser(FullUserEntity userEntity);
        void DeleteUser(UserEntity userEntity);
        void UpdateUser(UserEntity userEntity);
        void UpdateUser(FullUserEntity fullUserEntity);
        UserEntity GetUserByEmail(string email);
        UserEntity GetUserByNickname(string nickname);
        FullUserEntity GetFullUserEntity(UserEntity e);
    }
}
