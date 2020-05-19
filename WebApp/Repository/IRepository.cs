using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Repository
{
    public interface IRepository
    {

        Task<IList<Todo>> GetAllAsync();
        
        Task AddTodoAsync(Todo todo);
    }
}