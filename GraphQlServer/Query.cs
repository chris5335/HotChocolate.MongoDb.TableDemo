// <copyright file="Query.cs" company="Silver Fern.">
// Copyright (c) Silver Fern. All rights reserved.
// </copyright>

using Bogus;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace GraphQlServer;

public sealed class Query
{
    private readonly Faker<Person> _fakePersons = new Faker<Person>()
        .RuleFor(x=> x.Name, faker => faker.Person.FirstName)
        .RuleFor(x => x.Ordinal, faker => _ordinal++);

    private static int _ordinal = 1;

    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseFiltering]
    [UseSorting]
    public async Task<IExecutable<Person>> GetPerson([Service] IMongoDatabase database)
    {
        IMongoCollection<Person> collection = database.GetCollection<Person>(nameof(Person));
        FilterDefinition<Person> filter = Builders<Person>.Filter.Empty;
        Person person = await collection.Find(filter).FirstOrDefaultAsync();
        if (person is null)
        {
            await collection.InsertManyAsync(_fakePersons.Generate(200));
        }

        return collection.AsExecutable();
    }
}

public sealed class Person
{
    [BsonId] public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int Ordinal { get; set; } = 1;
}