using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Attendees.GetAllByEventId
{
    public class GetAllAttendeesByEventIdUseCase
    {
        private readonly PassInDbContext _dbContext;

        public GetAllAttendeesByEventIdUseCase()
        {
            _dbContext = new PassInDbContext();
        }


        public ResponseAllAttendeesJson Execute(decimal eventId)
        {
            var entity = _dbContext.Events.Include(e => e.Attendees).ThenInclude(e => e.CheckIn).FirstOrDefault(e => e.Id == eventId);

            if (entity == null)
            {
                throw new NotFoundException("An event with this Id don't exist.");
            }

            return new ResponseAllAttendeesJson
            {
                Attendees = entity.Attendees.Select(a => new ResponseAttendeeJson
                {
                    Id = a.Id,
                    Name = a.Name,
                    Email = a.Email,
                    CreatedAt = a.Created_At,
                    CheckedInAt = a.CheckIn?.Created_At
                }).ToList()
            };
        }
    }
}
