using EventFlow.Services.Response;

namespace EventFlow.Services.Interfaces
{
    public interface IViaCepIntegration
    {

        Task<ViaCepResponse> GetViaCepData(string cep);

    }
}
