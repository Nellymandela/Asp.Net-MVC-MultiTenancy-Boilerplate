using App.Core.Common;
using App.Core.Models;
using App.Core.Models.Entities;
using App.Core.Models.Inputs;
using App.Infrastructure.Factories.EntityFramework;
using App.Web.App_Start;
using App.Web.Authorizers;
using App.Web.Controllers;
using App.Web.Factories;
using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Admin.Controllers
{
    public class RoleController : AppBaseController
    {
        private readonly IRoleFactory _roleService;
        private readonly IUserFactory _userService;
        private readonly IPermissionFactory _permissionService;
        private readonly IMenuFactory _menuService;

        public RoleController()
        {
            _roleService = new StandardKernel(new NinjectConfig()).Get<IRoleFactory>();
            _userService = new StandardKernel(new NinjectConfig()).Get<IUserFactory>();
            _permissionService = new StandardKernel(new NinjectConfig()).Get<IPermissionFactory>();
            _menuService = new StandardKernel(new NinjectConfig()).Get<IMenuFactory>();
        }

        //[PermissionAuthorize("Admin.Role.Index")]
        public ActionResult Index()
        {
            var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            var roles = _roleService.GetAll(user.TenantID);
            return View(roles);
        }

        [PermissionAuthorize("Admin.Role.Create")]
        [HttpGet]
        public ActionResult Create()
        {
            return RedirectToAction("Index");
        }

        [PermissionAuthorize("Admin.Role.Create")]
        [HttpPost]
        public ActionResult Create(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                AddModelErrors();
                return RedirectToAction("Index");
            }

            var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            if (!_roleService.Exists(model.Name, user.TenantID))
            {
                var role = _roleService.Create(model, user.ID, user.TenantID);
                AddNotification(ExceptionType.success, $"Role '{ model.Name }' created successfully!");
                AddNotification(ExceptionType.info, $"Assign permissions for this role.");
                return RedirectToAction("Edit", new { Id = role.ID });
            }
            AddNotification(ExceptionType.error, $"Role '{ model.Name }' already exist!");
            return RedirectToAction("Index");
        }

        [PermissionAuthorize("Admin.Role.Edit")]
        public ActionResult Edit(long Id)
        {
            var roleVM = _roleService.GetById(Id);
            roleVM.AssignedPermissions = _roleService.GetAssignedPermissions(Id);
            var permissions = _permissionService.GetAll();
            ViewBag.menu = _menuService.GetPermissionMenu(permissions, roleVM.AssignedPermissions);
            return View(roleVM);
        }

        [PermissionAuthorize("Admin.Role.Edit")]
        [HttpPost]
        public ActionResult Edit(RoleViewModel model, FormCollection form)
        {
            List<MenuViewModel> menus = new List<MenuViewModel>();
            foreach (var menu in _menuService.GetPermissions(new List<MenuViewModel>(), ApplicationStore.ApplicationMenu))
            {
                if (form[menu.Claim.Type] != null)
                {
                    menus.Add(menu);
                }
            }
            var user = _userService.GetById(Convert.ToInt32(User.Identity.GetUserId()));
            if (!_roleService.Exists(model.Name, user.TenantID, model.ID))
            {
                _roleService.Update(model, user.ID);
                AddNotification(ExceptionType.success, $"Role '{ model.Name }' updated successfully!");
            }
            else
            {
                AddNotification(ExceptionType.error, $"Role with name '{ model.Name }' already exist!");
            }
            return RedirectToAction("Edit", new { Id = model.ID });
        }
    }
}