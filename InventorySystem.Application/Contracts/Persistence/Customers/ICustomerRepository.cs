namespace InventorySystem.Application.Contracts.Persistence.Customers;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync(int page, int pageSize);
    Task AddAsync(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}

