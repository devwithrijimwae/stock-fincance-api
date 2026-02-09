using stock_fincance_api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stock_fincance_api.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);

    }
}
