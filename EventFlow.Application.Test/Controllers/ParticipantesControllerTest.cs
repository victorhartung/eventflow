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
    public class ParticipantesControllerTest
    {

        private readonly ParticipantesController _controller;
        private readonly EventFlowContext _context;

        public ParticipantesControllerTest()
        {
            _context = ContextGenerator.Generate();
            _controller = new ParticipantesController(_context);
        }


        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfParticipantes()
        {
            
            _context.Participantes.Add(new Participante { Id = 1, Nome = "Participante Teste", Email = "teste@teste.com", Telefone = "123456789", DataNascimento = DateOnly.MaxValue });
            await _context.SaveChangesAsync();

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<System.Collections.Generic.List<Participante>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Create_ReturnsRedirectToAction_WhenModelIsValid()
        {

            var participante = new Participante
            {
                Nome = "Participante Teste",
                Email = "teste@teste.com",
                Telefone = "123456789",
                DataNascimento = DateOnly.MaxValue
            };
        
            var result = await _controller.Create(participante);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenParticipanteDoesNotExist()
        {
          
            var result = await _controller.Edit(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WhenParticipanteExists()
        {
         
            var participante = new Participante { Id = 1, Nome = "Participante Teste", Email = "teste@teste.com", Telefone = "123456789", DataNascimento = DateOnly.MaxValue };
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

      
            var result = await _controller.Edit(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Participante>(viewResult.Model);
            Assert.Equal(participante.Id, model.Id);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenParticipanteDoesNotExist()
        {

            var result = await _controller.Delete(999);
        
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesParticipante_WhenParticipanteExists()
        {
      
            var participante = new Participante { Id = 1, Nome = "Participante Teste", Email = "teste@teste.com", Telefone = "123456789", DataNascimento = DateOnly.MaxValue };
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();


            var result = await _controller.DeleteConfirmed(participante.Id);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            var deletedParticipante = await _context.Participantes.FindAsync(participante.Id);
            Assert.Null(deletedParticipante);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenParticipanteDoesNotExist()
        {
          
            var result = await _controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WhenParticipanteExists()
        {

            var participante = new Participante { Id = 1, Nome = "Participante Teste", Email = "teste@teste.com", Telefone = "123456789", DataNascimento = DateOnly.MaxValue };
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            var result = await _controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Participante>(viewResult.Model);
            Assert.Equal(participante.Id, model.Id);
        }
    
    }
}
