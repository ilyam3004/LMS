using Api.Protos;
using Application.Models.Authentication;
using Mapster;

namespace Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.UserId, src => src.User.UserId)
            .Map(dest => dest.Email, src => src.User.Email);

        config.NewConfig<ProfileResult, LecturerProfileResponse>()
            .Map(dest => dest.UserId, src => src.User.UserId)
            .Map(dest => dest.Email, src => src.User.Email)
            .Map(dest => dest.FullName, src => src.User.Lecturer!.FullName)
            .Map(dest => dest.Degree, src => src.User.Lecturer!.Degree)
            .Map(dest => dest.Address, src => src.User.Lecturer!.Address)
            .Map(dest => dest.Birthday, src => src.User.Lecturer!.Birthday.ToString("dd.MM.yyyy"));

        config.NewConfig<ProfileResult, StudentProfileResponse>()
            .Map(dest => dest.UserId, src => src.User.UserId)
            .Map(dest => dest.Email, src => src.User.Email)
            .Map(dest => dest.FullName, src => src.User.Student!.FullName)
            .Map(dest => dest.Group, src => src.User.Student!.Group.Name)
            .Map(dest => dest.Address, src => src.User.Student!.Address)
            .Map(dest => dest.Birthday, src => src.User.Student!.Birthday.ToString("dd.MM.yyyy"));
    }
}