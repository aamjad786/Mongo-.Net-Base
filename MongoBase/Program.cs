

using Jhispterlorf.Infrastructure.Data;
using Microsoft.Extensions.Options;
using MongoBase.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mongo Database Configuration
builder.Services.Configure<MongoDatabaseConfig>(builder.Configuration.GetSection(nameof(MongoDatabaseConfig)));
builder.Services.AddSingleton<IMongoDatabaseConfig>(sp => sp.GetRequiredService<IOptions<MongoDatabaseConfig>>().Value);
builder.Services.AddSingleton<IMongoDatabaseContext, MongoDatabaseContext>();


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
