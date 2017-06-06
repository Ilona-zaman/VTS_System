using System.Threading.Tasks;
using Core.Data.Helpers;
using Core.Data.Model;
using Core.Data.Repository.Interface;

namespace Core.Data.Repository.Implementation
{
    public class VacationRepository : Repository<Vacation>, IVacationRepository
    {
        public VacationRepository(IFileHelper fileHelper) : base(fileHelper)
        {
        }

        public async Task<Vacation> Find(string id)
        {
            return await Connection.Table<Vacation>().Where(i => i.Id.ToString() == id).FirstOrDefaultAsync();
        }
    }
}