﻿using Application.UnitTests.TestUtils.TestConstants;
using Task = Domain.Entities.Task;
using Domain.Entities;

namespace Application.UnitTests.TestUtils.Factories;

public static class SubjectFactory
{
    public static List<Subject> CreateSubjects(Guid? groupId = null,
        int subjectsCount = 1,
        List<Task>? tasks = null)
    {
        Guid generatedGroupId = groupId ?? Constants.Group.GroupId;

        return Enumerable.Range(0, subjectsCount)
            .Select(index =>
            {
                var subjectId = Constants.Subject.SubjectId;

                return new Subject
                {
                    SubjectId = subjectId,
                    Name = Constants.Subject.SubjectNameFromGivenIndex(index),
                    Description = Constants.Subject.SubjectDescriptionFromGivenIndex(index),
                    GroupId = generatedGroupId,
                    LecturerId = Constants.Lecturer.LecturerId,
                    Tasks = tasks ?? TaskFactory.CreateTasks(subjectId),
                    Group = GroupFactory.CreateGroupWithOutSubjects()
                };
            }).ToList();
    }
}