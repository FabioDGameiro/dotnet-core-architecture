using System;
using System.Collections.Generic;
using System.Text;
using Domain.Usuarios;
using Domain.Usuarios.Endereco;

namespace Infra.Data.Context
{
    public static class UsuariosContextExtensions
    {
        public static void EnsureSeedDataForContext(this UsuariosContext context)
        {
            context.Usuarios.RemoveRange(context.Usuarios);
            context.SaveChanges();

            var usuarios = new List<Usuario>()
            {
                new Usuario
                {
                    Id = new Guid("96a5bd92-0734-474a-9c38-46f03ac5be23"),
                    DataNascimento = new DateTime(1987, 3, 13),
                    Email = "tiago@email.com",
                    Nome = "Robson",
                    Sobrenome = "Santos",
                    Sexo = SexoType.Masculino,
                    Enderecos = new List<UsuarioEndereco>
                    {
                        new UsuarioEndereco
                        {
                            Id = new Guid("c4ebe549-cc93-439a-8003-45024edacd2d"),
                            Endereco = "Rua Jose Morais, 763",
                            Estado = "SP",
                            Tipo = EnderecoType.Residencial
                        },
                        new UsuarioEndereco
                        {
                            Id = new Guid("b829b507-fc13-440f-b503-ff0ddd8075ee"),
                            Endereco = "Rua Jardim Morais, 12",
                            Estado = "SP",
                            Tipo = EnderecoType.Comercial
                        }
                    }
                },
                new Usuario
                {
                    Id = new Guid("2a3044b5-cc02-4890-a328-a8279ee70007"),
                    DataNascimento = new DateTime(1995, 5, 8),
                    Email = "maria@email.com",
                    Nome = "Maria",
                    Sobrenome = "Rosado",
                    Sexo = SexoType.Feminino,
                    Enderecos = new List<UsuarioEndereco>
                    {
                        new UsuarioEndereco
                        {
                            Id = new Guid("55509a74-41ee-412e-8af9-70ce65c8ba03"),
                            Endereco = "Rua Maria das Dores, 9827",
                            Estado = "RJ",
                            Tipo = EnderecoType.Residencial
                        }
                    }
                }
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }
    }
}
