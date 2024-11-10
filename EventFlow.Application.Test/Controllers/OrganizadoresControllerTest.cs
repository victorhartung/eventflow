using EventFlow.Controllers;
using EventFlow.Data;
using EventFlow.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EventFlow.Application.Tests.Controllers
{
    public class OrganizadoresControllerTest
    {
        private readonly OrganizadoresController _controller;
        private readonly EventFlowContext _context;


        public OrganizadoresControllerTest()
        {

            _context = ContextGenerator.Generate();
            _controller = new OrganizadoresController(_context);

        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfOrganizadores()
        {
           
            _context.Organizadores.Add(new Organizador { Id = 1, Nome = "Organizador Teste", Email = "teste@teste.com", Telefone = "123456789" });
            await _context.SaveChangesAsync();

            var result = await _controller.Index();
          
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<System.Collections.Generic.List<Organizador>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Create_ReturnsRedirectToAction_WhenModelIsValid()
        {
            // Arrange
            var organizador = new Organizador
            {
                Nome = "Organizador Teste",
                Email = "teste@teste.com",
                Telefone = "123456789"
            };

          
            var result = await _controller.Create(organizador);

            
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenOrganizadorDoesNotExist()
        {
       
            var result = await _controller.Edit(999);

            
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WhenOrganizadorExists()
        {
         
            var organizador = new Organizador { Id = 1, Nome = "Organizador Teste", Email = "teste@teste.com", Telefone = "123456789" };
            _context.Organizadores.Add(organizador);
            await _context.SaveChangesAsync();
        
            var result = await _controller.Edit(1);
  
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Organizador>(viewResult.Model);
            Assert.Equal(organizador.Id, model.Id);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenOrganizadorDoesNotExist()
        {
         
            var result = await _controller.Delete(999);
          
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesOrganizador_WhenOrganizadorExists()
        {
         
            var organizador = new Organizador { Id = 1, Nome = "Organizador Teste", Email = "teste@teste.com", Telefone = "123456789" };
            _context.Organizadores.Add(organizador);
            await _context.SaveChangesAsync();

       
            var result = await _controller.DeleteConfirmed(organizador.Id);
   
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            var deletedOrganizador = await _context.Organizadores.FindAsync(organizador.Id);
            Assert.Null(deletedOrganizador);
        }
    }
}
