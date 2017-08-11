﻿using RestfulAPI.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPI.Models.Empresa
{
    public class EmpresaFilterModel : BaseFilterModel
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
    }
}
