namespace Domain.Base
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}