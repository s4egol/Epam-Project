using DAL.Interface.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class DalMapper
    {
        #region UserMapper
        public static User ToOrmUser(this DalUser user)
        {
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Nickname = user.NickName,
                Email = user.Email,
                Password = user.Password,
                JoinTime = user.JoinTime
            };
        }

        public static DalUser ToDalUser(this User user)
        {
            return new DalUser
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                NickName = user.Nickname,
                Email = user.Email,
                Password = user.Password,
                JoinTime = user.JoinTime
            };
        }
        #endregion

        #region RoleMapper
        public static Role ToOrmRole(this DalRole role)
        {
            return new Role
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.DiscriptionRole
            };
        }

        public static DalRole ToDalRole(this Role role)
        {
            return new DalRole
            {
                Id = role.Id,
                Name = role.Name,
                DiscriptionRole = role.Description
            };
        }
        #endregion

        #region UserRole
        public static UserRole ToOrmUserRole(this DalUserRole userRoleEntity)
        {
            return new UserRole
            {
                UserId = userRoleEntity.UserId,
                RoleId = userRoleEntity.RoleId
            };
        }

        public static DalUserRole ToDalUserRole(this UserRole userRoleEntity)
        {
            return new DalUserRole
            {
                UserId = userRoleEntity.UserId,
                RoleId = userRoleEntity.RoleId
            };
        }
        #endregion

        #region TestMapper
        public static Test ToOrmTest(this DalTest test)
        {
            return new Test
            {
                Id = test.Id,
                Name = test.NameTest,
                Description = test.Description,
                Published = test.Published,
                UserId = test.UserId
            };
        }

        public static DalTest ToDalTest(this Test test)
        {
            return new DalTest
            {
                Id = test.Id,
                NameTest = test.Name,
                Description = test.Description,
                Published = test.Published,
                UserId = test.UserId
            };
        }
        #endregion

        #region QuestionMapper
        public static Question ToOrmQuestion(this DalQuestion question)
        {
            return new Question
            {
                Id = question.Id,
                Content = question.Question,
                QuantityPoint = question.QuantityPoint,
                TestId = question.TestId
            };
        }

        public static DalQuestion ToDalQuestion (this Question question)
        {
            return new DalQuestion
            {
                Id = question.Id,
                Question = question.Content,
                QuantityPoint = question.QuantityPoint,
                TestId = question.TestId
            };
        }
        #endregion

        #region AnswerMapper
        public static Answer ToOrmAnswer(this DalAnswer answer)
        {
            return new Answer
            {
                Id = answer.Id,
                ContentAnswer = answer.ContentAnswer,
                IsTrue = answer.IsTrue,
                QuestionId = answer.QuestionId
            };
        }

        public static DalAnswer ToDalAnswer(this Answer answer)
        {
            return new DalAnswer
            {
                Id = answer.Id,
                ContentAnswer = answer.ContentAnswer,
                IsTrue = answer.IsTrue,
                QuestionId = answer.QuestionId
            };
        }
        #endregion

        #region ResultMapper
        public static Result ToOrmResult(this DalResult result)
        {
            return new Result
            {
                Id = result.Id,
                PassedUserId = result.UserId,
                QuestionId = result.QuestionId,
                PassedTime = result.PassedTime,
                IsTrueAnswer = result.IsTrueAnswer
            };
        }

        public static DalResult ToDalResult(this Result result)
        {
            return new DalResult
            {
                Id = result.Id,
                UserId = result.PassedUserId,
                QuestionId = result.QuestionId,
                PassedTime = result.PassedTime,
                IsTrueAnswer = result.IsTrueAnswer
            };
        }
        #endregion

    }
}
