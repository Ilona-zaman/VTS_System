using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Model;

namespace Core.Data.WebServices.Interfaces
{
    public interface IVacationWebService
    {
        Task<Vacation> CreateVacation(Vacation vacation);
        Task<bool> UpdateVacation(Vacation vacation);
        Task<IEnumerable<Vacation>> GetVacations();
        Task<bool> DeleteVacations(Vacation vacation);
        Task<Vacation> GetVacation(string id);
    }
}
