using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;
using Domain.Enums;
using NHibernateConfigs;
using Web.Models.Requests;

namespace Web.Services
{
    public class RequestCreationService
    {
        public void Create(NewRequestFormDto requestFormDto)
        {
            var categoryDs = new DataStore<Category>();
            var category = categoryDs.Get(requestFormDto.CategoryId);
            var currentUserService = new CurrentOperatorService();
            var newRequest = new Request
            {
                Category = category,
                Comment = requestFormDto.Comment,
                Author = currentUserService.GetCurrentUser(),
                ConsumerName = requestFormDto.CustemerFio,
                Date = DateTime.UtcNow,
                Phone = requestFormDto.PhoneNumber,
                State = EState.Registered
            };

            var reqDataStore = new DataStore<Request>();
            reqDataStore.Save(newRequest);
        }
    }
}