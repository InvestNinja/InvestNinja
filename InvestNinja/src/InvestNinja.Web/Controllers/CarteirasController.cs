using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InvestNinja.Core.Domain;
using InvestNinja.Core.Data;
using InvestNinja.Data;
using InvestNinja.Core.DTO;

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

        [HttpGet("{codigo}")]
        public Carteira Get(string codigo) => repository.GetById(codigo);

        [HttpPost]
        public void Post([FromBody]CarteiraInitializerDTO carteiraInitializer) => repository.Insert(new Carteira(carteiraInitializer.Codigo, carteiraInitializer.Descricao, carteiraInitializer.ValorCotaInicial, carteiraInitializer.DataCota, carteiraInitializer.Saldo));

        [HttpPost("{codigo}")]
        public void Post([FromBody]ItemCarteira itemCarteira, string codigo)
        {
            Carteira carteira = repository.GetById(codigo);
            carteira.AddItem(itemCarteira.DataCota, itemCarteira.Saldo, itemCarteira.ValorMovimentacao);
            repository.Update(carteira);
        }
    }
}
