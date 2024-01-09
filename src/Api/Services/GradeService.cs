using Api.Helpers;
using Api.Protos;
using Application.Features.Grades.Queries;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MapsterMapper;
using MediatR;

namespace Api.Services;

public class GradeService : Grade.GradeBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public GradeService(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override async Task<LecturerGradesResponse> GetLecturerGrades(Empty request, ServerCallContext context)
    {
        var token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];
        
        var command = new GetLecturerGradesQuery(token);

        var result = await _sender.Send(command);

        return result.Match(
            value => new LecturerGradesResponse()
            {
                Grades = { _mapper.Map<List<SubjectGradesResponse>>(value) }
            },
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }
}