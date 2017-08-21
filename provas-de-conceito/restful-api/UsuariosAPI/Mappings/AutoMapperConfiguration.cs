using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Mappings.Profiles;

namespace UsuariosAPI.Mappings
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(profile => { profile.AddProfile(new UsuariosProfile()); });
        }
    }
}
