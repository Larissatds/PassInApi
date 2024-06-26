using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventUseCase
    {
        public ResponseRegisteredEventJson Execute(RequestEventJson request)
        {
            Validate(request);

            var dbContext = new PassInDbContext();

            var evento = new Event
            {
                Title = request.Title,
                Details = request.Details,
                Maximum_Attendees = request.MaxAttendees,
                Slug = request.Title.ToLower().Replace(" ", "-")
            };

            dbContext.Events.Add(evento);
            dbContext.SaveChanges();

            return new ResponseRegisteredEventJson
            {
                Id = evento.Id
            };
        }

        private void Validate(RequestEventJson request)
        {
            if(request.MaxAttendees <= 0)
            {
                throw new PassInException("The maximum attendees is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new PassInException("The title is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new PassInException("The details is invalid.");
            }
        }
    }
}
