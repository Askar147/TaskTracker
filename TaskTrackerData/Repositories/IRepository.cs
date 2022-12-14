namespace TaskTrackerData.Repositories
{
    public interface IRepository <T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<T> Create(T _object);
        public void Update(T _object);
        public Task Delete(T _object);
    }
}
