using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Requests;
using Domain.Interfaces.Requests.Dto;
using Domain.Interfaces.Users;

namespace Domain.Services.Services
{
    public class RequestCreationService : IRequestCreationService
    {
        public IDataStore<Category> CategoryDataStore { get; set; }

        public IDataStore<Request> RequestDataStore { get; set; }

        public IDataStore<User> UserDataStore { get; set; }

        public ICurrentOperatorService CurrentOperatorService { get; set; }

        public void Create(NewRequestFormDto requestFormDto)
        {
            // todo wrap to transaction
            var category = CategoryDataStore.Get(requestFormDto.CategoryId);
            
            var newRequest = new Request
            {
                Category = category,
                Comment = requestFormDto.Comment,
                Author = UserDataStore.Get(requestFormDto.AuthorId),
                ConsumerName = requestFormDto.CustemerFio,
                Date = DateTime.UtcNow,
                Phone = requestFormDto.PhoneNumber,
                State = EState.Registered
            };
            
            RequestDataStore.Save(newRequest);
        }
    }
}