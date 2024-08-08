﻿using FluentResults;
using NET_Core_Task.BLL.Behavior;
using NET_Core_Task.BLL.DTO.Teacher;

namespace NET_Core_Task.BLL.MediatR.Teachers.Update
{
    public record UpdateTeacherCommand(TeacherUpdateDTO Teacher) : IValidatableRequest<Result<TeacherUpdateDTO>>;
}
