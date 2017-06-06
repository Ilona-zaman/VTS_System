using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Model;
using SQLite;

namespace Core.Business.Implementation
{
    public class DBService
    {
        public void CreateDatabase(string path)
        {
            try
            {
                SQLiteConnection sqLiteConnection = new SQLiteConnection(path);

                sqLiteConnection.CreateTable<User>();
                sqLiteConnection.CreateTable<VacationStatus>();
                sqLiteConnection.CreateTable<VacationType>();
                sqLiteConnection.CreateTable<SynchronizStatus>();
                sqLiteConnection.CreateTable<Vacation>();

                sqLiteConnection.DeleteAll<User>();
                sqLiteConnection.DeleteAll<VacationStatus>();
                sqLiteConnection.DeleteAll<VacationType>();
                sqLiteConnection.DeleteAll<SynchronizStatus>();
                sqLiteConnection.DeleteAll<Vacation>();

                SynchronizStatus synchronizStatus = new SynchronizStatus();
                synchronizStatus.Name = "Add";
                sqLiteConnection.Insert(synchronizStatus);
                synchronizStatus.Name = "Delete";
                sqLiteConnection.Insert(synchronizStatus);
                synchronizStatus.Name = "Update";
                sqLiteConnection.Insert(synchronizStatus);
                synchronizStatus.Name = "Synchroniz";
                sqLiteConnection.Insert(synchronizStatus);

                VacationType vacationType = new VacationType();
                vacationType.Name = "Undefined";
                sqLiteConnection.Insert(vacationType);
                vacationType.Name = "Regular";
                sqLiteConnection.Insert(vacationType);
                vacationType.Name = "Sick";
                sqLiteConnection.Insert(vacationType);
                vacationType.Name = "Exceptional";
                sqLiteConnection.Insert(vacationType);
                vacationType.Name = "LeaveWithoutPay";
                sqLiteConnection.Insert(vacationType);
                vacationType.Name = "Overtime";
                sqLiteConnection.Insert(vacationType);

                VacationStatus vacationStatus = new VacationStatus();
                vacationStatus.Name = "Draft";
                sqLiteConnection.Insert(vacationStatus);
                vacationStatus.Name = "Submitted";
                sqLiteConnection.Insert(vacationStatus);
                vacationStatus.Name = "Approved";
                sqLiteConnection.Insert(vacationStatus);
                vacationStatus.Name = "InProgress";
                sqLiteConnection.Insert(vacationStatus);
                vacationStatus.Name = "Closed";
                sqLiteConnection.Insert(vacationStatus);

                User user = new User();
                user.Login = "ark";
                user.Password = "123";
                sqLiteConnection.Insert(user);

                var req1 = new Vacation
                {
                    Id = new Guid("35055cea-59a5-43df-9d84-9f2bc8401008"),
                    Start = new DateTime(2017, 3, 20),
                    End = new DateTime(2017, 3, 30),
                    VacationTypeId = 1,
                    VacationStatusId = 3,
                    CreatedBy = "Someone",
                    Created = DateTime.Now,
                    IsSynchroniz = true,
                    SynchronizStatusId = 4
                };
                sqLiteConnection.Insert(req1);

                var req2 = new Vacation
                {
                    Id = new Guid("16954f5a-87cf-4030-8897-c8abe2e6d516"),
                    Start = new DateTime(2016, 12, 26),
                    End = new DateTime(2016, 12, 30),
                    VacationTypeId = 1,
                    VacationStatusId = 5,
                    CreatedBy = "Someone",
                    Created = DateTime.UtcNow,
                    IsSynchroniz = true,
                    SynchronizStatusId = 4
                };
                sqLiteConnection.Insert(req2);

                var req3 = new Vacation
                {
                    Id = new Guid("075bbbd8-4a50-4af0-beb7-96e89fc4f885"),
                    Start = new DateTime(2016, 11, 2),
                    End = new DateTime(2016, 11, 4),
                    VacationTypeId = 2,
                    VacationStatusId = 5,
                    CreatedBy = "Someone",
                    Created = DateTime.UtcNow,
                    IsSynchroniz = true,
                    SynchronizStatusId = 4
                };
                sqLiteConnection.Insert(req3);

                var req4 = new Vacation
                {
                    Id = new Guid("66d7d908-cb58-4fff-9a71-1067e148bdf5"),
                    Start = new DateTime(2016, 7, 11),
                    End = new DateTime(2016, 7, 13),
                    VacationTypeId = 3,
                    VacationStatusId = 5,
                    CreatedBy = "Someone",
                    Created = DateTime.UtcNow,
                    IsSynchroniz = true,
                    SynchronizStatusId = 4
                };
                sqLiteConnection.Insert(req4);

                var req5 = new Vacation()
                {
                    Id = new Guid("b3d47866-6ccb-46bc-8006-2b6ee7b7f168"),
                    Start = new DateTime(2016, 2, 6),
                    End = new DateTime(2016, 2, 7),
                    VacationTypeId = 5,
                    VacationStatusId = 5,
                    CreatedBy = "Someone",
                    Created = DateTime.UtcNow,
                    IsSynchroniz = true,
                    SynchronizStatusId = 4
                };
                sqLiteConnection.Insert(req5);
            }
            catch (Exception exception)
            {
                throw exception.InnerException;
            }
            

            
        }
    }
}

