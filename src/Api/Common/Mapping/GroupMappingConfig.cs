using Application.Models.Groups;
using Api.Protos;
using Mapster;

namespace Api.Common.Mapping;

public class GroupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GroupResult, GroupResponse>()
            .Map(dest => dest.GroupId, src => src.Group.GroupId)
            .Map(dest => dest.Name, src => src.Group.Name)
            .Map(dest => dest.Department, src => src.Group.Department)
            .AfterMapping((src, dest) => 
                dest.Students.AddRange(src.Students.Adapt<List<StudentResponse>>()));
    }
}