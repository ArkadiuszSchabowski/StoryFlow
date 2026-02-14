namespace StoryFlow.Interfaces
{
    public interface IEntityValidator<T> where T : class
    {
        void ThrowIsNull(T? entity);
    }
}
