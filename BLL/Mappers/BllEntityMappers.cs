using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        #region TestMapper
        public static DalTest ToDalTest(this TestEntity testEntity)
        {
            return new DalTest
            {
                Id = testEntity.Id,
                NameTest = testEntity.TestName,
                Description = testEntity.Description,
                TimeSec = testEntity.TimeSec,
                Published = testEntity.Published,
                UserId = testEntity.UserId
            };
        }

        public static TestEntity ToBllTest(this DalTest dalTest)
        {
            return new TestEntity
            {
                Id = dalTest.Id,
                TestName = dalTest.NameTest,
                Description = dalTest.Description,
                TimeSec = dalTest.TimeSec,
                Published = dalTest.Published,
                UserId = dalTest.UserId
            };
        }
        #endregion

        #region QuestionMapper
        public static DalQuestion ToDalQuestion(this QuestionEntity questionEntity)
        {
            return new DalQuestion
            {
                Id = questionEntity.Id,
                Question = questionEntity.QuestionContent,
                QuantityPoint = questionEntity.QuantityPoint,
                TestId = questionEntity.TestId
            };
        }

        public static QuestionEntity ToBllQuestion(this DalQuestion dalQuestion)
        {
            return new QuestionEntity
            {
                Id = dalQuestion.Id,
                QuestionContent = dalQuestion.Question,
                QuantityPoint = dalQuestion.QuantityPoint,
                TestId = dalQuestion.TestId
            };
        }
        #endregion

        #region AnswerMapper
        public static DalAnswer ToDalAnswer(this AnswerEntity answer)
        {
            return new DalAnswer
            {
                Id = answer.Id,
                ContentAnswer = answer.ContentAnswer,
                IsTrue = answer.IsTrue,
                QuestionId = answer.QuestionId
            };
        }

        public static AnswerEntity ToBllAnswer(this DalAnswer answer)
        {
            return new AnswerEntity
            {
                Id = answer.Id,
                ContentAnswer = answer.ContentAnswer,
                IsTrue = answer.IsTrue,
                QuestionId = answer.QuestionId
            };
        }
        #endregion

        #region UserMapper
        public static DalUser ToDalUser(this UserEntity entity)
        {
            return new DalUser
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                NickName = entity.NickName,
                Email = entity.Email,
                Password = entity.Password,
                JoinTime = entity.JoinTime
            };
        }

        public static UserEntity ToBllUser(this DalUser entity)
        {
            return new UserEntity
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                NickName = entity.NickName,
                Email = entity.Email,
                Password = entity.Password,
                JoinTime = entity.JoinTime
            };
        }
        #endregion

        #region RoleMapper
        public static DalRole ToDalRole(this RoleEntity entity)
        {
            return new DalRole
            {
                Id = entity.Id,
                Name = entity.Name,
                DiscriptionRole = entity.DiscriptionRole
            };
        }

        public static RoleEntity ToBllRole(this DalRole entity)
        {
            return new RoleEntity
            {
                Id = entity.Id,
                Name = entity.Name,
                DiscriptionRole = entity.DiscriptionRole
            };
        }
        #endregion

        #region UserRoleMapper
        public static DalUserRole ToDalUserRole(this UserRoleEntity userRoleEntity)
        {
            return new DalUserRole
            {
                UserId = userRoleEntity.UserId,
                RoleId = userRoleEntity.RoleId
            };
        }

        public static UserRoleEntity ToBllUserRole(this DalUserRole userRole)
        {
            return new UserRoleEntity
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId
            };
        }
        #endregion

        #region ResultMapper
        public static ResultEntity ToBllResult(this DalResult result)
        {
            return new ResultEntity
            {
                Id = result.Id,
                UserId = result.UserId,
                QuestionId = result.QuestionId,
                IsTrueAnswer = result.IsTrueAnswer,
                PassedTime = result.PassedTime
            };
        }

        public static DalResult ToDalResult(this ResultEntity result)
        {
            return new DalResult
            {
                Id = result.Id,
                UserId = result.UserId,
                QuestionId = result.QuestionId,
                IsTrueAnswer = result.IsTrueAnswer,
                PassedTime = result.PassedTime
            };
        }
        #endregion


    }
}
