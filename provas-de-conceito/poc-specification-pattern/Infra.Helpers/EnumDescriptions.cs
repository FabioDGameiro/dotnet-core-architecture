#region Using

using Domain.Usuarios;
using Domain.Usuarios.Enderecos;

#endregion

namespace Infra.Helpers
{
    public static class SexoTypeDescriptions
    {
        public static string GetDescription(this SexoType sexo)
        {
            switch (sexo)
            {
                case SexoType.Masculino:
                    return "M";

                case SexoType.Feminino:
                    return "F";

                default:
                    return string.Empty;
            }
        }
    }

    public static class EnderecoTypeDescriptions
    {
        public static string GetDescription(this EnderecoType endereco)
        {
            switch (endereco)
            {
                case EnderecoType.Residencial:
                    return "Residêncial";

                case EnderecoType.Comercial:
                    return "Comercial";

                default:
                    return string.Empty;
            }
        }
    }
}