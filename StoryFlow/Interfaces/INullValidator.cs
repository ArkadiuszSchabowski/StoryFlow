namespace StoryFlow.Interfaces
{
    public interface INullValidator<T> where T : class
    {
        void ThrowIfNull(T entity);
    }
}
