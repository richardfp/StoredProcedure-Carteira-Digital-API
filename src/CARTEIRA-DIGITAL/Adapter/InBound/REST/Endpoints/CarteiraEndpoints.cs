using CARTEIRA_DIGITAL.Adapter.InBound.REST.Dto;
using CARTEIRA_DIGITAL.Adapter.InBound.REST.Mappers;
using CARTEIRA_DIGITAL.Domain.Core.DomainModel;
using CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound;

namespace CARTEIRA_DIGITAL.Adapter.InBound.REST.Endpoints
{
    public static class CarteiraEndpoints
    {
        public static void AddEndpoints(this WebApplication app)
        {
            app.MapPost("/criarconta", CriarContaEndpoint);
            app.MapGet("/saldoconta", ConsultarSaldoEndpoint);
            app.MapPut("/sacarsaldoconta", SacarSaldoConta);
            app.MapGet("/log", VerificarLog);
        }

        public static async Task<IResult> VerificarLog(ICarteiraUseCase usecase, int id)
        {
            try
            {
                var usecaselayer = await usecase.UCTransactionlog(id);

                return Results.Ok(new
                {
                    succes = true,
                    mensage = new
                    {
                        status = usecaselayer.statusMessage,
                        logs = usecaselayer.logs
                    }
                });
            }
            catch (Exception ex)
            {

                return Results.BadRequest(new
                {
                    success = false,
                    mensage = ex.Message
                });
            }
        }

        public static async Task<IResult> CriarContaEndpoint(ICarteiraUseCase useCase, UserRequest request)
        {
            try
            {
                var model = MapperEndpoints.DtoToModel(request);

                await useCase.UCCriarUsuario(model);

                return Results.Created("/criarconta", new
                {
                    success = true,
                    mensage = "Usuário criado com sucesso"
                });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new
                {
                    success = false,
                    mensage = ex.Message
                });
            }
        }
        
        public static async Task<IResult> ConsultarSaldoEndpoint(ICarteiraUseCase usecase, int UserId, string Password)
        {
            try
            {
                var model = new SaldoModel
                {
                    UserId = UserId,
                    PasswordHash = Password
                };

                var usecaseLayer = await usecase.UCConsultarSaldo(model);

                return Results.Ok(new
                {
                    success = true,
                    mensage = $"Saldo em conta: ${usecaseLayer.Balance}"
                });
            }
            catch (Exception ex)
            {

                return Results.BadRequest(new
                {
                    success = false,
                    mensage = ex.Message
                });
            }
        }

        public static async Task<IResult> SacarSaldoConta(ICarteiraUseCase useCase, SaqueRequest saqueRequest)
        {
            try
            {
                var model = new SaqueModel
                {
                    UserId = saqueRequest.UserId,
                    HashPassword = saqueRequest.HashPassword,
                    ValorDoSaque = saqueRequest.ValorDoSaque
                };

                await useCase.UCSaqueSaldoConta(model);

                return Results.Ok(new
                {
                    success = true,
                    mensage = "Saque realizado com sucesso !!"
                });
            }
            catch (Exception ex)
            {

                return Results.BadRequest(new
                {
                    success = false,
                    mensage = ex.Message
                });
            }
        }
    }
}
