using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.CrossCutting.Reports.UsuariosReports.Repository
{
    public interface IUsuarioReportService
    {
        IEnumerable<UsuarioComEnderecoModel> RetornaUsuariosComEndereco();
    }
}
