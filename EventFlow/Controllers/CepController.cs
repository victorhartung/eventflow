using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EventFlow.Services.Response;
using EventFlow.Services.Interfaces;

namespace EventFlow.Controllers
{

    [Microsoft.AspNetCore.Components.Route("/api/[controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private readonly IViaCepIntegration _viaCepIntegration;

        public CepController(IViaCepIntegration viaCepIntegration)
        {
            _viaCepIntegration = viaCepIntegration;
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<ViaCepResponse>> ListarDadosCep(string cep)
        {
            var responseData = await _viaCepIntegration.GetViaCepData(cep);

            if (responseData == null) {

                return BadRequest("Cep não encontrado");
            }

            return Ok(responseData);


        }
    }
}
