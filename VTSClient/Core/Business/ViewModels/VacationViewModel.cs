using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Annotations;
using Core.Business.Exceptions;
using Core.Business.Implementation;
using Core.Business.Interface;
using Core.CrossCutting;
using Core.Data.Model;

namespace Core.Business.ViewModels
{
    public class VacationViewModel : INotifyPropertyChanged
    {
        private UserViewModel _userViewModel;
        private readonly IVacationService _vacationService;

        public VacationViewModel(IVacationService vacationService)
        {
            if (vacationService == null) throw new ArgumentNullException(nameof(vacationService));
            _vacationService = vacationService;
        }

        public ObservableCollection<Vacation> Vacation { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task Init(UserViewModel userViewModel)
        {
            try
            {
                _userViewModel = userViewModel;
                if (userViewModel.IsLogin)
                {
                    Vacation = new ObservableCollection<Vacation>(await _vacationService.GetAllVacations());
                }
            }
            catch (VTSException e)
            {
                throw new BusinessException("Can't get vacations", e);
            }
        }

        public Task Init()
        {
            throw new NotImplementedException();
        }

        public async Task<Vacation> AddVacation(Vacation vacation)
        {
            try
            {
                if (_userViewModel.IsLogin)
                {
                    var vac = await _vacationService.AddVacation(vacation);
                    Vacation.Add(vac);
                    OnPropertyChanged(nameof(Vacation));
                    return vac;
                }
                return null;
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
                if (_userViewModel.IsLogin)
                {
                    var isDeleded = await _vacationService.DeleteVacation(vacation);
                    if (isDeleded)
                    {
                        Vacation.Remove(Vacation.First(vac => vac.Id == vacation.Id));
                        OnPropertyChanged(nameof(Vacation));
                    }
                    return isDeleded;
                }
                return false;
            }
            catch (VTSException)
            {
                return false;
            }
            
        }

        public async Task<bool> UpdateVacation(Vacation vacation)
        {
            try
            {
                if (_userViewModel.IsLogin)
                {
                    var isUpdated = await _vacationService.EditVacation(vacation);
                    if (isUpdated)
                    {
                        Vacation.Remove(Vacation.First(vac => vac.Id == vacation.Id));
                        Vacation.Add(vacation);
                        OnPropertyChanged(nameof(Vacation));
                    }
                    return isUpdated;
                }
                return false;

            }
            catch (VTSException)
            {
                return false;
            }
            
        }

        public Vacation FindVacation(string id)
        {
            return Vacation.First(vac => vac.Id.ToString() == id);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}