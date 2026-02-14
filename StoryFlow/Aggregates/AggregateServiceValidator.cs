using StoryFlow.Interfaces;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Aggregates
{
    public class AggregateServiceValidator : IAggregateServiceValidator
    {
        private readonly IValidator<AddStoryDto> _storyValidator;
        private readonly IValidatorId _validatorId;

        public AggregateServiceValidator(IValidator<AddStoryDto> storyValidator, IValidatorId validatorId)
        {
            _storyValidator = storyValidator;
            _validatorId = validatorId;
        }
        public void Validate(AddStoryDto? item)
        {
            _storyValidator.Validate(item);
        }
        

        public void ValidateId(int? id)
        {
            _validatorId.ValidateId(id);
        }

        public void ValidateSentencesCount(int polishSentencesCount, int englishSentencesCount)
        {
            _storyValidator.ValidateSentencesCount(polishSentencesCount, englishSentencesCount);
        }
    }
}
