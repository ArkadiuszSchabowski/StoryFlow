namespace StoryFlow.Interfaces.Validators
{
    public interface IEntityValidator<T> where T : class
    {
        void ThrowIsNull(T? entity);
    }
}
