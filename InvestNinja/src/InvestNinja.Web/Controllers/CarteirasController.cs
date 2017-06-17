using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InvestNinja.Core.Data;
using InvestNinja.Data;
using InvestNinja.Core.DTO;
using InvestNinja.Core.Utils;
using System;
using InvestNinja.Core.Tipos;
using InvestNinja.Core.Domain.Carteira;

namespace InvestNinja.Web.Controllers
{
    [Route("api/[controller]")]
    public class CarteirasController : Controller
    {
        IRepository<Carteira> repository;

        public CarteirasController()
        {
            repository = new MongoRepository<Carteira>();
        }

        [HttpGet("payload/teste")]
        public Carteira TesteCarteira()
        {
            Carteira carteira = new Carteira("ITest", "Teste", 100.0, DateTime.Now, 1000.0);
            var list = new List<MovimentacaoCarteira>()
            {
                new MovimentacaoCarteira()
                {
                    Valor = 200.0,
                    Descricao = "Resgate",
                    Tipo = TipoMovimentacao.Resgate
                },
                new MovimentacaoCarteira()
                {
                    Valor = 100.0,
                    Descricao = "Aluguel",
                    Tipo = TipoMovimentacao.Rendimento
                }
            };
            carteira.AddItem(DateTime.Now.AddDays(1), 1000.0, list);
            return carteira;
        }

        [HttpGet]
        public IList<Carteira> GetAll() => repository.GetAll().ToList();

        [HttpGet("grupo/{codigoGrupoCarteira}")]
        public IList<Carteira> GetByGrupoCarteira(string codigoGrupoCarteira)
        {
            IList<Carteira> carteiras = new List<Carteira>();
            GrupoCarteira grupoCarteira = new MongoRepository<GrupoCarteira>().GetById(codigoGrupoCarteira);
            grupoCarteira.Carteiras.ForEach(codigoCarteira => carteiras.Add(repository.GetById(codigoCarteira)));
            return carteiras.ToList();
        }

        [HttpGet("{codigo}")]
        public Carteira Get(string codigo) => repository.GetById(codigo);

        [HttpPost]
        public void Post([FromBody]CarteiraInitializerDTO carteiraInitializer) => repository.Insert(new Carteira(carteiraInitializer.Codigo, carteiraInitializer.Descricao, carteiraInitializer.ValorCotaInicial, carteiraInitializer.DataCota, carteiraInitializer.Saldo));

        [HttpPut("{codigo}/{descricao}")]
        public void Put(string descricao, string codigo)
        {
            Carteira carteira = repository.GetById(codigo);
            carteira.Descricao = descricao;
            repository.Update(carteira);
        }

        [HttpPatch("{codigo}")]
        public void Patch([FromBody]ItemCarteira itemCarteira, string codigo)
        {
            Carteira carteira = repository.GetById(codigo);
            carteira.AddItem(itemCarteira.DataCota, itemCarteira.Saldo, itemCarteira.Movimentacoes);
            repository.Update(carteira);
        }
    }
}
