using System;

namespace RestfulAPI.Models.Empresa
{
    public class EmpresaEnderecoModel
    {
        public Guid Id { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
    }
}