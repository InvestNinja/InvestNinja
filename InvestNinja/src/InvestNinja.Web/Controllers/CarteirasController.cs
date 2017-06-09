using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InvestNinja.Core.Domain;
using InvestNinja.Core.Data;
using InvestNinja.Data;
using InvestNinja.Core.DTO;
using InvestNinja.Core.Utils;

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

        [HttpGet]
        public IList<Carteira> GetAll() => repository.GetAll().ToList();

        [HttpGet("/grupo/{codigoGrupoCarteira}")]
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

        [HttpPatch("{codigo}")]
        public void Patch([FromBody]ItemCarteira itemCarteira, string codigo)
        {
            Carteira carteira = repository.GetById(codigo);
            carteira.AddItem(itemCarteira.DataCota, itemCarteira.Saldo, itemCarteira.Movimentacoes);
            repository.Update(carteira);
        }
    }
}
