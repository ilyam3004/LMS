using Application.Tasks.Commands;
using Application.Tasks.Commands.CreateTask;
using Contracts.Requests.Tasks;
using Mapster;

namespace Api.Common.Mapping;

public class TaskMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(AssignTaskRequest, string), AssignTaskCommand>()
            .Map(dest => dest.Title, src => src.Item1.Title)
            .Map(dest => dest.Description, src => src.Item1.Description)
            .Map(dest => dest.SubjectId, src => src.Item1.SubjectId)
            .Map(dest => dest.Deadline, src => src.Item1.Deadline)
            .Map(dest => dest.MaxGrade, src => src.Item1.MaxGrade)
            .Map(dest => dest.Token, src => src.Item2);
    }
}