using Application.Models;
using Application.Models.Groups;
using Contracts.Responses.Students;
using Mapster;

namespace Api.Common.Mapping;

public class StudentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<StudentResult, StudentResponse>()
            .Map(dest => dest.StudentId, src => src.Student.StudentId)
            .Map(dest => dest.UserId, src => src.Student.UserId)
            .Map(dest => dest.GroupId, src => src.Student.GroupId)
            .Map(dest => dest.FullName, src => src.Student.FullName)
            .Map(dest => dest.Address, src => src.Student.Address)
            .Map(dest => dest.Birthday, src => src.Student.Birthday);
    }
}