using System;
using AutoMapper;
using Domain.Usuarios;
using Domain.Usuarios.Endereco;
using Infra.Helpers;
using UsuariosAPI.Models.Usuarios;
using UsuariosAPI.Models.Usuarios.Endereco;

namespace UsuariosAPI.Mappings.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            UsuarioMappings();
            UsuarioEnderecoMappings();
        }

        private void UsuarioMappings()
        {
            // Entity -> Model

            CreateMap<Usuario, GetUsuarioModel>()

                // Formatando o Nome Completo do Usuário a partir das propriedades Nome e Sobrenome
                .ForMember(t => t.NomeCompleto, opt => opt.MapFrom(s => $"{s.Nome} {s.Sobrenome}"))

                // Formatando a Idade do usuário a partir da sua Data de Nascimento
                .ForMember(t => t.Idade, opt => opt.MapFrom(s => s.DataNascimento.GetCurrentAge()))

                // Formatando o Sexo do usuário a partir do enum SexoType
                .ForMember(t => t.Sexo, opt => opt.MapFrom(s => (s.Sexo.HasValue) ? s.Sexo.Value.GetDescription() : null));

            CreateMap<Usuario, UpdateUsuarioModel>();

            // Model -> Entity

            CreateMap<CreateUsuarioModel, Usuario>();
            CreateMap<UpdateUsuarioModel, Usuario>();
        }

        private void UsuarioEnderecoMappings()
        {
            // Entity -> Model

            CreateMap<UsuarioEndereco, GetUsuarioEnderecoModel>()

                // Formatando o endereço no padrão 'Logradouro, Numero - Complemento (caso exista complemento)'
                // utilizando o método ToString() que já está com a formatação implementada na entidade
                .ForMember(t => t.Endereco, opt => opt.MapFrom(s => s.ToString()))

                // Formatando o Tipo do endereço a partir do enum EnderecoType
                .ForMember(t => t.Tipo, opt => opt.MapFrom(s => s.Tipo.GetDescription()));

            CreateMap<UsuarioEndereco, UpdateUsuarioEnderecoModel>();

            // Model -> Entity

            CreateMap<CreateUsuarioEnderecoModel, UsuarioEndereco>();
            CreateMap<UpdateUsuarioEnderecoModel, UsuarioEndereco>();
        }

    }
}