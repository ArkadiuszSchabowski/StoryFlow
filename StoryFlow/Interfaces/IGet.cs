namespace StoryFlow_Shared.Interfaces
{
    public interface IGet<T> where T : class
    {
        void Get(ICollection<T> collection);
    }
}
