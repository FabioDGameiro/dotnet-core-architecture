#region Using

using System.Collections.Generic;
using UsuariosAPI.Models.Usuarios.Endereco;

#endregion

namespace UsuariosAPI.Models.Usuarios
{
    public class CreateUsuarioModel : BaseUsuarioModel
    {
        public ICollection<CreateUsuarioEnderecoModel> Enderecos { get; set; } = new List<CreateUsuarioEnderecoModel>();
    }
}