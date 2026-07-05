using StoryFlow.Exceptions;
using StoryFlow.Interfaces.Validators;
using StoryFlow_Database.Entities;

namespace StoryFlow.Validators
{
    public class UserEntityValidator : IEntityValidator<User>
    {
        public void ThrowIsNull(User? entity)
        {
            if (entity is null)
            {
                throw new NotFoundException("Nie znaleziono użytkownika.");
            }
        }
    }
}
