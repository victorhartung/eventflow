using EventFlow.Controllers;
using EventFlow.Data;
using EventFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Reflection.Metadata;


namespace EventFlow.Application.Tests.Controllers
{
    public class EventosControllerTest
    {

        private readonly EventosController _controller;
        private readonly EventFlowContext _context;
        private readonly Endereco endereco;
        private readonly Endereco endereco2;

        public EventosControllerTest() 
        {

            _context = ContextGenerator.Generate(); 
            _controller = new EventosController(_context);
            endereco = new Endereco
            {
                Bairro = "Teste",
                CEP = "27255302",
                Complemento = "",
                Localidade = "Volta Redonda",
                Logradouro = "Rua teste",
                UF = "RJ",
                Id = 1,
                Numero = 1109
            };
            endereco2 = new Endereco
            {
                Bairro = "Teste",
                CEP = "27255302",
                Complemento = "",
                Localidade = "Volta Redonda",
                Logradouro = "Rua teste",
                UF = "RJ",
                Id = 2,
                Numero = 1109
            };
        }


        // Retorna a view quando o evento existe
        [Fact]
        public async Task Details_ReturnsViewResult_WhenEventoExists()
        {
            var organizador = new Organizador { Id = 1, Email = "teste@teste.com", Nome = "Fulano", Telefone = "32323232" };
            _context.Organizadores.Add(organizador);

            var evento = new Evento
            {
                Id = 1,
                Nome = "Evento Teste",
                Endereco = this.endereco,
                Preco = 100,
                PrevisaoClimatica = "Ensolarado",
                Data = DateTime.Now,
                OrganizadorId = 1
            };

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();


            var result = await _controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Evento>(viewResult.Model);

            Assert.Equal("Evento Teste", model.Nome);
        }
        //Retorna tabela de eventos

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfEventos()
        {
            _context.Database.EnsureDeleted();

            var organizador = new Organizador { Id = 1, Email = "teste@teste.com", Nome = "Fulano", Telefone = "32323232"};

            _context.Organizadores.Add(organizador);

            var evento1 = new Evento
            {
                Id = 1,
                Nome = "Evento 1",
                Endereco = this.endereco,
                Preco = 50,
                PrevisaoClimatica = "Ensolarado",
                Data = DateTime.Now,
                OrganizadorId = 1
            };
            _context.Eventos.Add(evento1);


            var evento2 = new Evento
            {
                Id = 2,
                Nome = "Evento 2",
                Endereco = this.endereco2,
                Preco = 70,
                PrevisaoClimatica = "Chuvoso",
                Data = DateTime.Now.AddDays(1),
                OrganizadorId = 1
            };

            _context.Eventos.Add(evento2);

            await _context.SaveChangesAsync();

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<System.Collections.Generic.List<Evento>>(viewResult.Model);

            Assert.Equal(2, model.Count);

        }

        [Fact]
        public async Task Create_ReturnsRedirect_WhenModelIsValid()
        {

            _context.Database.EnsureDeleted();

            var endereco = new Endereco
            {
                Bairro = "Teste",
                CEP = "27255302",
                Complemento = "",
                Localidade = "Volta Redonda",
                Logradouro = "Rua teste",
                UF = "RJ",
                Id = 1,
                Numero = 1109
            };

            var evento = new Evento
            {
                Id = 1,
                Nome = "Novo Evento",
                Endereco = this.endereco,
                Preco = 100,
                PrevisaoClimatica = "Ensolarado",
                Data = DateTime.Now,
                OrganizadorId = 1
            };

            var result = await _controller.Create(evento);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectResult.ActionName);
        }   

        //Retorna not found quando evento é null

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        //retorna erro de input inválido

        [Fact]
        public async Task Create_ReturnsView_WhenModelIsInvalid()
        {

            _context.Database.EnsureDeleted();

            var evento = new Evento
            {
                Id = 2,
                Nome = "",
                Endereco = this.endereco,
                Preco = 100,
                PrevisaoClimatica = "Ensolarado",
                Data = DateTime.Now,
                OrganizadorId = 1
            };

            _controller.ModelState.AddModelError("Nome", "O nome é obrigatório.");
          
            var result = await _controller.Create(evento);
            
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(evento, viewResult.Model);
        }

        //retorna not found quando evento não existe

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenEventoDoesNotExist()
        {
            var result = await _controller.Edit(999); 
            Assert.IsType<NotFoundResult>(result);
        }

        //retorna not found quando tenta deletar um evento que não existe
        [Fact]
        public async Task Delete_ReturnsNotFound_WhenEventoDoesNotExist()
        {
            var result = await _controller.Delete(999); 
            Assert.IsType<NotFoundResult>(result);
        }

        //Retorna view quando evento é excluído com sucessp

        [Fact]
        public async Task DeleteConfirmed_RemovesEvento_WhenEventoExists()
        {
            _context.Database.EnsureDeleted();

            var evento = new Evento
            {
                Id = 1,
                Nome = "Evento para deletar",
                Endereco = this.endereco,
                Preco = 100,
                PrevisaoClimatica = "Ensolarado",
                Data = DateTime.Now,
                OrganizadorId = 1
            };

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteConfirmed(1);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(await _context.Eventos.FindAsync(1)); 
        }

    }
}
