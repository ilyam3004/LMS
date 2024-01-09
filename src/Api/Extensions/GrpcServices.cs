﻿using Api.Services;

namespace Api.Extensions;

public static class GrpcServices
{
    public static WebApplication AddGrpcServices(this WebApplication app)
    {
        app.MapGrpcService<UserService>();
        app.MapGrpcService<GroupService>();
        app.MapGrpcService<SubjectService>();
        app.MapGrpcService<TaskService>();
        app.MapGrpcService<GradeService>();
        
        return app;
    }
}