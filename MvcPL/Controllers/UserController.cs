using BLL.Interface.Services;
using MvcPL.Infrastructure;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Providers;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcPL.Controllers
{
    public class UserController : Controller
    {
        private int DefaultRole = 1;

        private IUserService userService;
        private IRoleService roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(userService.GetAllUsers().Select(x => userService.GetFullUserEntity(x).ToMvcUser()));
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(FullUserViewModel userViewModel)
        {
            if (userViewModel.Captcha != (string)Session["code"])
            {
                ModelState.AddModelError("Captcha", "Incorrect captcha input");
                return View(userViewModel);
            }
            if (ModelState.IsValid)
            {
                if (userService.GetUserByNickname(userViewModel.NickName) != null)
                    ModelState.AddModelError("Nickname", "User with this Nickname already exists");
                else if (userService.GetUserByEmail(userViewModel.Email) != null)
                    ModelState.AddModelError("E-mail", "User with this e-mail already exists");
                else
                {
                    var currentUser = ((CustomMembershipProvider)Membership.Provider).GetUser(userViewModel.NickName, false);
                    if (currentUser == null)
                    {
                        var role = new List<RoleViewModel>();
                        role.Add(roleService.GetRole(DefaultRole)?.ToMvcRole());
                        userViewModel.Roles = role;
                        userViewModel.JoinTime = DateTime.Now;
                        userViewModel.Password = HashForPassword.GenerateHash(userViewModel.Password);
                        userService.CreateUser(userViewModel.ToBllUser());
                        FormsAuthentication.SetAuthCookie(userViewModel.NickName, false);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "This user already exist");
                }
            }

            return View(userViewModel);
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View("SignIn");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginViewModel model, string ReturnUrl)
        {
            if (Membership.ValidateUser(model.Nickname, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Nickname, true);
                if (Url.IsLocalUrl(ReturnUrl))
                    return Redirect(ReturnUrl);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Incorrect login or password");
            return View("SignIn");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return View("NotFound");
            var user = userService.GetUserById(id.Value);
            if (user == null)
                return View("NotFound");
            var fullUser = userService.GetFullUserEntity(user)?.ToMvcUser();
            if (fullUser == null)
                return View("NotFound");
            else
                return View(fullUser);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(FullUserViewModel user, int[] Role)
        {
            if (Role != null && Role.Length > 0)
            {
                user.Roles = Role.Select(x => roleService.GetRole(x).ToMvcRole());
                userService.UpdateUser(user.ToBllUser());
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details()
        {
            var userEntity = userService.GetUserByNickname(User.Identity.Name).ToMvcUser();
            var userEditor = new UserEditorViewModel
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Surname = userEntity.Surname,
                NickName = userEntity.NickName
            };
            return View(userEditor);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(UserEditorViewModel userEditor)
        {
            if (userEditor != null)
            {
                if (userEditor.OldPassword != null)
                {
                    if (userService.GetUserByNickname(userEditor.NickName).ToMvcUser().Password == HashForPassword.GenerateHash(userEditor.OldPassword))
                    {
                        userEditor.NewPassword = HashForPassword.GenerateHash(userEditor.NewPassword);
                        if (userEditor.NewPassword == HashForPassword.GenerateHash(userEditor.ConfirmNewPassword))
                        {
                            userService.UpdateUser(userEditor.ToBllEditorUser());
                            return View(userEditor);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("OldPassword", "Old password is incorrect");
                        return View(userEditor);
                    }
                }
                else
                {
                    userEditor.NewPassword = String.Empty;
                    userService.UpdateUser(userEditor.ToBllEditorUser());
                    return View(userEditor);
                }
            }
            return View(userEditor);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return View("NotFound");
            var user = userService.GetUserById(id.Value);
            if (user == null)
                return View("NotFound");
            var fullUser = userService.GetFullUserEntity(user)?.ToMvcUser();
            if (fullUser == null)
                return View("NotFound");
            else
                return View(fullUser);
        }

        [HttpPost]
        public ActionResult SearchNickname(string nickName)
        {
            if (nickName != null && nickName != String.Empty)
            {
                var user = userService.GetUserByNickname(nickName)?.ToMvcUser();
                if (user != null)
                {
                    if (Request.IsAjaxRequest())
                        return Json(userService.GetFullUserEntity(user.ToBllUser())?.ToMvcUser());                  
                }
            }
            return View("NotFound");
        }

        public ActionResult Partial()
        {
            return PartialView("PartialRole", roleService.GetAllRoles().Select(x => x.ToMvcRole()));
        }

        public ActionResult Captcha()
        {
            string code = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            Session["code"] = code;
            Captcha captcha = new Captcha(code, 211, 50, "Ubuntu");

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            captcha.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            captcha.Dispose();
            return null;
        }
    }
}