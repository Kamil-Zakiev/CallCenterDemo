using Domain.Enums;

namespace Web.Autentications.Attributes
{
    public class OperatorOnlyAttribute : CustomAuthorizeFilterAttribute
    {
        public OperatorOnlyAttribute()
        {
            Role = ERole.Operator;
        }
    }
}