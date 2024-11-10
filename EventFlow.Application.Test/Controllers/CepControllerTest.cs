using EventFlow.Controllers;
using EventFlow.Services.Interfaces;
using EventFlow.Services.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace EventFlow.Application.Test.Controllers
{
    public class CepControllerTest
    {

        private readonly Mock<IViaCepIntegration> _viaCepIntegrationMock;
        private readonly CepController _cepController;

        public CepControllerTest()
        {
            _viaCepIntegrationMock = new Mock<IViaCepIntegration>();
            _cepController = new CepController(_viaCepIntegrationMock.Object);
        }


        [Fact]
        public async Task ListarDadosCep_ReturnsOk_WhenCepDataExists()
        {

            var cep = "12345678";
            var expectedResponse = new ViaCepResponse { Cep = cep, Logradouro = "Rua Exemplo", Bairro = "Centro" };
            _viaCepIntegrationMock.Setup(x => x.GetViaCepData(cep)).ReturnsAsync(expectedResponse);

            var result = await _cepController.ListarDadosCep(cep);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task ListarDadosCep_ReturnsBadRequest_WhenCepDataIsNull()
        {
 
            var cep = "00000000";
            _viaCepIntegrationMock.Setup(x => x.GetViaCepData(cep)).ReturnsAsync((ViaCepResponse?)null);
    
            var result = await _cepController.ListarDadosCep(cep);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal("Cep não encontrado", badRequestResult.Value);
        }
    }

}
