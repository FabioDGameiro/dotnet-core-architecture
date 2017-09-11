using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Usuarios.Specifications
{
    public sealed class UsuariosPorEmailSpecification : Specification<Usuario>
    {
        private readonly string _email;

        public UsuariosPorEmailSpecification(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            _email = email.ToLower();
        }

        public override Expression<Func<Usuario, bool>> ToExpression()
        {
            return Usuario => Usuario.Email.ToLower() == _email;
        }
    }

    public sealed class UsuariosPorSexoSpecification : Specification<Usuario>
    {
        private readonly SexoType _sexo;

        public UsuariosPorSexoSpecification(SexoType sexo)
        {
            _sexo = sexo;
        }

        public override Expression<Func<Usuario, bool>> ToExpression()
        {
            return Usuario => Usuario.Sexo == _sexo;
        }
    }

    public sealed class UsuariosMaioresDeIdadeSpecification : Specification<Usuario>
    {
        private const int Anos = 18;

        public override Expression<Func<Usuario, bool>> ToExpression()
        {
            return Usuario => Usuario.DataNascimento <= DateTime.Now.AddYears(-Anos);
        }
    }

    public sealed class UsuariosSearchSpecification : Specification<Usuario>
    {
        private readonly string _search;

        public UsuariosSearchSpecification(string search)
        {
            if (String.IsNullOrWhiteSpace(search))
                throw new ArgumentNullException(nameof(search));

            _search = search.ToLower();
        }

        public override Expression<Func<Usuario, bool>> ToExpression()
        {
            return Usuario =>
                Usuario.Nome.ToLower().Contains(_search) ||
                (Usuario.Sobrenome == null || Usuario.Sobrenome.ToLower().Contains(_search)) ||
                (Usuario.Email == null || Usuario.Email.ToLower().Contains(_search));
        }
    }
}
