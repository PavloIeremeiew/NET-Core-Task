using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET_Core_Task.BLL.DTO.Teacher;
using NET_Core_Task.BLL.MediatR.Teachers;

namespace NET_Core_Task.WebAPI.Controllers
{
    public class TeacherController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllTeachersQuery()));
        }

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetById([FromRoute] int id)
        //{
        //    return HandleResult(await Mediator.Send(new GetTeacherByIdQuery(id)));
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherDTO teacher)
        {
            return HandleResult(await Mediator.Send(new CreateTeacherCommand(teacher)));
        }

        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    return HandleResult(await Mediator.Send(new DeleteTeacherCommand(id)));
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] TeacherDTO teacher)
        //{
        //    return HandleResult(await Mediator.Send(new UpdateTeacherCommand(teacher)));
        //}
    }
}
