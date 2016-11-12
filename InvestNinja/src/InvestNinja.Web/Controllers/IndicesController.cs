using InvestNinja.Core.Data;
using InvestNinja.Core.Domain;
using InvestNinja.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Web.Controllers
{
    [Route("api/[controller]")]
    public class IndicesController : Controller
    {
        [HttpGet]
        public IList<Indice> GetAll()
        {
            IRepository<Indice> repository = new MongoRepository<Indice>();
            return repository.GetAll().ToList();
        }

        [HttpGet("{codigo}")]
        public Indice Get(string codigo)
        {
            IRepository<Indice> repository = new MongoRepository<Indice>();
            return repository.GetById(codigo);
        }

        [HttpPost]
        public void Post([FromBody]IndiceInitializer indiceInitializer)
        {
            Indice indice = new Indice(indiceInitializer);
            IRepository<Indice> repository = new MongoRepository<Indice>();
            repository.Insert(indice);
        }

        [HttpPost("{codigo}")]
        public void Post([FromBody]ItemIndice itemIndice, string codigo)
        {
            IRepository<Indice> repository = new MongoRepository<Indice>();
            Indice indice = repository.GetById(codigo);
            indice.AddItem(itemIndice.DataCota, itemIndice.ValorMovimentacao, itemIndice.Saldo);
            repository.Update(indice);
        }
    }
}
