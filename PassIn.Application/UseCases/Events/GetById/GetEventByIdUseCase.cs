using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById
{
    public class GetEventByIdUseCase
    {
        private readonly PassInDbContext _dbContext;
        public GetEventByIdUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseEventJson Execute(decimal id)
        {
            var entity = _dbContext.Events.Find(id);

            if (entity == null)
            {
                throw new NotFoundException("An event with this Id don't exist.");
            }

            return new ResponseEventJson
            {
                Id = entity.Id,
                Title = entity.Title,
                Details = entity.Details,
                MaxAttendees = entity.Maximum_Attendees,
                AttendeesAmount = -1
            };
        }
    }
}
