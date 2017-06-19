using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InvestNinja.Core.Data;
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
        IRepository<Carteira> repositoryCarteira;
        IRepository<GrupoCarteira> repositoryGrupoCarteira;

        public CarteirasController(IRepository<Carteira> repositoryCarteira, IRepository<GrupoCarteira> repositoryGrupoCarteira)
        {
            this.repositoryCarteira = repositoryCarteira;
            this.repositoryGrupoCarteira = repositoryGrupoCarteira;
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
        public IList<Carteira> GetAll() => repositoryCarteira.GetAll().ToList();

        [HttpGet("grupo/{codigoGrupoCarteira}")]
        public IList<Carteira> GetByGrupoCarteira(string codigoGrupoCarteira)
        {
            IList<Carteira> carteiras = new List<Carteira>();
            GrupoCarteira grupoCarteira = repositoryGrupoCarteira.GetById(codigoGrupoCarteira);
            grupoCarteira.Carteiras.ForEach(codigoCarteira => carteiras.Add(repositoryCarteira.GetById(codigoCarteira)));
            return carteiras.ToList();
        }

        [HttpGet("{codigo}")]
        public Carteira Get(string codigo) => repositoryCarteira.GetById(codigo);

        [HttpPost]
        public void Post([FromBody]CarteiraInitializerDTO carteiraInitializer) => repositoryCarteira.Insert(new Carteira(carteiraInitializer.Codigo, carteiraInitializer.Descricao, carteiraInitializer.ValorCotaInicial, carteiraInitializer.DataCota, carteiraInitializer.Saldo));

        [HttpPut("{codigo}/{descricao}")]
        public void Put(string descricao, string codigo)
        {
            Carteira carteira = repositoryCarteira.GetById(codigo);
            carteira.Descricao = descricao;
            repositoryCarteira.Update(carteira);
        }

        [HttpPatch("{codigo}")]
        public void Patch([FromBody]ItemCarteira itemCarteira, string codigo)
        {
            Carteira carteira = repositoryCarteira.GetById(codigo);
            carteira.AddItem(itemCarteira.DataCota, itemCarteira.Saldo, itemCarteira.Movimentacoes);
            repositoryCarteira.Update(carteira);
        }

        [HttpDelete("{codigo}")]
        public void Delete(string codigo) => repositoryCarteira.Delete(repositoryCarteira.GetById(codigo));
    }
}
