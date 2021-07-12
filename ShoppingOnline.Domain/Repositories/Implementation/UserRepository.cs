using ShoppingOnline.Common.Repository;
using ShoppingOnline.Data;
using ShoppingOnline.Domain.Model;

namespace ShoppingOnline.Domain.Repositories.Implementation
{
    public class UserRepository : EntityFrameworkRepository<User>, IUserRepository
    {
        public UserRepository(ShoppingOnlineDBContext dbContext) :base(dbContext)
        {
        }
    }
}
