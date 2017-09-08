using System;

namespace UsuariosAPI.Models.Usuarios.Endereco
{
    public class GetUsuarioEnderecoModel
    {
        public Guid Id { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}