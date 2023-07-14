using System.Net.Http;
using System.Threading.Tasks;

namespace CobotADTInitializeFunctionApp.Interface
{
    public interface IAdtModelCreator
    {
        Task<HttpResponseMessage> CreateAsync(string id);
    }
}
