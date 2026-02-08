using stock_fincance_api.DTOs;
using stock_fincance_api.Models;

namespace stock_fincance_api.Repositoy
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id );
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int Id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int Id);


    }
}
