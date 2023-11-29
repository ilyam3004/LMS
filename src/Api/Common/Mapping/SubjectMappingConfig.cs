using Application.Models.Grades;
using Application.Subjects.Commands.CreateSubject;
using Contracts.Requests.Subjects;
using Contracts.Responses.Subjects;
using Application.Models.Subjects;
using Application.Models.Tasks;
using Contracts.Responses.Grades;
using Contracts.Responses.Students;
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
            .Map(dest => dest.Group, src => src.Group);

        config.NewConfig<StudentSubjectResult, StudentSubjectResponse>()
            .Map(dest => dest.SubjectId, src => src.Subject.SubjectId)
            .Map(dest => dest.Name, src => src.Subject.Name)
            .Map(dest => dest.Description, src => src.Subject.Description)
            .Map(dest => dest.LecturerName, src => src.Subject.Lecturer.FullName)
            .Map(dest => dest.Tasks, src => src.Tasks)
            .Map(dest => dest.AverageGrade, src => src.AverageGrade)
            .Map(dest => dest.TotalGrade, src => src.TotalGrade);
        
        config.NewConfig<SubjectGradesResult, SubjectGradesResponse>()
            .Map(dest => dest.SubjectId, src => src.SubjectId)
            .Map(dest => dest.SubjectName, src => src.SubjectId)
            .Map(dest => dest.StudentTasks, src => src.StudentTasks);
    }
}