#region Using

using System;
using System.Linq.Expressions;
using Domain.Base;

#endregion

namespace Domain.Usuarios.Specifications
{
    public sealed class UsuariosPorEmailSpecification : Specification<Usuario>
    {
        private readonly string _email;

        public UsuariosPorEmailSpecification(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            _email = email.ToLower();
        }

        public override Expression<Func<Usuario, bool>> ToExpression()
        {
            return usuario => usuario.Email.ToLower() == _email;
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
            return usuario => usuario.Sexo == _sexo;
        }
    }

    public sealed class UsuariosMaioresDeIdadeSpecification : Specification<Usuario>
    {
        private const int Anos = 18;

        public override Expression<Func<Usuario, bool>> ToExpression()
        {
            return usuario => usuario.DataNascimento <= DateTime.Now.AddYears(-Anos);
        }
    }

    public sealed class UsuariosSearchSpecification : Specification<Usuario>
    {
        private readonly string _search;

        public UsuariosSearchSpecification(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                throw new ArgumentNullException(nameof(search));

            _search = search.ToLower();
        }

        public override Expression<Func<Usuario, bool>> ToExpression()
        {
            return usuario =>
                usuario.Nome.ToLower().Contains(_search) || usuario.Sobrenome == null ||
                usuario.Sobrenome.ToLower().Contains(_search) || usuario.Email == null ||
                usuario.Email.ToLower().Contains(_search);
        }
    }
}