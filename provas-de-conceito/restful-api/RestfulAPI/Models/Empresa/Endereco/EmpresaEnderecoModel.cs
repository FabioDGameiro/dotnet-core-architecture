using RestfulAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPI.Models.Empresa
{
    public class EmpresaEnderecoModel : BaseModel
    {
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
    }
}
