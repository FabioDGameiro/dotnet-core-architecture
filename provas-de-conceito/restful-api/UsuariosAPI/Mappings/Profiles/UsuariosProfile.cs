using AutoMapper;
using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Models.Usuarios;
using Infra.Helpers;

namespace UsuariosAPI.Mappings.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            // Entity -> Model
            CreateMap<Usuario, UsuarioGetModel>()
                .ForMember(t => t.NomeCompleto, opt => opt.MapFrom(s => $"{s.Nome} {s.Sobrenome}"))
                .ForMember(t => t.Idade, opt => opt.MapFrom(s => s.DataNascimento.GetCurrentAge()))
                .ForMember(t => t.Sexo, opt => opt.MapFrom(s => (s.Sexo.HasValue) ? s.Sexo.Value.GetDescription() : string.Empty));

            // Model -> Entity

        }
    }
}
