using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET_Core_Task.BLL.DTO.Teachers;
using NET_Core_Task.BLL.MediatR.Teachers;
using NET_Core_Task.BLL.MediatR.Teachers.Delete;
using NET_Core_Task.BLL.MediatR.Teachers.GetById;
using NET_Core_Task.BLL.MediatR.Teachers.Update;

namespace NET_Core_Task.WebAPI.Controllers
{
    public class TeacherController : BaseApiController
    {
        /// <summary>
        /// Finds a list of all teachers
        /// </summary>
        /// <returns>IEnumerable of TeacherDTO </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllTeachersQuery()));
        }

        /// <summary>
        /// Finds a teacher by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetTeacherByIdQuery(id)));
        }

        /// <summary>
        ///  Create teacher
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherDTO teacher)
        {
            return HandleResult(await Mediator.Send(new CreateTeacherCommand(teacher)));
        }

        /// <summary>
        /// Delete teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteTeacherCommand(id)));
        }

        /// <summary>
        /// Update Teacher`s param
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TeacherUpdateDTO teacher)
        {
            return HandleResult(await Mediator.Send(new UpdateTeacherCommand(teacher)));
        }
    }
}
