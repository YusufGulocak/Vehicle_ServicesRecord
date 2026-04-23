namespace App.Repositories.User
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Task<List<Users>> GetAllAsync();
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
        Task<Users?> GetByUsernameOrEmailAsync(string value);
    }
}
