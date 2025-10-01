using ContasPagar.Api.Data;
using ContasPagar.Api.Domain.Entities;
using ContasPagar.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContasPagar.Api.Services
{
    public class ContaPagarService : IContaPagarService
    {
        private readonly AppDbContext _context;

        public ContaPagarService(AppDbContext context)
        {
            _context = context;
        }

        public ContaPagarDto CriarContaPagar(CriarContaPagarDto contaPagarDto)
        {
            var contaPagar = new ContaPagar
            {
                Nome = contaPagarDto.Nome,
                ValorOriginal = contaPagarDto.ValorOriginal,
                DataVencimento = contaPagarDto.DataVencimento,
                DataPagamento = contaPagarDto.DataPagamento
            };

            CalcularRegrasAtraso(contaPagar);

            _context.ContasPagar.Add(contaPagar);
            _context.SaveChanges();

            return MapearEntidadeParaDto(contaPagar);
        }

        public ContaPagarDto ObterContaPagarPorId(int id)
        {
            var conta = _context.ContasPagar.Find(id);
            
            return conta != null ? MapearEntidadeParaDto(conta) : null;
        }

        public IEnumerable<ContaPagarDto> ListarContasPagar()
        {
            return _context.ContasPagar
                .ToList()
                .Select(MapearEntidadeParaDto);
        }

        public ContaPagarDto AtualizarContaPagar(AtualizarContaPagarDto atualizarDto)
        {
            var contaExistente = _context.ContasPagar.Find(atualizarDto.Id);
            if (contaExistente == null)
            {
                return null;
            }

            contaExistente.Nome = atualizarDto.Nome;
            contaExistente.ValorOriginal = atualizarDto.ValorOriginal;
            contaExistente.DataVencimento = atualizarDto.DataVencimento;

            CalcularRegrasAtraso(contaExistente);

            _context.SaveChanges();

            return MapearEntidadeParaDto(contaExistente);
        }

        public ContaPagarDto ExcluirContaPagar(int id)
        {
            var contaExistente = _context.ContasPagar.Find(id);
            
            if (contaExistente == null)
            {
                return null;
            }

            var dtoDeRetorno = MapearEntidadeParaDto(contaExistente);

            _context.ContasPagar.Remove(contaExistente);
            _context.SaveChanges();

            return dtoDeRetorno;
        }

        private void CalcularRegrasAtraso(ContaPagar contaPagar)
        {
            int diasAtraso = (contaPagar.DataPagamento - contaPagar.DataVencimento).Days;

            if (diasAtraso < 0)
            {
                diasAtraso = 0;
            }

            contaPagar.QuantidadeDiasAtraso = diasAtraso;

            if (diasAtraso == 0)
            {
                contaPagar.PercentualMultaAplicado = 0;
                contaPagar.PercentualJurosDiaAplicado = 0;
                contaPagar.ValorCorrigido = contaPagar.ValorOriginal;
                
                return;
            }

            decimal percentualMulta;
            decimal percentualJurosDia;

            if (diasAtraso <= 3)
            {
                percentualMulta = 2.0m;
                percentualJurosDia = 0.1m;
            }
            else if (diasAtraso <= 5)
            {
                percentualMulta = 3.0m;
                percentualJurosDia = 0.2m;
            }
            else
            {
                percentualMulta = 5.0m;
                percentualJurosDia = 0.3m;
            }

            contaPagar.PercentualMultaAplicado = percentualMulta;
            contaPagar.PercentualJurosDiaAplicado = percentualJurosDia;

            decimal valorDaMulta = contaPagar.ValorOriginal * (percentualMulta / 100);
            decimal valorDosJuros = contaPagar.ValorOriginal * (percentualJurosDia / 100) * diasAtraso;
            decimal valorFinalCorrigido = contaPagar.ValorOriginal + valorDaMulta + valorDosJuros;

            contaPagar.ValorCorrigido = Math.Round(valorFinalCorrigido, 2);
        }

        private ContaPagarDto MapearEntidadeParaDto(ContaPagar contaPagar)
        {
            var contaPagarDto = new ContaPagarDto
            {
                Id = contaPagar.Id,
                Nome = contaPagar.Nome,
                ValorOriginal = contaPagar.ValorOriginal,
                ValorCorrigido = contaPagar.ValorCorrigido,
                QuantidadeDiasAtraso = contaPagar.QuantidadeDiasAtraso,
                DataPagamento = contaPagar.DataPagamento
            };

            return contaPagarDto;
        }
    }
}