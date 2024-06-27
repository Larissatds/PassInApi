using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee
{
    public class RegisterAttendeeOnEventUseCase
    {
        private readonly PassInDbContext _dbContext;
        public RegisterAttendeeOnEventUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredJson Execute(decimal eventId, ResponseRegisterEventJson request)
        {
            Validate(eventId, request);

            var entity = new Attendee
            {
                Name = request.Name,
                Email = request.Email,
                Event_Id = eventId,
                Created_At = DateTime.UtcNow
            };

            _dbContext.Attendees.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredJson
            {
                Id = entity.Id
            };
        }

        private void Validate(decimal eventId, ResponseRegisterEventJson request)
        {
            var eventEntity = _dbContext.Events.Find(eventId);

            if(eventEntity == null)
            {
                throw new NotFoundException("An event with this Id don't exist.");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("The name is invalid.");
            }

            if (!EmailIsValid(request.Email))
            {
                throw new ErrorOnValidationException("The e-mail is invalid.");
            }

            var attendeeAlreadyRegistered = _dbContext.Attendees.Any(e => e.Email.Equals(request.Email) && e.Event_Id == eventId);

            if (attendeeAlreadyRegistered)
            {
                throw new ErrorOnValidationException("You can't register twice on the same event.");
            }

            var attendeesCount = _dbContext.Attendees.Count(a => a.Event_Id == eventId);

            if( attendeesCount >= eventEntity.Maximum_Attendees)
            {
                throw new ErrorOnValidationException("There is no tickets for this event.");
            }
        }

        private bool EmailIsValid(string email)
        {
            try
            {
                new MailAddress(email);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
