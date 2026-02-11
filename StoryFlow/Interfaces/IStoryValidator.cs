namespace StoryFlow_Shared.Interfaces
{
    public interface IValidator<T> where T : class
    {
        void Validate(T? item);
    }
}
