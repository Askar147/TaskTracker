using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerData.Repositories
{
    public interface IRepository <T>
    {
        public Task<T> Create(T _object);
        public void Delete(T _object);
        public void Update(T _object);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
    }
}
