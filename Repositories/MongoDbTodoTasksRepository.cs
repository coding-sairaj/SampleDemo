using MongoDB.Bson;
using MongoDB.Driver;
using SampleDemo.Models;

namespace SampleDemo.Repositories;

public class MongoDbTodoTasksRepository : ITodoTasksRepository
{
    private const string databaseName = "todolist";
    private const string collectionName = "tasks";
    private readonly IMongoCollection<ToDo> todoTasksCollection;
    private readonly FilterDefinitionBuilder<ToDo> filterBuilder = Builders<ToDo>.Filter;
    public MongoDbTodoTasksRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(databaseName);
        todoTasksCollection = database.GetCollection<ToDo>(collectionName);
    }
    public async Task CreateTaskAsync(ToDo todo)
    {
        await todoTasksCollection.InsertOneAsync(todo);

    }

    public async Task DeleteTaskAsync(Guid id)
    {
        var filter = filterBuilder.Eq(task => task.Id, id);
        await todoTasksCollection.DeleteOneAsync(filter);
    }

    public async Task<ToDo> GetTaskAsync(Guid id)
    {
        var filter = filterBuilder.Eq(task => task.Id, id);
        return await todoTasksCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<ToDo>> GetTasksAsync()
    {
        return await todoTasksCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateTaskAsync(ToDo todo)
    {
        var filter = filterBuilder.Eq(task => task.Id, todo.Id);
        await todoTasksCollection.ReplaceOneAsync(filter, todo);
    }
}
