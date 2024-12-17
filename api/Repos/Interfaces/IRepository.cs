namespace api.Repos.Interfaces
{
    public interface IRepository<T>
    {
        Task<T?> FindById(Guid id);
        Task<List<T>> FindAll();
        Task<T?> Create(T target);
        Task<T?> Update(Guid id, T data);
        Task<T?> Delete(Guid id);
    }
}