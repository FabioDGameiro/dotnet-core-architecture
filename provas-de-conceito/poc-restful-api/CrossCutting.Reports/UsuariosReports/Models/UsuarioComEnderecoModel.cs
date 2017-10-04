using Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.CrossCutting.Reports.UsuariosReports
{
    public class UsuarioComEnderecoModel
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public int TotalEnderecos { get; set; }
    }
}
