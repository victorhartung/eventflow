using EventFlow.Services.Interfaces;
using EventFlow.Services.Refit;
using EventFlow.Services.Response;

namespace EventFlow.Services
{
    public class ViaCepIntegration : IViaCepIntegration
    {
        private readonly IViaCepRefit _refit;

        public ViaCepIntegration(IViaCepRefit viaCepRefit)
        {
            _refit = viaCepRefit;
        }

        public async Task<ViaCepResponse> GetViaCepData(string cep)
        {

            var responseData = await _refit.GetViaCep(cep);

            if (responseData != null && responseData.IsSuccessStatusCode)
            {
                return responseData.Content;
            }

            return null;
        }
    }
}
