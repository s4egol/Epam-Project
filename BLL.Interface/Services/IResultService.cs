using BLL.Interface.Entities.FullModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IResultService
    {
        void SaveResult(FullQuestionEntity question, string nickname);

        FullResultEntity GetResultByIdTest(int idTest, string nicknameUser);
    }
}
