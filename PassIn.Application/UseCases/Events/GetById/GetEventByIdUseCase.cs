using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById
{
    public class GetEventByIdUseCase
    {
        public  ResponseEventJson Execute(decimal id)
        {
            var dbContext = new PassInDbContext();

            var evento = dbContext.Events.Find(id);

            if (evento == null)
            {
                throw new PassInException("An event with this Id don't exist.");
            }

            return new ResponseEventJson
            {
                Id = evento.Id,
                Title = evento.Title,
                Details = evento.Details,
                MaxAttendees = evento.Maximum_Attendees,
                AttendeesAmount = -1
            };
        }
    }
}
