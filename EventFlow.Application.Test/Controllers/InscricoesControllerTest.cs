using EventFlow.Controllers;
using EventFlow.Data;
using EventFlow.Enums;
using EventFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EventFlow.Application.Tests.Controllers
{
    public class InscricoesControllerTest
    {

        private readonly InscricoesController _controller;
        private readonly EventFlowContext _context;
        public InscricoesControllerTest()
        {
            _context = ContextGenerator.Generate();
            _controller = new InscricoesController(_context);

        }
        
        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfInscricoes()
        {
           
            var evento = new Evento {
                Id = 1,
                Nome = "Evento Teste",
                Local = "Local Teste",
                Preco = 100,
                PrevisaoClimatica = "Ensolarado",
                Data = DateTime.Now,
                OrganizadorId = 1
            
            };

            var participante = new Participante
            {
                Id = 1,
                Nome = "Fulano",
                Email = "teste@teste.com",
                Telefone = "32323232"
            };

            _context.Eventos.Add(evento);
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            _context.Inscricoes.Add(new Inscricao
            {
                EventoId = 1,
                ParticipanteId = 1,
                DataInscricao = DateTime.Now,
                StatusPagamento = StatusPagamento.Pendente,
                MetodoPagamento = MetodoPagamento.PIX
            });
            await _context.SaveChangesAsync();

         
            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<System.Collections.Generic.List<Inscricao>>(viewResult.Model);
            Assert.Single(model);
           
        }

        [Fact]
        public async Task Create_ReturnsRedirectToAction_WhenModelIsValid()
        {
            
            var evento = new Evento
            {
                Id = 1,
                Nome = "Evento Teste",
                Local = "Local Teste",
                Preco = 100,
                PrevisaoClimatica = "Ensolarado",
                Data = DateTime.Now,
                OrganizadorId = 1
            };

            var participante = new Participante
            {
                Id = 1,
                Nome = "Fulano",
                Email = "teste@teste.com",
                Telefone = "32323232"
            };

            _context.Eventos.Add(evento);
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            var inscricao = new Inscricao
            {
                EventoId = 1,
                ParticipanteId = 1,
                DataInscricao = DateTime.Now,
                StatusPagamento = StatusPagamento.Pendente,
                MetodoPagamento = MetodoPagamento.PIX
            };

       
            var result = await _controller.Create(inscricao);

            
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenInscricaoDoesNotExist()
        {
  
            var result = await _controller.Edit(999);

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task Delete_ReturnsNotFound_WhenInscricaoDoesNotExist()
        {
     
            var result = await _controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesInscricao_WhenInscricaoExists()
        {
       
            var evento = new Evento
            {
                Id = 1,
                Nome = "Evento Teste",
                Local = "Local Teste",
                Preco = 100,
                PrevisaoClimatica = "Ensolarado",
                Data = DateTime.Now,
                OrganizadorId = 1
            };
            
            var participante = new Participante 
            { 
                Id = 1, 
                Nome = "Fulano", 
                Email = "teste@teste.com", 
                Telefone = "32323232" 
            };

            _context.Eventos.Add(evento);
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            var inscricao = new Inscricao
            {
                EventoId = 1,
                ParticipanteId = 1,
                DataInscricao = DateTime.Now,
                StatusPagamento = StatusPagamento.Pendente,
                MetodoPagamento = MetodoPagamento.PIX
            };

            _context.Inscricoes.Add(inscricao);
            await _context.SaveChangesAsync();

       
            var result = await _controller.DeleteConfirmed(inscricao.Id);

          
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            var deletedInscricao = await _context.Inscricoes.FindAsync(inscricao.Id);
            Assert.Null(deletedInscricao);
        }

    }
}
