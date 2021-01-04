using System;
using CandidaturesCore.Model;
using MongoDB.Driver;

namespace CandidaturesCore.Data
{
    public class Context<T> : IDisposable where T : Entity
    {
        private readonly IMongoDatabase _mongoDatabase;
        private MongoClient Client { get; set; }
        private string DatabaseName { get; set; } = "GestionCandidatures";
        public Context()
        {
            Client = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = Client.GetDatabase(DatabaseName);
        }

        public IMongoCollection<T> Collection => _mongoDatabase.GetCollection<T>(typeof(T).Name);

        public void DropDatabase()
        {
            Client.DropDatabase(DatabaseName);
        }

        public void Dispose()
        {
        }
    }
}
