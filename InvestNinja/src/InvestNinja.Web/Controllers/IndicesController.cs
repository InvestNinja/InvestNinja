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
        public void Post([FromBody]Indice indiceDTO)
        {
            Indice indice = new Indice(indiceDTO.Codigo, indiceDTO.Descricao, indiceDTO.ValorCotaInicial, DateTime.Now, 1000.00, 1000.00);
            IRepository<Indice> repository = new MongoRepository<Indice>();
            repository.Insert(indice);
        }

        [HttpPost("{codigo}")]
        public void Post([FromBody]ItemIndice itemIndice, string codigo)
        {
            //Trocar linha abaixo para buscar o Indice pelo Código
            Indice indice = new Indice("INinja", "Ninja", 10.0, DateTime.Now, 1000.00, 1000.00);
            //Inclusão de um novo item ao Índice
            indice.AddItem(itemIndice.DataCota, itemIndice.ValorMovimentacao, itemIndice.Saldo);
        }
    }
}
