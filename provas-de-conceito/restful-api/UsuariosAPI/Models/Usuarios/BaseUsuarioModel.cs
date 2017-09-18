#region Using

using System;
using System.ComponentModel.DataAnnotations;
using Domain.Usuarios;

#endregion

namespace UsuariosAPI.Models.Usuarios
{
    public abstract class BaseUsuarioModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(20, ErrorMessage = "O campo deve ter no máximo '{1}' caracteres")]
        public virtual string Nome { get; set; }

        [MaxLength(30, ErrorMessage = "O campo deve ter no máximo '{1}' caracteres")]
        public virtual string Sobrenome { get; set; }

        [MaxLength(30, ErrorMessage = "O campo deve ter no máximo '{1}' caracteres")]
        public virtual string Email { get; set; }

        public virtual DateTime? DataNascimento { get; set; }

        public virtual SexoType? Sexo { get; set; }
    }
}