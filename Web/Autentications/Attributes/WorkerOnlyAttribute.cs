﻿using Domain.Enums;

namespace Web.Autentications.Attributes
{
    public class WorkerOnlyAttribute : CustomAuthorizeFilterAttribute
    {
        public WorkerOnlyAttribute()
        {
            Roles = new[] { ERole.Worker };
        }
    }
}