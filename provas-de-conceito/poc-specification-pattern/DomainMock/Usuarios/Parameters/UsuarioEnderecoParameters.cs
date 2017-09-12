#region Using

using System;
using Domain.Base;

#endregion

namespace Domain.Usuarios.Parameters
{
    public class UsuarioEnderecoParameters : Parameter
    {
        public Guid UsuarioId { get; set; }
    }
}