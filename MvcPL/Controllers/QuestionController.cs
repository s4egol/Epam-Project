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
    public class QuestionController : Controller
    {
        private IQuestionService questionService;
        private ITestService testService;

        public QuestionController(IQuestionService questionService, ITestService testService)
        {
            this.questionService = questionService;
            this.testService = testService;
        }

        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                QuestionViewModel fullQuestion = new QuestionViewModel { TestId = id.Value };
                return View(fullQuestion);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(QuestionViewModel question, int[] isTrueAnswers, string[] answers)
        {
            if (isTrueAnswers != null && isTrueAnswers.Count() != answers.Count())
            {
                if (question.QuantityPoint <= 0)
                {
                    ModelState.AddModelError("QuantityPoint", "Incorrect quantity point input");
                    return View(question);
                }
                var listAnswers = new List<AnswerViewModel>();
                listAnswers.ToAnswerModel(isTrueAnswers, answers);

                if (questionService.GetQuestionByParams(question.TestId, question.QuestionContent) == null)
                {
                    var fullQuestion = new FullQuestionViewModel
                    {
                        Id = question.Id,
                        QuestionContent = question.QuestionContent,
                        QuantityPoint = question.QuantityPoint,
                        TestId = question.Id,
                        Answers = listAnswers
                    };
                    questionService.CreateQuestion(fullQuestion.ToBllQuestion());
                    return RedirectToAction("Index", "Test", new { nameTest = testService.GetTestById(question.TestId).TestName });
                }
            }
            return View(question);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? idQuestion)
        {
            if (idQuestion != null)
            {
                var simpleQuestion = questionService.GetQuestionById(idQuestion.Value)?.ToMvcQuestion();
                if (simpleQuestion != null)
                {
                    var fullQuestion = questionService.GetFullQuestion(simpleQuestion.ToBllQuestion())?.ToMvcQuestion();
                    return View(fullQuestion);
                }
            }
            return View("NotFound");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(FullQuestionViewModel question, int[] isTrueAnswers, string[] answers)
        {
            if (isTrueAnswers != null && isTrueAnswers.Count() != answers.Count())
            {
                var listAnswers = new List<AnswerViewModel>();
                listAnswers.ToAnswerModel(isTrueAnswers, answers);

                listAnswers.ToList().ForEach(x => x.QuestionId = question.Id);
                question.Answers = listAnswers;
                if (question.QuantityPoint <= 0)
                {
                    ModelState.AddModelError("QuantityPoint", "Incorrect quantity point input");
                    return View(question);
                }
                questionService.UpdateQuestion(question.ToBllQuestion());
            }
            return RedirectToAction("Show", "Test", new { idOfTest = question.TestId });
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? idQuestion)
        {
            if (idQuestion != null)
            {
                var simpleQuestion = questionService.GetQuestionById(idQuestion.Value)?.ToMvcQuestion();
                if (simpleQuestion != null)
                    return View(simpleQuestion);
            }
            return View("NotFound");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(QuestionViewModel question)
        {
            if (question != null)
            {
                var fullQuestion = questionService.GetFullQuestion(question.ToBllQuestion())?.ToMvcQuestion();
                if (fullQuestion != null)
                {
                    questionService.DeleteQuestion(fullQuestion.ToBllQuestion());
                    return RedirectToAction("Show", "Test", new { idOfTest = question.TestId });
                }
            }
            return View("NotFound");
        }

        
    }
}