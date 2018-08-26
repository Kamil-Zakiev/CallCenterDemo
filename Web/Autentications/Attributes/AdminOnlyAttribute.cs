using Domain.Enums;

namespace Web.Autentications.Attributes
{
    public class AdminOnlyAttribute : CustomAuthorizeFilterAttribute
    {
        public AdminOnlyAttribute()
        {
            Role = ERole.Admin;
        }
    }
}