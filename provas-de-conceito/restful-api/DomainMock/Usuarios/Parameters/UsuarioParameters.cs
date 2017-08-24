using Domain.Base;

namespace Domain.Usuarios.Parameters
{
    public class UsuarioParameters : Parameter
    {
        public SexoType? Sexo { get; set; }
        public string Email { get; set; }
    }
}