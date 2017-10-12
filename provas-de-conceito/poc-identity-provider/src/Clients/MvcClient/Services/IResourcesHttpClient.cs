using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcClient.Services
{
    public interface IResourcesHttpClient
    {
        Task<HttpClient> GetClient();
    }
}
