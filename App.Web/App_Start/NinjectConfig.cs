using App.Infrastructure;
using App.Infrastructure.Factories.EntityFramework;
using App.Infrastructure.Services.EntityFramework;
using App.Web.Factories;
using App.Web.Services;

namespace App.Web.App_Start
{
    public class NinjectConfig : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IRoleFactory>().To<RoleService>();
            Bind<ITenantFactory>().To<TenantService>();
            Bind<IUserFactory>().To<UserService>();
            Bind<IPermissionFactory>().To<PermissionService>();
            Bind<IMenuFactory>().To<MenuService>();
            Bind<IAuthFactory>().To<AuthService>();
            Bind<ApplicationDbContext>().ToSelf();
        }
    }
}