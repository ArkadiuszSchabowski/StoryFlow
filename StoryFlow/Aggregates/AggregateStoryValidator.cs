using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

namespace StoryFlow.Aggregates
{
    public class AggregateStoryValidator : IAggregateStoryValidator
    {
        private readonly IValidator<AddStoryDto> _storyValidator;
        private readonly IValidatorId _validatorId;
        private readonly IEntityValidator<Story> _entityValidator;
        private readonly IResultValidator _resultValidator;

        public AggregateStoryValidator(IValidator<AddStoryDto> storyValidator, IValidatorId validatorId, IEntityValidator<Story> entityValidator, IResultValidator resultValidator)
        {
            _storyValidator = storyValidator;
            _validatorId = validatorId;
            _entityValidator = entityValidator;
            _resultValidator = resultValidator;
        }

        public void ThrowIsNull(Story? entity)
        {
            _entityValidator.ThrowIsNull(entity);
        }

        public void Validate(AddStoryDto? item)
        {
            _storyValidator.Validate(item);
        }

        public void ValidateId(int? id)
        {
            _validatorId.ValidateId(id);
        }

        public void ValidateResult(int result)
        {
           _resultValidator.ValidateResult(result);
        }

        public void ValidateSentencesCount(int polishSentencesCount, int englishSentencesCount)
        {
            _storyValidator.ValidateSentencesCount(polishSentencesCount, englishSentencesCount);
        }
    }
}
