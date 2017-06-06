using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Data.Model;

namespace Core.Data.Repository.Interface
{
    public interface IVacationRepository
    {
        Task<IEnumerable<Vacation>> GetAll();

        Task<Vacation> Add(Vacation vacation);

        Task<bool> Delete(Vacation vacation);

        Task<bool> Edit(Vacation vacation);

        Task<Vacation> Find(string id);
    }
}
