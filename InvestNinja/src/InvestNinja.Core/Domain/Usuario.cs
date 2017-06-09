using InvestNinja.Core.Data;
using MongoDB.Bson.Serialization.Attributes;

namespace InvestNinja.Core.Domain
{
    public class Usuario : IEntity
    {
        [BsonId]
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }
    }
}
