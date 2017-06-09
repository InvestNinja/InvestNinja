using InvestNinja.Core.Data;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace InvestNinja.Core.Domain
{
    public class GrupoCarteira : IEntity
    {
        public GrupoCarteira()
        {
            Carteiras = new List<string>();
        }

        [BsonId]
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string UserName { get; set; }

        public IList<string> Carteiras;
    }
}
