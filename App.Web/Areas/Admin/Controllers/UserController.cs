using App.Core.Models.Entities;
using App.Infrastructure.Factories.EntityFramework;
using App.Web.App_Start;
using App.Web.Authorizers;
using App.Web.Controllers;
using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static App.Web.Controllers.AppBaseController;

namespace App.Web.Areas.Admin.Controllers
{
    [PermissionAuthorize("Admin.User.Index")]
    public class UserController : AppBaseController
    {
        private readonly IRoleFactory _roleService;
        private readonly IUserFactory _userService;

        public UserController()
        {
            _roleService = new StandardKernel(new NinjectConfig()).Get<IRoleFactory>();
            _userService = new StandardKernel(new NinjectConfig()).Get<IUserFactory>();
        }

        public ActionResult Index()
        {
            var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            var users = _userService.GetAll(user.TenantID);
            return View(users);
        }

        public ActionResult Create()
        {
            var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            ViewBag.Roles = new SelectList(_roleService.GetAllActive(user.TenantID), "ID", "Name");
            return View(new UserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel model)
        {
            var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            if (!ModelState.IsValid)
            {
                AddModelErrors();
                ViewBag.Roles = new SelectList(_roleService.GetAllActive(user.TenantID), "ID", "Name");
                return View(model);
            }

            if (model.SelectedRoles != null && model.SelectedRoles.Count > 0)
            {
                if (!_userService.Exists(model.Email, user.TenantID))
                {
                    var userVM = _userService.Create(model, user.ID, user.TenantID);
                    _userService.AddRoles(model.SelectedRoles, userVM.ID, user.ID);
                    AddNotification(ExceptionType.success, $"User '{ model.Email }' created successfully!");
                    return RedirectToAction("Index");
                }
                AddNotification(ExceptionType.error, $"User with email address '{ model.Email }' already exist!");
                ViewBag.Roles = new SelectList(_roleService.GetAllActive(user.TenantID), "ID", "Name");
                return View(model);
            }
            else
            {
                AddNotification(ExceptionType.error, $"Please select user roles");
            }
            ViewBag.Roles = new SelectList(_roleService.GetAllActive(user.TenantID), "ID", "Name");
            return View(model);
        }

        public ActionResult Edit(long Id)
        {
            var userVM = _userService.GetById(Id);
            userVM.SelectedRoles = _roleService.GetUserRoles(userVM.ID).Select(s => s.ID).ToList();
            var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            ViewBag.Roles = new SelectList(_roleService.GetAllActive(user.TenantID), "ID", "Name");
            return View(userVM);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(_roleService.GetAllActive(user.TenantID), "ID", "Name");
                return View(model);
            }
            if (model.SelectedRoles != null && model.SelectedRoles.Count > 0)
            {
                if (!_userService.Exists(model.Email, user.TenantID, model.ID))
                {
                    _userService.Update(model, user.ID);
                    AddNotification(ExceptionType.success, $"User '{ model.Email }' updated successfully!");
                    return RedirectToAction("Index");
                }
                AddNotification(ExceptionType.error, $"User with email address '{ model.Email }' already exist!");
            }
            else
            {
                TempData["error"] = $"Please select user roles";
            }
            ViewBag.Roles = new SelectList(_roleService.GetAllActive(user.TenantID), "ID", "Name");
            return View(model);
        }
    }
}