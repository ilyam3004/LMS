using Application.Subjects.Commands.CreateSubject;
using Contracts.Requests.Subjects;
using Contracts.Responses.Subjects;
using Application.Models;
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

        config.NewConfig<LecturerSubjectResult, LecturerSubjectResponse>()
            .Map(dest => dest.SubjectId, src => src.Subject.SubjectId)
            .Map(dest => dest.Name, src => src.Subject.Name)
            .Map(dest => dest.Description, src => src.Subject.Description)
            .Map(dest => dest.Groups, src => src.Groups);

        config.NewConfig<StudentSubjectResult, StudentSubjectResponse>()
            .Map(dest => dest.Id, src => src.Subject.SubjectId)
            .Map(dest => dest.Name, src => src.Subject.Name)
            .Map(dest => dest.Description, src => src.Subject.Description)
            .Map(dest => dest.LecturerName, src => src.lecturerName);
    }
}