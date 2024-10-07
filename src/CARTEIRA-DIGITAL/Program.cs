using CARTEIRA_DIGITAL.Adapter.InBound.REST.Endpoints;
using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.RepositoryLayer;
using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Settings;
using CARTEIRA_DIGITAL.Domain.Application.UseCase;
using CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = "Server=W3-DEV-18\\SQLEXPRESS;Database=CARTEIRA_DIGITAL;User Id=sa;Password=Postgres;";
builder.Services.AddSingleton<ISqlConnectionFactory>(new SqlConnectionFactory(connectionString));
builder.Services.AddTransient<ICarteiraRepository, CarteiraRepository>();
builder.Services.AddScoped<ICarteiraUseCase, CarteiraUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddEndpoints();

app.UseHttpsRedirection();

app.Run();
