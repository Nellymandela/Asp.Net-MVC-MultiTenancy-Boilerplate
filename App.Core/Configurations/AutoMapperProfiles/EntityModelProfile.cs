using App.Core.Domain.Entities;
using App.Core.Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Configurations.AutoMapperProfiles
{
    class EntityModelProfile : Profile
    {
        public EntityModelProfile()
        {
            CreateMap<Role, RoleViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<Tenant, TenantViewModel>();
            CreateMap<Permission, PermissionViewModel>();
            //CreateMap<State, StateViewModel>();
            //CreateMap<LGA, LGAViewModel>();
            //CreateMap<Nationality, NationalityViewModel>();
            //CreateMap<NewEnrollmentViewModel, Pensionier>();
            //CreateMap<BankViewModel, Bank>();
        }
    }
}
