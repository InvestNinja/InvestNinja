using InvestNinja.Core.Domain;
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
            //buscar Índice pelo Código
            return new List<Indice>() { new Indice("INinja", "Ninja", 10.0, DateTime.Now, 1000.00, 1000.00),
                new Indice("IBOV", "Bovespa", 100.0, DateTime.Now, 100.00, 100.00)};
        }

        [HttpGet("{codigo}")]
        public Indice Get()
        {
            //buscar Índice pelo Código
            return new Indice("INinja", "Ninja", 10.0, DateTime.Now, 1000.00, 1000.00);
        }

        [HttpPost]
        public void Post([FromBody]Indice indice)
        {
            //salvar o Índice
            indice.ToString();
        }

        [HttpPost("{codigo}")]
        public void Post([FromBody]ItemIndice itemIndice)
        {
            //Trocar linha abaixo para buscar o Indice pelo Código
            Indice indice = new Indice("INinja", "Ninja", 10.0, DateTime.Now, 1000.00, 1000.00);
            //Inclusão de um novo item ao Índice
            indice.AddItem(itemIndice.DataCota, itemIndice.ValorMovimentacao, itemIndice.Saldo);
        }
    }
}
