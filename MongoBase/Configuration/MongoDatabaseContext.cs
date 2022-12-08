using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoBase.Configuration;

namespace Jhispterlorf.Infrastructure.Data;

public class MongoDatabaseContext : IMongoDatabaseContext, IDisposable
{
    private MongoClient _mongoClient { get; set; }
    public IClientSessionHandle Session { get; set; }
    public IMongoDatabase Database { get; }

    protected List<Func<Task>> _commands;
    public MongoDatabaseContext(IOptions<MongoDatabaseConfig> configuration)
    {
        _mongoClient = new MongoClient(configuration.Value.ConnectionString);
        _commands = new List<Func<Task>>();
        Session = _mongoClient.StartSession();
        Database = Session.Client.GetDatabase(configuration.Value.DatabaseName);
    }

    public IMongoCollection<T> Set<T>(string name)
    {
        return Database.GetCollection<T>(name);
    }

    public void AddCommand(Func<Task> func)
    {
        _commands.Add(func);
    }

    public async Task<int> SaveChangesAsync()
    {
        var commandTasks = _commands.Select(c => c());
        await Task.WhenAll(commandTasks);
        return _commands.Count;
    }

    public void Dispose()
    {
        Session?.Dispose();
    }
}
