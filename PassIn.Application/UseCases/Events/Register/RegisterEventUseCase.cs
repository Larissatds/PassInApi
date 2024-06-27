﻿using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventUseCase
    {
        private readonly PassInDbContext _dbContext;

        public RegisterEventUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredJson Execute(RequestEventJson request)
        {
            Validate(request);

            var entity = new Event
            {
                Title = request.Title,
                Details = request.Details,
                Maximum_Attendees = request.MaxAttendees,
                Slug = request.Title.ToLower().Replace(" ", "-")
            };

            _dbContext.Events.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredJson
            {
                Id = entity.Id
            };
        }

        private void Validate(RequestEventJson request)
        {
            if(request.MaxAttendees <= 0)
            {
                throw new ErrorOnValidationException("The maximum attendees is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ErrorOnValidationException("The title is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new ErrorOnValidationException("The details is invalid.");
            }
        }
    }
}
