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
                // Formatando o Nome Completo do Usuário a partir das propriedades Nome e Sobrenome
                .ForMember(t => t.NomeCompleto, opt => opt.MapFrom(s => $"{s.Nome} {s.Sobrenome}"))
                // Formatando a Idade do usuário a partir da sua Data de Nascimento
                .ForMember(t => t.Idade, opt => opt.MapFrom(s => s.DataNascimento.GetCurrentAge()))
                // Formatando o Sexo do usuário a partir do enum SexoType
                .ForMember(t => t.Sexo, opt => opt.MapFrom(s => (s.Sexo.HasValue) ? s.Sexo.Value.GetDescription() : string.Empty));

            // Model -> Entity

        }
    }
}
