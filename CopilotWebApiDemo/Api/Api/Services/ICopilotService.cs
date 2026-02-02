using System.Threading;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface ICopilotService
    {
        Task<string> CompleteAsync(string prompt, CancellationToken ct = default);
    }
}