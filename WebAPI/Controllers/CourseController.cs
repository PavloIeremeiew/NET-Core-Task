using Microsoft.AspNetCore.Mvc;
using NET_Core_Task.BLL.DTO.Course;
using NET_Core_Task.BLL.MediatR.Courses.Create;
using NET_Core_Task.BLL.MediatR.Courses.Delete;

namespace NET_Core_Task.WebAPI.Controllers
{
    public class CourseController : BaseApiController
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    return HandleResult(await Mediator.Send(new GetAllCoursesQuery()));
        //}

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetById([FromRoute] int id)
        //{
        //    return HandleResult(await Mediator.Send(new GetCourseByIdQuery(id)));
        //}

        /// <summary>
        /// Create course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDTO course)
        {
            return HandleResult(await Mediator.Send(new CreateCourseCommand(course)));
        }

        /// <summary>
        /// Delete course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteCourseCommand(id)));
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] CourseDTO course)
        //{
        //    return HandleResult(await Mediator.Send(new UpdateCourseCommand(course)));
        //}
    }
}
