﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InvestNinja.Core.Data
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly MongoClient client;
        private IMongoDatabase database;

        public MongoRepository()
        {
            client = new MongoClient("mongodb://tp:salamandra@ds027155.mlab.com:27155/tp-dev");
            database = client.GetDatabase("tp-dev");
        }

        public IEnumerable<T> GetAll() => Collection.Find(new BsonDocument()).ToList();

        public T GetById(string id) => Collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefault();

        public void Delete(IEnumerable<T> entities) => entities.ToList().ForEach(entity => Delete(entity));

        public void Delete(T entity) => Collection.DeleteOne(GetFilterById(entity));

        public void Insert(IEnumerable<T> entities) => Collection.InsertMany(entities);

        public void Insert(T entity) => Collection.InsertOne(entity);

        public void Update(IEnumerable<T> entities) => entities.ToList().ForEach(entity => Update(entity));

        public void Update(T entity) => Collection.ReplaceOne(GetFilterById(entity), entity);

        private FilterDefinition<T> GetFilterById(T entity) => Builders<T>.Filter.Eq("_id", GetIdValue(entity));

        private IMongoCollection<T> Collection => database.GetCollection<T>(typeof(T).Name);

        private object GetIdValue(T entity) => typeof(T).GetRuntimeProperties().Where(prop => prop.GetCustomAttribute(typeof(BsonIdAttribute)) != null).FirstOrDefault().GetValue(entity);
    }
}
