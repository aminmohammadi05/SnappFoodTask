using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Order.Command.Api.Commands;
using Order.Command.Domain.Aggregates;
using Order.Command.Infrastructure.Config;
using Order.Command.Infrastructure.Dispatchers;
using Order.Command.Infrastructure.Handlers;
using Order.Command.Infrastructure.Infrastructures;
using Order.Command.Infrastructure.Repositories;
using Order.Command.Infrastructure.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<OrderAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

// command handler methods registration

var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();

dispatcher.RegisterHandler<NewOrderCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<EditOrderCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<RemoveOrderCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<AddProductCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<ChangeProductCountCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<RemoveProductCommand>(commandHandler.HandleAsync);

builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
