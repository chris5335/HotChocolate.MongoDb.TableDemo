using GraphQlServer;
using HotChocolate.Data.Filters;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
MongoClient client = new("mongodb://mongoadmin:secret@localhost:27017/");
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
builder.Services
    .AddSingleton(client.GetDatabase("TestTableDb"))
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMongoDbFiltering()
    .AddMongoDbSorting()
    .AddMongoDbPagingProviders()
    .AddConvention<IFilterConvention>(new FilterConventionExtension(x =>
        x.AddProviderExtension(
            new MongoDbFilterProviderExtension(y =>
                y.AddFieldHandler<MongoDbInsensitiveStringContainsHandler>()))))
    // .AddMongoDbCustomFiltering()
    ;

var app = builder.Build();

app.UseCors("Open");
app.MapGraphQL();
app.Run();