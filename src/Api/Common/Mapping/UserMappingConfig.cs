using Contracts.Responses.Authentication;
using Application.Models;
using Mapster;

namespace Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.UserId, src => src.User.UserId)
            .Map(dest => dest.Email, src => src.User.Email);
    }
}