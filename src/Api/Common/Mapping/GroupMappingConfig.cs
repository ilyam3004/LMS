using Application.Models;
using Mapster;

namespace Api.Common.Mapping;

public class GroupMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GroupResult, GroupResponse>()
            .Map(dest => dest.GroupId, src => src.group.GroupId)
            .Map(dest => dest.Name, src => src.group.Name)
            .Map(dest => dest.Department, src => src.group.Department);

        config.NewConfig<List<GroupResult>, GetAllGroupsResponse>()
            .Map(dest => dest.Groups, src => src);
    }
}