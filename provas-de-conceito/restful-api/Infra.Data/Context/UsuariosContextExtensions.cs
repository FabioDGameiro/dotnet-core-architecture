﻿using Domain.Usuarios;
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
                    Nome = "Tiago",
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
                },
                new Usuario
                {
                    Id = new Guid("d78260e5-cff3-42ae-a543-784cdb579550"),
                    DataNascimento = new DateTime(1987, 10, 1),
                    Email = "martin@email.com",
                    Nome = "Martin",
                    Sobrenome = "Santos",
                    Sexo = SexoType.Masculino,
                },
                new Usuario
                {
                    Id = new Guid("cb52e287-a912-4229-ae75-7c31e5ab6d57"),
                    DataNascimento = new DateTime(1996, 5, 20),
                    Email = "marina@email.com",
                    Nome = "Marina",
                    Sobrenome = "Santos",
                    Sexo = SexoType.Feminino,
                },
                new Usuario
                {
                    Id = new Guid("d1619f4e-9846-410a-a567-fde11aeac32d"),
                    DataNascimento = new DateTime(1952, 5, 20),
                    Email = "rose@email.com",
                    Nome = "Rose",
                    Sobrenome = "Santos",
                    Sexo = SexoType.Feminino,
                },
                new Usuario
                {
                    Id = new Guid("1184d5fb-6c44-49ed-843c-f9ac82415c14"),
                    DataNascimento = new DateTime(1976, 5, 2),
                    Email = "alan@email.com",
                    Nome = "Alan",
                    Sobrenome = "Algusto",
                    Sexo = SexoType.Masculino,
                },
                new Usuario
                {
                    Id = new Guid("f0902e0c-c9aa-4b07-bba9-e62e148782db"),
                    DataNascimento = new DateTime(1981, 5, 2),
                    Email = "alan@email.com",
                    Nome = "Alan",
                    Sobrenome = "Santos",
                    Sexo = SexoType.Masculino,
                }
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }
    }
}