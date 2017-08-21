using System;

namespace Domain.Usuarios.Endereco
{
    public class UsuarioEndereco
    {
        public Guid Id { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public EnderecoType Tipo { get; set; }
        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }

    public enum EnderecoType
    {
        Residencial = 1,
        Comercial = 2
    }
}
