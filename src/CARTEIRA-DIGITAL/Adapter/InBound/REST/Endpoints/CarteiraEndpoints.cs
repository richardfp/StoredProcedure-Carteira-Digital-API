using CARTEIRA_DIGITAL.Adapter.InBound.REST.Dto;
using CARTEIRA_DIGITAL.Adapter.InBound.REST.Mappers;
using CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound;

namespace CARTEIRA_DIGITAL.Adapter.InBound.REST.Endpoints
{
    public static class CarteiraEndpoints
    {
        public static void AddEndpoints(this WebApplication app)
        {
            app.MapPost("/criarconta", CriarContaEndpoint);
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
    }
}
