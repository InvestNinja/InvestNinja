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
        IRepository<Indice> repository;

        public IndicesController()
        {
            repository = new MongoRepository<Indice>();
        }

        [HttpGet]
        public IList<Indice> GetAll() => repository.GetAll().ToList();

        [HttpGet("{codigo}")]
        public Indice Get(string codigo) => repository.GetById(codigo);

        [HttpPost]
        public void Post([FromBody]IndiceInitializer indiceInitializer) => repository.Insert(new Indice(indiceInitializer));

        [HttpPost("{codigo}")]
        public void Post([FromBody]ItemIndice itemIndice, string codigo)
        {
            Indice indice = repository.GetById(codigo);
            indice.AddItem(itemIndice.DataCota, itemIndice.ValorMovimentacao, itemIndice.Saldo);
            repository.Update(indice);
        }
    }
}
