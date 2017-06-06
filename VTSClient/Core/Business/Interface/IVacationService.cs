using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Model;

namespace Core.Business.Interface
{
    public interface IVacationService
    {
        Task<IEnumerable<Vacation>> GetAllVacations();
        Task<Vacation> AddVacation(Vacation vacation);
        Task<bool> DeleteVacation(Vacation vacation);
        Task<bool> EditVacation(Vacation vacation);

    }
}
