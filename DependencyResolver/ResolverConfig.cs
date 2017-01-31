using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using DAL.Interface.Repository;
using DAL.Concrete;
using ORM;
using BLL.Interface.Services;
using BLL.Services;
using Ninject.Web.Common;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<EntityModel>().To<EntityModel>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<EntityModel>().To<EntityModel>().InSingletonScope();
            }

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();

            kernel.Bind<ITestService>().To<TestService>();
            kernel.Bind<ITestRepository>().To<TestRepository>();

            kernel.Bind<IQuestionService>().To<QuestionService>();
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>();

            kernel.Bind<IAnswerRepository>().To<AnswerRepository>();

            kernel.Bind<IResultService>().To<ResultService>();
            kernel.Bind<IResultRepository>().To<ResultRepository>();
        }
    }
}

