using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Threading;
using Core.Business.Implementation;
using Core.Business.Interface;
using Core.Business.ViewModels;
using Core.CrossCutting;
using Core.Data;
using Core.Data.Model;
using Core.Data.Repository.Implementation;
using Newtonsoft.Json;
using SQLite;

namespace Tests
{
    public class Program
    {
        
        public static Factory Fuctory { get; set; }

        private static void Main(string[] args)
        {
            try
            {
                Fuctory = new Factory();
                Fuctory.Init();


                var userViewModel = Fuctory.Get<UserViewModel>();
                var vacationViewModel = Fuctory.Get<VacationViewModel>();
                var connectedStateService = Fuctory.Get<ConnectedStateService>();
                var localizationService = Fuctory.Get<ILocalizationService>();

                Console.WriteLine("Please, enter your languege: 1 - English, 2 - Russion");
                Console.WriteLine("Пожалуйста, выберете ваш язык: 1 - Английский, 2 - Русский");
                var languege = Console.ReadLine();
                switch (languege)
                {
                    case "1":
                        localizationService.CultureInfo = new CultureInfo("en");
                        break;
                    case "2":
                        localizationService.CultureInfo = new CultureInfo("ru");
                        break;
                };


                Console.WriteLine(localizationService.Localize("Hello"));

                /*var dbService = Fuctory.Get<DBService>();
                var fileSystemService = Fuctory.Get<FileSystemService>();
                dbService.CreateDatabase(fileSystemService.GetDBPath());*/
                connectedStateService.PropertyChanged += ViewModelOnPropertyChanged;

                Console.WriteLine(localizationService.Localize("ConnectToInternet"));
                var connectionToInternet = Console.ReadLine();
                switch (connectionToInternet)
                {
                    case "1":
                        connectedStateService.ForceDisconnectedState = false;
                        break;
                    case "2":
                        connectedStateService.ForceDisconnectedState = true;
                        break;
                };

                Console.WriteLine(localizationService.Localize("Login"));
                var login = Console.ReadLine();
                Console.WriteLine(localizationService.Localize("Password"));
                var password = Console.ReadLine();
                var isExit = false;

                var isLogin = userViewModel.Login(login, password).Result;
                while (!isLogin)
                {
                    Console.WriteLine(localizationService.Localize("AutorizationFaild"));
                    Console.WriteLine(localizationService.Localize("Login"));
                    login = Console.ReadLine();
                    Console.WriteLine(localizationService.Localize("Password"));
                    password = Console.ReadLine();
                    isLogin = userViewModel.Login(login, password).Result;
                }
                if (isLogin)
                {
                    vacationViewModel.PropertyChanged += ViewModelOnPropertyChanged;

                    vacationViewModel.Init(userViewModel).Wait();


                    Console.WriteLine(localizationService.Localize("SecsessfullLogin"));
                    while (!isExit)
                    {
                        Console.WriteLine(localizationService.Localize("Activity"));
                        isExit = ChooseActive(vacationViewModel, connectedStateService, localizationService);
                    }
                }

                vacationViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
                connectedStateService.PropertyChanged -= ViewModelOnPropertyChanged;

            }
            catch (Exception exception)
            {
                throw new VTSException(exception.Message, exception);
            }
            
        }

        private static void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Console.WriteLine("Property with name " + propertyChangedEventArgs.PropertyName + " was changed.");
        }

        private static bool ChooseActive(VacationViewModel vacationViewModel, ConnectedStateService connectedStateService, ILocalizationService localizationService)
        {
            var activity = Console.ReadLine();
            switch (activity)
            {
                case "1":
                    foreach (var vac in vacationViewModel.Vacation)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(vac));
                    }
                    return false;
                case "2":
                    Console.WriteLine(localizationService.Localize("GetVacation"));
                    for (var i = 0; i < vacationViewModel.Vacation.Count; i++)
                    {
                        Console.WriteLine(i + 1 + " - " + vacationViewModel.Vacation[i].Id);
                    }
                    Console.WriteLine(localizationService.Localize("CanselOperation"));
                    var getVac = Console.ReadLine();
                    if (getVac != "0")
                    {
                        Console.WriteLine(
                            JsonConvert.SerializeObject(
                                vacationViewModel.FindVacation(
                                    vacationViewModel.Vacation[int.Parse(getVac) - 1].Id.ToString())));
                    }
                    return false;
                case "3":
                    Vacation vacation = new Vacation();

                    Console.WriteLine(localizationService.Localize("StartDate"));
                    vacation.Start = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine(localizationService.Localize("EndDate"));
                    vacation.End = DateTime.Parse(Console.ReadLine());
                    
                    Console.WriteLine(localizationService.Localize("VacationType"));
                    var typeNewVac = Console.ReadLine();
                    switch (typeNewVac)
                    {
                        case "1":
                            vacation.VacationTypeId = 1;
                            break;
                        case "2":
                            vacation.VacationTypeId  = 2;
                            break;
                        case "3":
                            vacation.VacationTypeId = 3;
                            break;
                        case "4":
                            vacation.VacationTypeId = 4;
                            break;
                        case "5":
                            vacation.VacationTypeId = 5;
                            break;
                        case "6":
                            vacation.VacationTypeId = 6;
                            break;
                    }
                    Console.WriteLine(localizationService.Localize("VacationStatus"));
                    var statusNewVac = Console.ReadLine();
                    switch (statusNewVac)
                    {
                        case "1":
                            vacation.VacationStatusId = 1;
                            break;
                        case "2":
                            vacation.VacationStatusId = 2;
                            break;
                        case "3":
                            vacation.VacationStatusId = 3;
                            break;
                        case "4":
                            vacation.VacationStatusId = 4;
                            break;
                        case "5":
                            vacation.VacationStatusId = 5;
                            break;
                    }
                    Console.WriteLine(localizationService.Localize("CreatedBy"));
                    vacation.CreatedBy = Console.ReadLine();
                    vacation.Created = DateTime.Now;
                    Console.WriteLine(
                            JsonConvert.SerializeObject(
                                vacationViewModel.AddVacation(vacation).Result));
                    return false;
                case "4":
                    Console.WriteLine(localizationService.Localize("DeleteVacation"));
                    for (var i = 0; i < vacationViewModel.Vacation.Count; i++)
                    {
                        Console.WriteLine(i + 1 + " - " + vacationViewModel.Vacation[i].Id);
                    }
                    Console.WriteLine(localizationService.Localize("CanselOperation"));
                    var detetedVac = Console.ReadLine();
                    if (detetedVac != "0")
                    {
                        Console.WriteLine(
                            JsonConvert.SerializeObject(
                                vacationViewModel.DeleteVacation(
                                    vacationViewModel.Vacation[int.Parse(detetedVac) - 1]).Result));
                    }
                    return false;
                case "5":
                    Console.WriteLine(localizationService.Localize("UpdateVacation"));
                    for (var i = 0; i < vacationViewModel.Vacation.Count; i++)
                    {
                        Console.WriteLine(i + 1 + " - " + vacationViewModel.Vacation[i].Id);
                    }
                    Console.WriteLine(localizationService.Localize("CanselOperation"));
                    var updateVac = Console.ReadLine();
                    if (updateVac != "0")
                    {
                        
                        Console.WriteLine(
                            localizationService.Localize("WhatChange"));
                        var change = Console.ReadLine();
                        switch (change)
                        {
                            case "1":
                                Console.WriteLine(localizationService.Localize("StartDate"));
                                vacationViewModel.Vacation[int.Parse(updateVac) - 1].Start = DateTime.Parse(Console.ReadLine());
                                break;
                            case "2":
                                Console.WriteLine(localizationService.Localize("EndDate"));
                                vacationViewModel.Vacation[int.Parse(updateVac) - 1].End = DateTime.Parse(Console.ReadLine());
                                break;
                            case "3":
                                Console.WriteLine(localizationService.Localize("VacationType"));
                                var typeVac = Console.ReadLine();
                                switch (typeVac)
                                {
                                    case "1":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationTypeId = 1;
                                        break;
                                    case "2":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationStatusId = 2;
                                        break;
                                    case "3":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationTypeId = 3;
                                        break;
                                    case "4":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationStatusId = 4;
                                        break;
                                    case "5":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationTypeId = 5;
                                        break;
                                    case "6":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationStatusId = 6;
                                        break;
                                }
                                break;
                            case "4":
                                Console.WriteLine(localizationService.Localize("VacationStatus"));
                                var statusVac = Console.ReadLine();
                                switch (statusVac)
                                {
                                    case "1":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationStatusId = 1;
                                        break;
                                    case "2":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationStatusId = 2;
                                        break;
                                    case "3":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationStatusId = 3;
                                        break;
                                    case "4":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationStatusId = 4;
                                        break;
                                    case "5":
                                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].VacationStatusId = 5;
                                        break;
                                }
                                break;
                            case "5":
                                Console.WriteLine(localizationService.Localize("CreatedBy"));
                                vacationViewModel.Vacation[int.Parse(updateVac) - 1].CreatedBy = Console.ReadLine();
                                break;
                        }

                        vacationViewModel.Vacation[int.Parse(updateVac) - 1].Created  = DateTime.Now;

                        Console.WriteLine(
                            JsonConvert.SerializeObject(
                                vacationViewModel.UpdateVacation(
                                    vacationViewModel.Vacation[int.Parse(updateVac) - 1]).Result));
                    }
                    return false;
                case "6":
                    if (connectedStateService.ForceDisconnectedState)
                    {
                        Console.WriteLine(localizationService.Localize("WantConnectToInternet"));
                        var connectionToInterne = Console.ReadLine();
                        switch (connectionToInterne)
                        {
                            case "1":
                                connectedStateService.ForceDisconnectedState = false;
                                break;
                            case "2":
                                connectedStateService.ForceDisconnectedState = true;
                                break;
                        };
                    }
                    else
                    {
                        Console.WriteLine(localizationService.Localize("WantDisconnectToInternet"));
                        var connectionToInterne = Console.ReadLine();
                        switch (connectionToInterne)
                        {
                            case "1":
                                connectedStateService.ForceDisconnectedState = true;
                                break;
                            case "2":
                                connectedStateService.ForceDisconnectedState = false;
                                break;
                        };
                    }
                    break;
                case "7":
                    return true;
            }
            return false;
        }
    }
}