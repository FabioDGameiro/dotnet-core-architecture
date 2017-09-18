using Domain.Usuarios;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infra.CrossCutting.Reports.UsuariosReports.Repository
{
    public class UsuarioReportService : IUsuarioReportService
    {
        private readonly UsuariosContext _context;

        public UsuarioReportService(UsuariosContext context)
        {
            _context = context;
        }

        public IEnumerable<UsuarioComEnderecoModel> RetornaUsuariosComEndereco()
        {
            return _context.Usuarios
                    .Include(usuario => usuario.Enderecos)
                    .Where(x => x.Enderecos.Count() >= 1)
                    .Select(usuario => new UsuarioComEnderecoModel
                    {
                        Id = usuario.Id,
                        NomeCompleto = $"{usuario.Nome} {usuario.Sobrenome}",
                        TotalEnderecos = usuario.Enderecos.Count()
                    })
                    .ToList();
        }
    }
}