using Domain.Usuarios.Endereco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosAPI.Models.Usuarios.Endereco
{
    public abstract class BaseUsuarioEnderecoModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(100, ErrorMessage = "O campo deve ter no máximo '{1}' caracteres")]
        public virtual string Logradouro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(10, ErrorMessage = "O campo deve ter no máximo '{1}' caracteres")]
        public virtual string Numero { get; set; }

        [MaxLength(20, ErrorMessage = "O campo deve ter no máximo '{1}' caracteres")]
        public virtual string Complemento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(2, ErrorMessage = "O campo deve ter no máximo '{1}' caracteres")]
        public virtual string Estado { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public virtual EnderecoType Tipo { get; set; }
    }
}
