#region Using

using AutoMapper;
using UsuariosAPI.Mappings.Profiles;

#endregion

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