using Application.Models;
using Api.Protos;
using Application.Models.Groups;
using Google.Protobuf.Collections;
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
            .Map(dest => dest.Students, src => src.Students);
    }
}