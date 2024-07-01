using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace PassIn.Application.UseCases.Events.CheckIns.DoCkeckIn
{
    public class DoAttendeeCheckInUseCase
    {
        private readonly PassInDbContext _dbContext;

        public DoAttendeeCheckInUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredJson Execute(decimal attendeeId)
        {
            Validate(attendeeId);

            var entity = new CheckIn
            {
                Created_At = DateTime.UtcNow,
                Attendee_Id = attendeeId
            };

            _dbContext.CheckIns.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredJson
            {
                Id = entity.Id
            };
        }

        private void Validate(decimal attendeeId)
        {
            var existAttendee = _dbContext.Attendees.Any(a => a.Id == attendeeId);

            if(!existAttendee)
            {
                throw new NotFoundException("The attendee with this Id wasn't found.");
            }

            var existCheckIn = _dbContext.CheckIns.Any(c => c.Attendee_Id == attendeeId);

            if(existCheckIn)
            {
                throw new ErrorOnValidationException("Attendee cannot check in twice in the same event.");
            }
        }
    }
}
