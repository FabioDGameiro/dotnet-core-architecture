using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Usuarios.Specifications
{
    // TODO 
    //public sealed class MaiorDeIdadeSpecification : Specification<Usuario>
    //{
    //    public override Expression<Func<Usuario, bool>> ToExpression()
    //    {
    //        return Usuario => Usuario.DataNascimento.Value.age <= MpaaRating.PG;
    //    }
    //}

    //public sealed class AvailableOnCDSpecification : Specification<Usuario>
    //{
    //    private const int MonthsBeforeDVDIsOut = 6;

    //    public override Expression<Func<Usuario, bool>> ToExpression()
    //    {
    //        return Usuario => Usuario.ReleaseDate <= DateTime.Now.AddMonths(-MonthsBeforeDVDIsOut);
    //    }
    //}

    //public sealed class UsuarioDirectedBySpecification : Specification<Usuario>
    //{
    //    private readonly string _director;

    //    public UsuarioDirectedBySpecification(string director)
    //    {
    //        _director = director;
    //    }

    //    public override Expression<Func<Usuario, bool>> ToExpression()
    //    {
    //        return Usuario => Usuario.Director.Name == _director;
    //    }
    //}
}
