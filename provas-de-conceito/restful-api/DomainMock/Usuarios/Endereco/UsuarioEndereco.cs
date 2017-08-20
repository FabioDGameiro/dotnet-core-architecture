namespace Domain.Usuarios
{
    public class UsuarioEndereco
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool IsPrincipal { get; set; }
        public EnderecoType Tipo { get; set; }
    }

    public enum EnderecoType
    {
        Residencial = 1,
        Comercial = 2
    }
}
