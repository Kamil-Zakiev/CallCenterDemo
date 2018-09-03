using Domain.Enums;

namespace Web.Autentications.Attributes
{
    public class OperatorOnlyAttribute : CustomAuthorizeFilterAttribute
    {
        public OperatorOnlyAttribute()
        {
            Roles = new[] { ERole.Operator };
        }
    }
}