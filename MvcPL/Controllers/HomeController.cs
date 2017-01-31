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
    public class HomeController : Controller
    {
        private const int PageSize = 3;
        private readonly ITestService testService;

        public HomeController(ITestService testService)
        {
            this.testService = testService;
        }

        public ActionResult Index(int page = 1)
        {
            var result = new PagingViewModel<TestViewModel>();
            int totalItems = 0;
            result.Items = testService.GetTestsForPage((page - 1) * PageSize, PageSize, ref totalItems)
                .Select(x => x.ToMvcTest());

            result.Paging = new Paging { PageNumber = page, PageSize = PageSize, TotalItems = totalItems };
            if (Request.IsAjaxRequest())
            {
                return PartialView("ContentPartial", result);
            }
            return View(result);
        }

        public ActionResult Search(string seachText)
        {
            if (seachText == null || seachText == String.Empty)
                return RedirectToAction("Index");
            var result = new PagingViewModel<TestViewModel>();
            result.Paging = new Paging { PageSize = PageSize};
            result.Items = testService.GetTestsForPageByText(seachText)
                .Select(x => x.ToMvcTest());
            if (result.Items != null)
            {
                if (Request.IsAjaxRequest())
                {
                    if (result.Items.Count() > 3)
                        return PartialView("ContentPartial", result.Items.Take(3));
                    else
                        return PartialView("ContentPartial", result);
                }
            }
            return View("Index", result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}