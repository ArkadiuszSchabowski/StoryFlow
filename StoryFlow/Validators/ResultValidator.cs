using StoryFlow.Exceptions;
using StoryFlow.Interfaces;

namespace StoryFlow.Validators
{
    public class ResultValidator : IResultValidator
    {
        public void ValidateResult(int result)
        {
            if(result < 0 || result > 220)
            {
                throw new BadRequestException("Inncorect result.");
            }
        }
    }
}
