using RestfulAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPI.Models.Empresa
{
    public class EmpresaCompactModel : BaseModel
    {
        public string RazaoSocial { get; set; }
    }
}
