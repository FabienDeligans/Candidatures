using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CandidaturesCore.Data;
using CandidaturesCore.Model;
using MongoDB.Driver;

namespace CandidaturesCore.Controller
{
    public class BaseController<T> where T : Entity
    {
        public IQueryable<T> QueryCollection()
        {
            using var context = new Context<T>();
            return context.Collection.AsQueryable();
        }

        public void Insert(T entity)
        {
            using var context = new Context<T>();
            context.Collection.InsertOne(entity);
        }

        public void InsertAll(IEnumerable<T> listEntities)
        {
            using var context = new Context<T>();
            context.Collection.InsertMany(listEntities);
        }

        public void Remove(T entity)
        {
            using var context = new Context<T>();
            context.Collection.DeleteOne(v => v.Id == entity.Id);
        }

        public void RemoveAll(Expression<Func<T, bool>> predicate)
        {
            using var context = new Context<T>();
            context.Collection.DeleteMany(predicate);
        }


        public void Update(T entity, string propertyName, object newValue)
        {
            using var context = new Context<T>();
            var update = Builders<T>.Update.Set(propertyName, newValue);
            context.Collection.UpdateOne(v => v.Id == entity.Id, update);
        }


        public void ReplaceOne(T entity)
        {
            using var context = new Context<T>();
            context.Collection.ReplaceOne(v => v.Id == entity.Id, entity);
        }
    }
}
