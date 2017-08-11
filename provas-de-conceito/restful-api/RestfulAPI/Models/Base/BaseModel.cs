using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPI.Models.Base
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }
        public StatusType Status { get; set; }
    }

    public enum StatusType: short
    {
        Ativo = 1,
        Bloqueado = 2,
        Excluido = 3
    }
}