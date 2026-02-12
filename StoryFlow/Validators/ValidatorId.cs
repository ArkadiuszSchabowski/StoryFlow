using StoryFlow.Exceptions;
using StoryFlow.Interfaces;

namespace StoryFlow.Validators
{
    public class ValidatorId : IValidatorId
    {
        public void ValidateId(int? id)
        {
            if(id == null)
            {
                throw new BadRequestException("Id is required.");
            }

            if (id <= 0)
            {
                throw new BadRequestException("Id must be greater than 0.");
            }
        }
    }
}
