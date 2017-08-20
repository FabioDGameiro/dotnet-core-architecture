using System;
using System.Collections.Generic;

namespace Domain.Usuarios
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public SexoType Sexo { get; set; }

        public ICollection<UsuarioEndereco> Enderecos { get; set; }
    }

    public enum SexoType
    {
        Masculino = 1,
        Feminino = 2
    }
}