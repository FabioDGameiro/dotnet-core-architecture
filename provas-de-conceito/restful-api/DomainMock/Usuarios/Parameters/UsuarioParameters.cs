#region Using

using Domain.Base;

#endregion

namespace Domain.Usuarios.Parameters
{
    public class UsuarioParameters : Parameter
    {
        public SexoType? Sexo { get; set; }
        public string Email { get; set; }
        public bool Ativos { get; set; }
        public bool Inativos { get; set; }
        public bool MaioresDeIdade { get; set; }
    }
}