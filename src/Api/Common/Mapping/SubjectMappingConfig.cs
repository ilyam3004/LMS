using Application.Subjects.Commands.CreateSubject;
using Contracts.Requests.Subjects;
using Mapster;

namespace Api.Common.Mapping;

public class SubjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateSubjectRequest, string), CreateSubjectCommand>()
            .Map(dest => dest.Name, src => src.Item1.Name)
            .Map(dest => dest.Description, src => src.Item1.Description)
            .Map(dest => dest.GroupName, src => src.Item1.GroupName)
            .Map(dest => dest.Token, src => src.Item2);
    }
}