using System;

namespace RestfulAPI.Models.Empresa
{
    public class EmpresaModel
    {
        public Guid Id { get; set; }
        public string Cnpj { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string SocioProprietario { get; set; }
        public string Ramo { get; set; }
        public string Categoria { get; set; }
    }
}