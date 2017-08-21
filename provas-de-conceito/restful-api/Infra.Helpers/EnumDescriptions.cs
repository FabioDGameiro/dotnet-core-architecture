using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

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
}
