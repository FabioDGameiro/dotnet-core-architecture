using AutoMapper;
using RestfulAPI.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPI.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(profile => { profile.AddProfile(new UsuarioProfile()); });
        }
    }
}
