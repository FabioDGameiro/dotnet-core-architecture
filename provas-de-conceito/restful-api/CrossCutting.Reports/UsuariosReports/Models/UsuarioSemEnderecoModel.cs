using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.CrossCutting.Reports.UsuariosReports
{
    public class UsuarioSemEnderecoModel
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
    }
}
