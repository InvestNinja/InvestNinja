﻿using InvestNinja.Core.Data;
using InvestNinja.Core.Domain;
using InvestNinja.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestNinja.Web.Controllers
{
    [Route("api/[controller]")]
    public class IndicesController : Controller
    {
        IRepository<Indice> repository;

        public IndicesController(IRepository<Indice> repository)
        {
            this.repository = repository;
        }

        [HttpGet("payload/teste")]
        public Indice TesteCarteira()
        {
            Indice indice = new Indice("ITest", "Teste", 100.0, DateTime.Now);
            indice.AddItemByValorCota(DateTime.Now.AddDays(1), 110.0);
            return indice;
        }

        [HttpGet]
        public IList<Indice> GetAll() => repository.GetAll().ToList();

        [HttpGet("{codigo}")]
        public Indice Get(string codigo) => repository.GetById(codigo);

        [HttpPost]
        public void Post([FromBody]IndiceInitializerDTO indiceInitializer) => repository.Insert(new Indice(indiceInitializer.Codigo, indiceInitializer.Descricao, indiceInitializer.ValorCotaInicial, indiceInitializer.DataCota));

        [HttpPut("{codigo}/{descricao}")]
        public void Put(string descricao, string codigo)
        {
            Indice indice = repository.GetById(codigo);
            indice.Descricao = descricao;
            repository.Update(indice);
        }

        [HttpPatch("{codigo}")]
        public void Patch([FromBody]ItemIndice itemIndice, string codigo)
        {
            Indice indice = repository.GetById(codigo);
            if (itemIndice.ValorCota >= 0.0)
                indice.AddItemByValorCota(itemIndice.DataCota, itemIndice.ValorCota);
            else
                indice.AddItemByVariacaoCota(itemIndice.DataCota, itemIndice.VariacaoCotaPercentual);
            repository.Update(indice);
        }

        [HttpDelete("{codigo}")]
        public void Delete(string codigo) => repository.Delete(repository.GetById(codigo));
    }
}
