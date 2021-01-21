using App.Core.Configurations.AutoMapperProfiles;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Configurations
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configurations;

        public static void RegisterMappingObjects()
        {
            Configurations = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntityModelProfile>();
            });
            Configurations.CreateMapper();
        }
    }
}
