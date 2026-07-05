using StoryFlow.Exceptions;
using StoryFlow.Interfaces.Validators;
using StoryFlow_Database.Entities;

namespace StoryFlow.Validators
{
    public class StoryEntityValidator : IEntityValidator<Story>
    {
        public void ThrowIsNull(Story? entity)
        {
            if (entity is null)
            {
                throw new NotFoundException("Nie znaleziono historii.");
            }
        }
    }
}
