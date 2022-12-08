using MongoDB.Driver;

namespace MongoBase.Configuration;

public interface IMongoDatabaseContext
{
    IClientSessionHandle Session { get; set; }
    IMongoDatabase Database { get; }
    IMongoCollection<T> Set<T>(string name);
    Task<int> SaveChangesAsync();
    void AddCommand(Func<Task> p);
    void Dispose();
}
