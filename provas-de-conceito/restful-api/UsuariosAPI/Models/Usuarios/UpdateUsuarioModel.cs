using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Models.Usuarios.Endereco;

namespace UsuariosAPI.Models.Usuarios
{
    public class UpdateUsuarioModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public SexoType? Sexo { get; set; }
    }
}
