using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Models.Usuarios.Endereco;

namespace UsuariosAPI.Models.Usuarios
{
    public class UpdateUsuarioModel : BaseUsuarioModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public override string Sobrenome { get => base.Sobrenome; set => base.Sobrenome = value; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public override string Email { get => base.Email; set => base.Email = value; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public override DateTime? DataNascimento { get => base.DataNascimento; set => base.DataNascimento = value; }
    }
}
