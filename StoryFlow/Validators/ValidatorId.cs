using StoryFlow.Exceptions;
using StoryFlow.Interfaces.Validators;

namespace StoryFlow.Validators
{
    public class ValidatorId : IValidatorId
    {
        public void ValidateId(int? id)
        {
            if(id is null)
            {
                throw new BadRequestException("Id jest wymagane.");
            }

            if (id <= 0)
            {
                throw new BadRequestException("Id musi być większe od 0.");
            }
        }
    }
}
