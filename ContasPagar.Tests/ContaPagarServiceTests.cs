using ContasPagar.Api.Data;
using ContasPagar.Api.Domain.Entities;
using ContasPagar.Api.DTOs;
using ContasPagar.Api.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace ContasPagar.Api.Tests
{
    public class ContaPagarServiceTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public void CriarContaPagar_QuandoPagaEmDia_DeveRetornarSemJuros()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new ContaPagarService(context);
            var dto = new CriarContaPagarDto { Nome = "Teste", ValorOriginal = 100, DataVencimento = DateTime.Now, DataPagamento = DateTime.Now };

            // Act
            var resultado = service.CriarContaPagar(dto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(0, resultado.QuantidadeDiasAtraso);
            Assert.Equal(100, resultado.ValorCorrigido);
        }

        [Fact]
        public void CriarContaPagar_QuandoPagaCom2DiasAtraso_DeveCalcularJurosCorretamente()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new ContaPagarService(context);
            var dto = new CriarContaPagarDto { Nome = "Teste", ValorOriginal = 100, DataVencimento = DateTime.Now.AddDays(-2), DataPagamento = DateTime.Now };

            // Act
            var resultado = service.CriarContaPagar(dto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.QuantidadeDiasAtraso);
            Assert.Equal(102.20m, resultado.ValorCorrigido);
        }

        [Fact]
        public void CriarContaPagar_QuandoPagaCom10DiasAtraso_DeveCalcularJurosCorretamente()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new ContaPagarService(context);
            var dto = new CriarContaPagarDto { Nome = "Teste", ValorOriginal = 100, DataVencimento = DateTime.Now.AddDays(-10), DataPagamento = DateTime.Now };

            // Act
            var resultado = service.CriarContaPagar(dto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(10, resultado.QuantidadeDiasAtraso);
            Assert.Equal(108.00m, resultado.ValorCorrigido);
        }

        [Fact]
        public void ObterContaPagarPorId_QuandoIdExiste_DeveRetornarConta()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var contaExistente = new ContaPagar { Id = 1, Nome = "Conta Existente", ValorOriginal = 50, DataVencimento = DateTime.Now, DataPagamento = DateTime.Now };
            context.ContasPagar.Add(contaExistente);
            context.SaveChanges();
            var service = new ContaPagarService(context);

            // Act
            var resultado = service.ObterContaPagarPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal("Conta Existente", resultado.Nome);
        }

        [Fact]
        public void ObterContaPagarPorId_QuandoIdNaoExiste_DeveRetornarNulo()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new ContaPagarService(context);

            // Act
            var resultado = service.ObterContaPagarPorId(99);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public void ListarContasPagar_QuandoExistemContas_DeveRetornarListaComTodosOsItens()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.ContasPagar.Add(new ContaPagar { Nome = "Conta 1" });
            context.ContasPagar.Add(new ContaPagar { Nome = "Conta 2" });
            context.SaveChanges();
            var service = new ContaPagarService(context);

            // Act
            var resultado = service.ListarContasPagar();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public void AtualizarContaPagar_QuandoIdExiste_DeveAtualizarErecalcularJuros()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var dataDeReferencia = new DateTime(2025, 10, 1);

            var contaOriginal = new ContaPagar
            {
                Id = 1,
                Nome = "Original",
                ValorOriginal = 100,
                DataVencimento = dataDeReferencia,
                DataPagamento = dataDeReferencia
            };

            context.ContasPagar.Add(contaOriginal);
            context.SaveChanges();
            var service = new ContaPagarService(context);

            var dtoAtualizacao = new AtualizarContaPagarDto
            {
                Id = 1,
                Nome = "Atualizado",
                ValorOriginal = 200,
                DataVencimento = dataDeReferencia.AddDays(-10)
            };

            // Act
            service.AtualizarContaPagar(dtoAtualizacao);
            var resultadoDoBanco = context.ContasPagar.Find(1);

            // Assert
            Assert.NotNull(resultadoDoBanco);
            Assert.Equal("Atualizado", resultadoDoBanco.Nome);
            Assert.Equal(200, resultadoDoBanco.ValorOriginal);
            Assert.Equal(10, resultadoDoBanco.QuantidadeDiasAtraso);
            Assert.Equal(216.00m, resultadoDoBanco.ValorCorrigido);
        }

        [Fact]
        public void ExcluirContaPagar_QuandoIdExiste_DeveRemoverDoBanco()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.ContasPagar.Add(new ContaPagar { Id = 1, Nome = "Para Excluir" });
            context.SaveChanges();
            Assert.Equal(1, context.ContasPagar.Count());
            var service = new ContaPagarService(context);

            // Act
            service.ExcluirContaPagar(1);
            var totalNoBanco = context.ContasPagar.Count();

            // Assert
            Assert.Equal(0, totalNoBanco);
        }
    }
}