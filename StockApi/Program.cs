using StockApi.Repository;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var stockApiConnectionString = configuration.GetConnectionString("StockApiDb");
builder.Services.ConfigureStockRepository(stockApiConnectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UpdateDatabase();

app.MapControllers();

app.Run();
