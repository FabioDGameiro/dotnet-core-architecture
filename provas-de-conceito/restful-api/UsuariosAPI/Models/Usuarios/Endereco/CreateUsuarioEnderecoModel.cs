using Domain.Usuarios.Endereco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosAPI.Models.Usuarios.Endereco
{
    public class CreateUsuarioEnderecoModel
    {
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public EnderecoType Tipo { get; set; }
    }
}
