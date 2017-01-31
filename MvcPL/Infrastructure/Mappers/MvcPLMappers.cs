using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;
using BLL.Interface.Entities.FullModel;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcPLMappers
    {
        #region TestMappers
        public static TestEntity ToBllTest(this TestViewModel testViewModel)
        {
            return new TestEntity
            {
                Id = testViewModel.Id,
                TestName = testViewModel.TestName,
                Description = testViewModel.Description,
                TimeSec = testViewModel.TimeSec,
                Published = testViewModel.Published,
                UserId = testViewModel.UserId
            };
        }

        public static TestViewModel ToMvcTest(this TestEntity testEntity)
        {
            return new TestViewModel
            {
                Id = testEntity.Id,
                TestName = testEntity.TestName,
                Description = testEntity.Description,
                TimeSec = testEntity.TimeSec,
                Published = testEntity.Published,
                UserId = testEntity.UserId
            };
        }

        public static FullTestEntity ToBllTest(this FullTestViewModel test)
        {
            return new FullTestEntity
            {
                Id = test.Id,
                Name = test.TestName,
                Description = test.Description,
                TimeSec = test.TimeSec,
                Published = test.Published,
                UserId = test.UserId,
                Questions = test.Questions.Select(x => x.ToBllQuestion())
            };
        }

        public static FullTestViewModel ToMvcTest(this FullTestEntity test)
        {
            return new FullTestViewModel
            {
                Id = test.Id,
                TestName = test.Name,
                Description = test.Description,
                TimeSec = test.TimeSec,
                Published = test.Published,
                UserId = test.UserId,
                Questions = test.Questions.Select(x => x.ToMvcQuestion())
            };
        }
        #endregion

        #region QuestionMappers
        public static QuestionEntity ToBllQuestion(this QuestionViewModel questionViewModel)
        {
            return new QuestionEntity
            {
                Id = questionViewModel.Id,
                QuestionContent = questionViewModel.QuestionContent,
                QuantityPoint = questionViewModel.QuantityPoint,
                TestId = questionViewModel.TestId
            };
        }

        public static QuestionViewModel ToMvcQuestion(this QuestionEntity questionEntity)
        {
            return new QuestionViewModel
            {
                Id = questionEntity.Id,
                QuestionContent = questionEntity.QuestionContent,
                QuantityPoint = questionEntity.QuantityPoint,
                TestId = questionEntity.TestId
            };
        }

        public static FullQuestionEntity ToBllQuestion(this FullQuestionViewModel questionModel)
        {
            return new FullQuestionEntity
            {
                Id = questionModel.Id,
                QuestionContent = questionModel.QuestionContent,
                QuantityPoint = questionModel.QuantityPoint,
                TestId = questionModel.TestId,
                Answers = questionModel.Answers.Select(x => x.ToBllAnswer())
            };
        }

        public static FullQuestionViewModel ToMvcQuestion(this FullQuestionEntity questionModel)
        {
            return new FullQuestionViewModel
            {
                Id = questionModel.Id,
                QuestionContent = questionModel.QuestionContent,
                QuantityPoint = questionModel.QuantityPoint,
                TestId = questionModel.TestId,
                Answers = questionModel.Answers.Select(x => x.ToMvcAnswer())
            };
        }
        #endregion

        #region AnswerMapper
        public static AnswerEntity ToBllAnswer(this AnswerViewModel answer)
        {
            return new AnswerEntity
            {
                Id = answer.Id,
                ContentAnswer = answer.ContentAnswer,
                IsTrue = answer.IsTrue,
                QuestionId = answer.QuestionId
            };
        }

        public static AnswerViewModel ToMvcAnswer(this AnswerEntity answer)
        {
            return new AnswerViewModel
            {
                Id = answer.Id,
                ContentAnswer = answer.ContentAnswer,
                IsTrue = answer.IsTrue,
                QuestionId = answer.QuestionId
            };
        }
        #endregion

        #region RoleMappers
        public static RoleViewModel ToMvcRole(this RoleEntity roleEntity)
        {
            return new RoleViewModel
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name,
                DiscriptionRole = roleEntity.DiscriptionRole
            };
        }

        public static RoleEntity ToBllRole(this RoleViewModel roleViewModel)
        {
            return new RoleEntity
            {
                Id = roleViewModel.Id,
                Name = roleViewModel.Name,
                DiscriptionRole = roleViewModel.DiscriptionRole
            };
        }
        #endregion

        #region UserMappers
        public static FullUserViewModel ToMvcUser(this FullUserEntity userEntity)
        {
            return new FullUserViewModel
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Surname = userEntity.Surname,
                NickName = userEntity.NickName,
                Email = userEntity.Email,
                Password = userEntity.Password,
                JoinTime = userEntity.JoinTime,
                Roles = userEntity.Roles.Select(x => x.ToMvcRole())
            };
        }

        public static FullUserEntity ToBllUser(this FullUserViewModel userEntity)
        {
            return new FullUserEntity
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Surname = userEntity.Surname,
                NickName = userEntity.NickName,
                Email = userEntity.Email,
                Password = userEntity.Password,
                JoinTime = userEntity.JoinTime,
                Roles = userEntity.Roles.Select(x => x.ToBllRole())
            };
        }

        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Surname = userEntity.Surname,
                NickName = userEntity.NickName,
                Email = userEntity.Email,
                Password = userEntity.Password,
                JoinTime = userEntity.JoinTime
            };
        }

        public static UserEntity ToBllUser(this UserViewModel userEntity)
        {
            return new UserEntity
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Surname = userEntity.Surname,
                NickName = userEntity.NickName,
                Email = userEntity.Email,
                Password = userEntity.Password,
                JoinTime = userEntity.JoinTime
            };
        }

        public static UserEntity ToBllEditorUser(this UserEditorViewModel user)
        {
            return new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                NickName = user.NickName,
                Password = user.NewPassword
            };
        }

        public static UserEditorViewModel ToMvcEditorUser(this UserEntity user)
        {
            return new UserEditorViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                NickName = user.NickName,
                OldPassword = user.Password
            };
        }
        #endregion

        #region ResultMapper
        public static FullResultViewModel ToMvcFullResult(this FullResultEntity result)
        {
            var listResult = new List<ResultQuestionViewModel>();
            foreach (var item in result.Questions)
            {
                listResult.Add(new ResultQuestionViewModel
                {
                    Id = item.Id,
                    QuestionContent = item.QuestionContent,
                    QuantityPoint = item.QuantityPoint,
                    IsTrueQuestion = item.IsTrueQuestion,
                    Answers = item.Answers.Select(x => x.ToMvcAnswer())
                });
            }

            return new FullResultViewModel
            {
                TestName = result.TestName,
                Questions = listResult
            };
        }
        #endregion
    }
}