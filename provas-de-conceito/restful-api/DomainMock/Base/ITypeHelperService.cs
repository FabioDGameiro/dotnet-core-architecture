using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
