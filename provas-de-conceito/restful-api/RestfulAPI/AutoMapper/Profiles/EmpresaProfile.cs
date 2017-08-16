using AutoMapper;
using Domain.Empresas;
using RestfulAPI.Models.Empresa;

namespace RestfulAPI.AutoMapper.Profiles
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            // Model to Entity
            CreateMap<EmpresaModel, Empresa>();

            // Entity to Model
            CreateMap<Empresa, EmpresaModel>();
            CreateMap<Empresa, EmpresaItemModel>();
        }
    }
}