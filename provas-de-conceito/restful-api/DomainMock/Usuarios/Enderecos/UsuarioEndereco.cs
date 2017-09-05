using System;
using System.Text;

namespace Domain.Usuarios.Endereco
{
    public class UsuarioEndereco
    {
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }
        public EnderecoType Tipo { get; set; }
        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder($"{Logradouro}, {Numero}");

            if (!String.IsNullOrWhiteSpace(Complemento))
                sb.Append($" - {Complemento}");

            return sb.ToString();

        }
    }

    public enum EnderecoType
    {
        Residencial = 1,
        Comercial = 2
    }
}