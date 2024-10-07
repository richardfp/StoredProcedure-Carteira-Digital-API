using CARTEIRA_DIGITAL.Adapter.InBound.REST.Endpoints;
using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.RepositoryLayer;
using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Settings;
using CARTEIRA_DIGITAL.Domain.Application.UseCase;
using CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound;

namespace CARTEIRA_DIGITAL.Infra.Registers
{
    public static class Extensions
    {
        //public static void CriaInjecaoDependencia(this IServiceCollection service)
        //{
        //    string connectionString = "Server=W3AS-DEV-18\\SQLEXPRESS;Database=CARTEIRA_DIGITAL;User Id=sa;Password=Postgres;";
        //    service.AddSingleton<ISqlConnectionFactory>(new SqlConnectionFactory(connectionString));
        //    service.AddScoped<ICarteiraRepository, CarteiraRepository>();
        //    service.AddScoped<ICarteiraUseCase, CarteiraUseCase>();
        //}

        //public static void OrganzarPipeline(this WebApplication app)
        //{
        //    if (app.Environment.IsDevelopment())
        //    {
        //        app.UseSwagger();
        //        app.UseSwaggerUI();
        //    }

        //    app.AddEndpoints();

        //    app.UseHttpsRedirection();
        //}
    }
}
