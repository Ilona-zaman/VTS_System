using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Business.Exceptions;
using Core.Business.Interface;
using Core.CrossCutting;
using Core.Data.Model;
using Core.Data.Repository.Implementation;
using Core.Data.Repository.Interface;
using Core.Data.WebServices.Interfaces;

namespace Core.Business.Implementation
{
    public class VacationService : IVacationService
    {
        private const int Add = 1;
        private const int Delete = 2;
        private const int Update = 3;
        private const int Synchroniz = 4;

        private readonly IVacationRepository _vacationRepository;
        private readonly IVacationWebService _vacationWebService;
        private readonly ConnectedStateService _сonnectedStateService;

        public VacationService(IVacationRepository repository, IVacationWebService webService,
            ConnectedStateService сonnectedStateService)
        {
            _vacationRepository = repository;
            _vacationWebService = webService;
            _сonnectedStateService = сonnectedStateService;
        }

        public async Task<IEnumerable<Vacation>> GetAllVacations()
        {
            try
            {
                if (await _сonnectedStateService.IsServiceAvailable())
                {
                    await Synchronization();
                }
                return await _vacationRepository.GetAll();
            }
            catch (VTSException e)
            {
                throw new BusinessException("Can't get vacations", e);
            }
        }

        public async Task<Vacation> AddVacation(Vacation vacation)
        {
            try
            {
                if (await _сonnectedStateService.IsServiceAvailable())
                {
                    await Synchronization();
                    vacation.IsSynchroniz = true;
                    vacation.SynchronizStatusId = Synchroniz;
                    await _vacationWebService.CreateVacation(vacation);
                    return await _vacationRepository.Add(vacation);
                }
                vacation.IsSynchroniz = false;
                vacation.SynchronizStatusId = Add;
                return await _vacationRepository.Add(vacation);
            }
            catch (VTSException e)
            {
                throw new BusinessException("Can't add vacation", e);
            }
        }

        public async Task<bool> DeleteVacation(Vacation vacation)
        {
            try
            {
                if (await _сonnectedStateService.IsServiceAvailable())
                {
                    await Synchronization();
                    vacation.IsSynchroniz = true;
                    vacation.SynchronizStatusId = Synchroniz;
                    return await _vacationWebService.DeleteVacations(vacation) && await _vacationRepository.Delete(vacation);
                }
                vacation.IsSynchroniz = false;
                vacation.SynchronizStatusId = Delete;
                return true;
            }
            catch (VTSException)
            {
                return false;
            }
        }

        public async Task<bool> EditVacation(Vacation vacation)
        {
            try
            {
                if (await _сonnectedStateService.IsServiceAvailable())
                {
                    await Synchronization();
                    vacation.IsSynchroniz = true;
                    vacation.SynchronizStatusId = Synchroniz;
                    return await _vacationWebService.UpdateVacation(vacation) && await _vacationRepository.Edit(vacation);
                }

                vacation.IsSynchroniz = false;
                vacation.SynchronizStatusId = Update;
                return await _vacationRepository.Edit(vacation);
            }
            catch (VTSException)
            {
                return false;
            }
           
        }

        public async Task<bool> Synchronization()
        {
            try
            {
                IEnumerable<Vacation> vacations = await _vacationRepository.GetAll();
                IEnumerable<Vacation> vacationsNotSynchroniz = vacations.Where(vacation => vacation.IsSynchroniz == false);
                foreach (var vacation in vacationsNotSynchroniz)
                {
                    if (vacation.SynchronizStatusId == Add)
                    {
                        vacation.IsSynchroniz = true;
                        vacation.SynchronizStatusId = Synchroniz;
                        await _vacationWebService.CreateVacation(vacation);
                        await _vacationRepository.Edit(vacation);
                    }
                    else if (vacation.SynchronizStatusId == Delete)
                    {
                        await _vacationRepository.Delete(vacation);
                        await _vacationWebService.DeleteVacations(vacation);
                    }
                    else if (vacation.SynchronizStatusId == Update)
                    {
                        vacation.IsSynchroniz = true;
                        vacation.SynchronizStatusId = Synchroniz;
                        await _vacationWebService.UpdateVacation(vacation);
                        await _vacationRepository.Edit(vacation);
                    }
                }
                return true;

            }
            catch (VTSException)
            {
                return false;
            }
            
        }
    }
}