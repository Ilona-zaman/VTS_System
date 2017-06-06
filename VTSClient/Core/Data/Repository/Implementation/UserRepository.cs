using System.Threading.Tasks;
using Core.Data.Helpers;
using Core.Data.Model;
using Core.Data.Repository.Interface;

namespace Core.Data.Repository.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IFileHelper fileHelper) : base(fileHelper)
        {
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            var user =
                await
                    Connection.Table<User>()
                        .Where(i => i.Login == loginModel.Login && i.Password == loginModel.Password)
                        .FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}