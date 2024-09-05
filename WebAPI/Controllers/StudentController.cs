using Microsoft.AspNetCore.Mvc;
using NET_Core_Task.BLL.DTO.Student;
using NET_Core_Task.BLL.MediatR.Students.Create;
using NET_Core_Task.BLL.MediatR.Students.Delete;
using NET_Core_Task.BLL.MediatR.Students.GetAll;
using NET_Core_Task.BLL.MediatR.Students.GetById;
using NET_Core_Task.BLL.MediatR.Students.Update;

namespace NET_Core_Task.WebAPI.Controllers
{
    public class StudentController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllStudentQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetStudentByIdQuery(id)));
        }
        /// <summary>
        /// cteate student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDTO student)
        {
            return HandleResult(await Mediator.Send(new CreateStudentCommand(student)));
        }

        /// <summary>
        /// delete student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteStudentCommand(id)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StudentUpdateDTO student)
        {
            return HandleResult(await Mediator.Send(new UpdateStudentCommand(student)));
        }
    }
}
