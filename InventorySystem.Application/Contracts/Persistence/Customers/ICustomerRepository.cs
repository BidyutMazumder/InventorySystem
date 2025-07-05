namespace InventorySystem.Application.Contracts.Persistence.Customers;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetAllAsync(int page, int pageSize);
    Task<bool> AddAsync(Customer customer);
    Task<bool> Update(Customer customer);
    Task<bool> Delete(Customer customer); // hard delete
    Task<bool> ExistsAsync(int id);
}


