using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Annotations;
using Core.Business.Exceptions;
using Core.Business.Interface;
using Core.Data.Model;
using Core.Data.WebServices.Interfaces;
using Newtonsoft.Json;

namespace Core.Business.Implementation
{
    public class ConnectedStateService : INotifyPropertyChanged
    {
        private bool _forceDisconnectedState = false;
        private IUserWebService _userWebService;

        public ConnectedStateService(IUserWebService userWebService)
        {
            _userWebService = userWebService;
        }

        public bool ForceDisconnectedState
        {
            get { return _forceDisconnectedState; }

            set
            {
                if (value != _forceDisconnectedState)
                {
                    _forceDisconnectedState = value;
                    OnPropertyChanged(nameof(ForceDisconnectedState));
                }
            }
        }

        public async Task<bool> IsServiceAvailable()
        {
            return await _userWebService.Ping() && !_forceDisconnectedState;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}