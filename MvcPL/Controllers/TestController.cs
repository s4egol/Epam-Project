using BLL.Interface.Services;
using MvcPL.Infrastructure;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class TestController : Controller
    {
        private IUserService userService;
        private ITestService testService;
        private IQuestionService questionService;
        private IResultService resultService;

        public TestController(IUserService userService, ITestService testService, IQuestionService questionService, IResultService resultService)
        {
            this.userService = userService;
            this.testService = testService;
            this.questionService = questionService;
            this.resultService = resultService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(TestViewModel testViewModel)
        {
            int id = userService.GetUserByNickname(User.Identity.Name).Id;
            if (id > 0)
            {
                if (testService.GetTestByName(testViewModel.TestName)?.ToMvcTest() != null)
                {
                    ModelState.AddModelError("TestName", "Test with this name already exists");
                    return View(testViewModel);
                }
                testViewModel.UserId = id;
                testViewModel.Published = false;
                testService.CreateTest(testViewModel.ToBllTest());
                return RedirectToAction("Index", "Test", new { nameTest = testViewModel.TestName});
            }
            return View(testViewModel);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("Index")]
        public ActionResult Index(string nameTest)
        {
            if (nameTest != null)
            {
                var test = testService.GetTestByName(nameTest)?.ToMvcTest();
                if (test != null)
                    return View("Index", test);
            }
            return View("NotFound");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Show(int? idTest)
        {
            if (idTest != null)
            {
                var test = testService.GetTestById(idTest.Value)?.ToMvcTest();
                if (test != null)
                    return View("Index", testService.GetFullTestEntity(test.ToBllTest())?.ToMvcTest());
            }
            return View("NotFound");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string nameTest)
        {
            if (nameTest != null && nameTest != String.Empty)
            {
                var test = testService.GetTestByName(nameTest)?.ToMvcTest();
                if (test != null)
                    return View(test);
                else
                    return View("NotFound");
            }
            return RedirectToRoute("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(FullTestViewModel fullTest)
        {
            if (fullTest != null)
            {
                var test = new TestViewModel
                {
                    Id = fullTest.Id,
                    TestName = fullTest.TestName,
                    Description = fullTest.Description,
                    Published = fullTest.Published,
                    TimeSec = fullTest.TimeSec,
                    UserId = userService.GetUserByNickname(User.Identity.Name).Id
                };                
                testService.UpdateTest(test.ToBllTest());           
            }
            else
                return View("NotFound");

            return View("Index", testService.GetTestByName(fullTest.TestName).ToMvcTest());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAllTests()
        {
            return View("AllTest", testService.GetAll().Select(x => x.ToMvcTest()));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Publishing(TestViewModel test)
        {
            if (test != null)
            {
                testService.PublichingTest(test.ToBllTest());
            }
            return RedirectToAction("GetAllTests");
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetATestQuestion(int? idTest)
        {
            if (idTest != null)
            {
                var fullQuestion = questionService.MoveToNextQuestion(idTest.Value, User.Identity.Name)?.ToMvcQuestion();
                if (fullQuestion == null)
                    return RedirectToAction("GetResultTesting", new { idOfTest = idTest.Value } );
                if (Request.IsAjaxRequest())
                    return PartialView("QuestionContentPartial", fullQuestion);
                return View(fullQuestion);
            }
            return View("NotFound");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Reply(QuestionViewModel question, int[] isTrueAnswers, string[] answers)
        {
            if (isTrueAnswers != null && isTrueAnswers.Where(x => x == 0).Count() != answers.Count())
            {
                var listAnswers = new List<AnswerViewModel>();
                listAnswers.ToAnswersModel(isTrueAnswers, answers);
                var fullQuestion = new FullQuestionViewModel
                {
                    Id = question.Id,
                    QuestionContent = question.QuestionContent,
                    QuantityPoint = question.QuantityPoint,
                    TestId = question.TestId,
                    Answers = listAnswers
                };
                resultService.SaveResult(fullQuestion.ToBllQuestion(), User.Identity.Name);
                var nextQuestion = questionService.MoveToNextQuestion(question.TestId, User.Identity.Name)?.ToMvcQuestion();
                if (nextQuestion == null)
                    return RedirectToAction("GetResultTesting", new { idOfTest = question.TestId });
                if (Request.IsAjaxRequest())
                    return PartialView("QuestionContentPartial", nextQuestion);
            }         
            return RedirectToAction("GetATestQuestion", new { idTest = question.TestId});
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetResultTesting(int? idOfTest)
        {
            if (idOfTest != null)
            {
                var testResult = resultService.GetResultByIdTest(idOfTest.Value, User.Identity.Name)?.ToMvcFullResult();
                if (testResult != null)
                {
                    if (Request.IsAjaxRequest())
                        return PartialView("ResultContentPartial", testResult);
                    else
                        return View("ResultTesting", testResult);
                }
            }
            return View("ResultNotFound");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? idTest)
        {
            if (idTest != null)
            {
                var test = testService.GetTestById(idTest.Value)?.ToMvcTest();
                if (test != null)
                    return View(test);
            }
            return View("NotFound");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(TestViewModel test)
        {
            if (test != null)
            {
                testService.DeleteTest(test.ToBllTest());
                return RedirectToAction("GetAllTests");
            }
            return View("NotFound");
        }

    }
}