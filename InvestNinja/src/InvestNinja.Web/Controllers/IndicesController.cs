﻿using InvestNinja.Core.Data;
using InvestNinja.Core.Domain;
using InvestNinja.Core.DTO;
using InvestNinja.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
        public void Post([FromBody]IndiceInitializerDTO indiceInitializer) => repository.Insert(new Indice(indiceInitializer.Codigo, indiceInitializer.Descricao, indiceInitializer.ValorCotaInicial, indiceInitializer.DataCota));

        [HttpPost("{codigo}")]
        public void Post([FromBody]ItemIndice itemIndice, string codigo)
        {
            Indice indice = repository.GetById(codigo);
            indice.AddItemByValorCota(itemIndice.DataCota, itemIndice.ValorCota);
            repository.Update(indice);
        }
    }
}
