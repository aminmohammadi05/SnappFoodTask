using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Order.Query.Api.Queries;
using Order.Query.Domain.Entities;
using Order.Query.Domain.Repositories;
using Order.Query.Infrastructure.Consumers;
using Order.Query.Infrastructure.DataAccess;
using Order.Query.Infrastructure.Dispatchers;
using Order.Query.Infrastructure.Handlers;
using Order.Query.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Action<DbContextOptionsBuilder> configureDbContext = (o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

// Create database and tables from code
var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
dataContext.Database.EnsureCreated();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IQueryHandler, QueryHandler>();
builder.Services.AddScoped<IEventHandler, Order.Query.Infrastructure.Handlers.EventHandler>();
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

// register query handler methods
var queryHandler = builder.Services.BuildServiceProvider().GetRequiredService<IQueryHandler>();
var dispatcher = new QueryDispatcher();
dispatcher.RegisterHandler<FindAllOrdersQuery>(queryHandler.HandleAsync);
dispatcher.RegisterHandler<FindOrderByIdQuery>(queryHandler.HandleAsync);
dispatcher.RegisterHandler<FindOrdersByCriteriaQuery>(queryHandler.HandleAsync);
dispatcher.RegisterHandler<FindOrdersWithProductsQuery>(queryHandler.HandleAsync);
builder.Services.AddSingleton<IQueryDispatcher<OrderEntity>>(_ => dispatcher);



builder.Services.AddControllers();
builder.Services.AddHostedService<ConsumerHostedService>();
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
