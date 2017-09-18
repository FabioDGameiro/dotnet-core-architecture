#region Using

using System;
using System.Collections.Generic;
using Domain.Base;
using Domain.Usuarios.Enderecos;

#endregion

namespace Domain.Usuarios
{
    public class Usuario : Entity
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public SexoType? Sexo { get; set; }

        public ICollection<UsuarioEndereco> Enderecos { get; set; } = new List<UsuarioEndereco>();
    }

    public enum SexoType
    {
        Masculino = 1,
        Feminino = 2
    }
}