﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InvestNinja.Core.Data;
using InvestNinja.Core.Domain.Carteira;

namespace InvestNinja.Web.Controllers
{
    [Route("api/[controller]")]
    public class GruposCarteiraController : Controller
    {
        IRepository<GrupoCarteira> repository;

        public GruposCarteiraController(IRepository<GrupoCarteira> repository)
        {
            this.repository = repository;
        }

        [HttpGet("payload/teste")]
        public GrupoCarteira TesteGrupo()
        {
            GrupoCarteira grupoCarteira = new GrupoCarteira();
            grupoCarteira.Codigo = "GrupoTeste";
            grupoCarteira.Descricao = "Grupo de carteira teste";
            grupoCarteira.UserName = "teste";
            grupoCarteira.Carteiras.Add("ITest");
            return grupoCarteira;
        }

        [HttpGet]
        public IList<GrupoCarteira> GetAll() => repository.GetAll().ToList();

        [HttpGet("{codigo}")]
        public GrupoCarteira Get(string codigo) => repository.GetById(codigo);

        [HttpPost]
        public void Post([FromBody]GrupoCarteira grupoCarteira) => repository.Insert(grupoCarteira);

        [HttpPut("{codigo}")]
        public void Put([FromBody]GrupoCarteira grupoCarteira) => repository.Insert(grupoCarteira);

        [HttpDelete("{codigo}")]
        public void Delete(string codigo) => repository.Delete(repository.GetById(codigo));
    }
}
