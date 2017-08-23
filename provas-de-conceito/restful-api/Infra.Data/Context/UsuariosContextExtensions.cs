using Domain.Usuarios;
using Domain.Usuarios.Endereco;
using System;
using System.Collections.Generic;

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
                            Logradouro = "Rua Jose Morais",
                            Numero = "763",
                            Complemento = "AP 759",
                            Estado = "SP",
                            Tipo = EnderecoType.Residencial
                        },
                        new UsuarioEndereco
                        {
                            Id = new Guid("b829b507-fc13-440f-b503-ff0ddd8075ee"),
                            Logradouro = "Rua Jardim Morais",
                            Numero = "12",
                            Complemento = "10º Andar - Ramal 2",
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
                            Logradouro = "Rua Maria das Dores",
                            Numero = "9827",
                            Estado = "RJ",
                            Tipo = EnderecoType.Residencial
                        }
                    }
                },
                new Usuario
                {
                    Id = new Guid("8f1ff6e5-ed57-4ec1-b01b-7fe6c6624d70"),
                    DataNascimento = new DateTime(1985, 3, 1),
                    Email = "iran@email.com",
                    Nome = "Iran",
                    Sobrenome = "Carvalho",
                    Sexo = SexoType.Masculino,
                    Enderecos = new List<UsuarioEndereco>
                    {
                        new UsuarioEndereco
                        {
                            Id = new Guid("67c8ccc5-de65-4c71-abaa-36c1b225f025"),
                            Logradouro = "Rua José Pinheiro",
                            Numero = "763",
                            Estado = "SP",
                            Tipo = EnderecoType.Residencial
                        },
                        new UsuarioEndereco
                        {
                            Id = new Guid("22a43f85-da68-4a35-850e-020b7597525a"),
                            Logradouro = "Av. Brasil",
                            Numero = "48512",
                            Complemento = "Ramal 239",
                            Estado = "RJ",
                            Tipo = EnderecoType.Comercial
                        }
                    }
                },
                new Usuario
                {
                    Id = new Guid("69abf241-efc7-4e48-8b1a-250e9ca3a47c"),
                    DataNascimento = new DateTime(1989, 12, 23),
                    Email = "pamela@email.com",
                    Nome = "Pamela",
                    Sobrenome = "Consuelo",
                    Sexo = SexoType.Feminino,
                    Enderecos = new List<UsuarioEndereco>
                    {
                        new UsuarioEndereco
                        {
                            Id = new Guid("c9f946f1-39ef-41b7-a3d6-7bc1cabde805"),
                            Logradouro = "Av. Emilio Esteves",
                            Numero = "957",
                            Complemento = "Casa 3",
                            Estado = "SP",
                            Tipo = EnderecoType.Residencial
                        }
                    }
                },
                new Usuario
                {
                    Id = new Guid("b689073a-2963-49ee-b529-9f68228176a2"),
                    DataNascimento = new DateTime(1988, 3, 3),
                    Email = "luana@email.com",
                    Nome = "Luana",
                    Sobrenome = "Martins",
                    Sexo = SexoType.Feminino
                }
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }
    }
}