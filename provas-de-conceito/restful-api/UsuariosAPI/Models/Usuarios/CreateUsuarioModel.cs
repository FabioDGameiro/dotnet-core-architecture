using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Models.Usuarios.Endereco;

namespace UsuariosAPI.Models.Usuarios
{
    public class CreateUsuarioModel : BaseUsuarioModel
    {
        public ICollection<CreateUsuarioEnderecoModel> Enderecos { get; set; } = new List<CreateUsuarioEnderecoModel>();
    }
}
