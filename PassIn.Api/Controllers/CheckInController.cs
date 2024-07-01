using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.CheckIns.DoCkeckIn;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        [HttpPost]
        [Route("{attendeeId}")]
        [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult CheckIn([FromRoute] decimal attendeeId)
        {
            var useCase = new DoAttendeeCheckInUseCase();

            var response = useCase.Execute(attendeeId);

            return Created(string.Empty, response);
        }
    }
}
