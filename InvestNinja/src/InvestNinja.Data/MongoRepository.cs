using InvestNinja.Core.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Data
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly MongoClient client;
        private IMongoDatabase database;

        public MongoRepository()
        {
            client = new MongoClient("");
            database = client.GetDatabase("");
        }

        public IEnumerable<T> GetAll() => database.GetCollection<T>(typeof(T).Name).Find(new BsonDocument()).ToList();

        public T GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return database.GetCollection<T>(typeof(T).Name).Find(filter).First();
        }

        public void Delete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            InsertOneOptions io = new InsertOneOptions();
            database.GetCollection<T>(typeof(T).Name).InsertOne(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
