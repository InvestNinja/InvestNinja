using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvestNinja.Core.Domain;
using InvestNinja.Core.Data;
using InvestNinja.Data;

namespace InvestNinja.Web.Controllers
{
    [Route("api/[controller]")]
    public class CarteiraController : Controller
    {
        IRepository<Carteira> repository;

        public CarteiraController()
        {
            repository = new MongoRepository<Carteira>();
        }

        [HttpGet]
        public IList<Carteira> GetAll() => repository.GetAll().ToList();

        [HttpGet("{codigo}")]
        public Carteira Get(string codigo) => repository.GetById(codigo);

        [HttpPost]
        public void Post([FromBody]CarteiraInitializer carteiraInitializer) => repository.Insert(new Carteira(carteiraInitializer));

        [HttpPost("{codigo}")]
        public void Post([FromBody]ItemCarteira itemCarteira, string codigo)
        {
            Carteira carteira = repository.GetById(codigo);
            carteira.AddItem(itemCarteira.DataCota, itemCarteira.ValorMovimentacao, itemCarteira.Saldo);
            repository.Update(carteira);
        }
    }
}
