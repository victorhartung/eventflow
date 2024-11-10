using EventFlow.Services.Response;
using Refit;

namespace EventFlow.Services.Refit
{
    public interface IViaCepRefit
    {
        [Get("/ws/{cep}/json")]
        Task<ApiResponse<ViaCepResponse>> GetViaCep(string cep);
    }
}
