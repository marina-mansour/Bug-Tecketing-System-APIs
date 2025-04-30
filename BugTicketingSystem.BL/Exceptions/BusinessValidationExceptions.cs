using Microsoft.IdentityModel.Tokens;

namespace BugTicketingSystem.BL
{
    public class BusinessValidationException : Exception
    {
        public List<ValidationFailure> Errors { get; }
        public BusinessValidationException(List<ValidationFailure> errors)
        {
            Errors = errors;
        }

    }
}
