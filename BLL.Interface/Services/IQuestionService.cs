using BLL.Interface.Entities;
using BLL.Interface.Entities.FullModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IQuestionService
    {
        IEnumerable<QuestionEntity> GetQuestionsByIdTest(int id);
        QuestionEntity GetQuestionById(int id);
        FullQuestionEntity GetFullQuestion(QuestionEntity questionEntity);
        void CreateQuestion(FullQuestionEntity questionEntity);
        void DeleteQuestion(FullQuestionEntity questionEntity);
        void UpdateQuestion(FullQuestionEntity questionEntity);
        QuestionEntity GetQuestionByParams(int idTest, string question);
        FullQuestionEntity MoveToNextQuestion(int idTest, string nicknameUser);
    }
}
