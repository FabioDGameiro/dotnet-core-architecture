#region Using

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UsuariosAPI.Models.Usuarios
{
    public class UpdateUsuarioModel : BaseUsuarioModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public override string Sobrenome
        {
            get => base.Sobrenome;
            set => base.Sobrenome = value;
        }

        [Required(ErrorMessage = "Campo obrigatório")]
        public override string Email
        {
            get => base.Email;
            set => base.Email = value;
        }

        [Required(ErrorMessage = "Campo obrigatório")]
        public override DateTime? DataNascimento
        {
            get => base.DataNascimento;
            set => base.DataNascimento = value;
        }
    }
}