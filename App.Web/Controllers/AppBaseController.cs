using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Infrastructure.Factories.EntityFramework;
using App.Web.App_Start;
using Ninject;
using App.Core.Models.Inputs;
using App.Infrastructure;

namespace App.Web.Controllers
{
    public abstract class AppBaseController : Controller
    {
        public readonly ITenantFactory _tenantService;

        public AppBaseController()
        {
            _tenantService = new StandardKernel(new NinjectConfig()).Get<ITenantFactory>();
        }

        public void AddErrors(string[] errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public void AddNotification(ExceptionType type, string message)
        {
            string value = type.ToString();
            if (TempData[value] != null)
            {
                TempData[value] = $"{TempData[value]}{Environment.NewLine}{message}";
            }
            else
            {
                TempData[value] = message;
            }
        }

        public void AddModelErrors()
        {
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    AddNotification(ExceptionType.error, error.ErrorMessage);
                }
            }
        }

        public enum ExceptionType{ error, warning, success, info }
    }
}