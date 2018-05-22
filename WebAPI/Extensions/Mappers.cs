using WebAPI.DataModels;

#region Mapping Declarations
using DormitoryContract = DataContract.Objects.Dormitory;
using DormitoryInternal = WebAPI.DataModels.Dormitory;
using AdministratorContract = DataContract.Objects.Administrator;
using AdministratorInternal = WebAPI.DataModels.Administrator;
using GuardContract = DataContract.Objects.Guard;
using GuardInternal = WebAPI.DataModels.Guard;
using GuestContract = DataContract.Objects.Guest;
using GuestInternal = WebAPI.DataModels.Guest;
using ResidentContract = DataContract.Objects.Resident;
using ResidentInternal = WebAPI.DataModels.Resident;
using RoomContract = DataContract.Objects.Room;
using RoomInternal = WebAPI.DataModels.Room;
using VisitContract = DataContract.Objects.Visit;
using VisitInternal = WebAPI.DataModels.Visit;
#endregion

namespace WebAPI.Extensions
{
    public static class Mappers
    {
        #region Dormitory
        public static DormitoryContract ToContract(this DormitoryInternal entity)
        {
            if (entity == null)
                return null;

            var dorm = new DormitoryContract
            {
                ID = entity.ID,
                Adress = entity.Adress,
                Name = entity.Name
            };
            return dorm;
        }

        public static DormitoryInternal ToInternal(this DormitoryContract entity)
        {
            if (entity == null)
                return null;

            var dorm = new DormitoryInternal
            {
                ID = entity.ID,
                Adress = entity.Adress,
                Name = entity.Name
            };
            return dorm;
        }
        #endregion

        #region Administrator
        public static AdministratorContract ToContract(this AdministratorInternal entity)
        {
            if (entity == null)
                return null;

            var admin = new AdministratorContract
            {
                Name = entity.Name,
                PersonalCode = entity.PersonalCode,
                Surname = entity.Surname,
                DormitoryId = entity.Dormitory.ID,
                Username = entity.Username,
                Password = entity.Password
            };
            return admin;
        }

        public static AdministratorInternal ToInternal(this AdministratorContract entity)
        {
            if (entity == null)
                return null;
            DormitoryInternal adminDormitory = new DormitoryInternal();
            using(MainDbModelContainer1 db = new MainDbModelContainer1())
            {
                adminDormitory = db.DormitorySet.Find(entity.DormitoryId);
            }
            var admin = new AdministratorInternal
            {
                Name = entity.Name,
                PersonalCode = entity.PersonalCode,
                Surname = entity.Surname,
                Dormitory = adminDormitory,
                Username = entity.Username,
                Password = entity.Password
            };
            return admin;
        }
        #endregion

        #region Guard
        public static GuardContract ToContract(this GuardInternal entity)
        {
            if (entity == null)
                return null;

            var guard = new GuardContract
            {
                Name = entity.Name,
                PersonalCode = entity.PersonalCode,
                Surname = entity.Surname,
                DormitoryId = entity.Dormitory.ID,
                Username = entity.Username,
                Password = entity.Password
            };
            return guard;
        }

        public static GuardInternal ToInternal(this GuardContract entity)
        {
            if (entity == null)
                return null;
            DormitoryInternal guardDormitory = new DormitoryInternal();
            using (MainDbModelContainer1 db = new MainDbModelContainer1())
            {
                guardDormitory = db.DormitorySet.Find(entity.DormitoryId);
            }

            var guard = new GuardInternal
            {
                Name = entity.Name,
                PersonalCode = entity.PersonalCode,
                Surname = entity.Surname,
                Dormitory = guardDormitory,
                Username = entity.Username,
                Password = entity.Password
            };
            return guard;
        }
        #endregion

        #region Guest
        public static GuestContract ToContract(this GuestInternal entity)
        {
            if (entity == null)
                return null;

            var guest = new GuestContract
            {
                Name = entity.Name,
                Surname = entity.Surname,
                PersonalCode = entity.PersonalCode
            };
            return guest;
        }

        public static GuestInternal ToInternal(this GuestContract entity)
        {
            if (entity == null)
                return null;

            var guest = new GuestInternal
            {
                Name = entity.Name,
                Surname = entity.Surname,
                PersonalCode = entity.PersonalCode
            };
            return guest;
        }
        #endregion

        #region Room
        public static RoomContract ToContract(this RoomInternal entity)
        {
            if (entity == null)
                return null;

            var room = new RoomContract
            {
                ID = entity.ID,
                Number = entity.Number,
                DormitoryId = entity.Dormitory.ID
            };
            return room;
        }

        public static RoomInternal ToInternal(this RoomContract entity)
        {
            if (entity == null)
                return null;
            DormitoryInternal roomDormitory = new DormitoryInternal();
            using (MainDbModelContainer1 db = new MainDbModelContainer1())
            {
                roomDormitory = db.DormitorySet.Find(entity.DormitoryId);
            }

            var room = new RoomInternal
            {
                ID = entity.ID,
                Number = entity.Number,
                Dormitory = roomDormitory
            };
            return room;
        }
        #endregion

        #region Resident
        public static ResidentContract ToContract(this ResidentInternal entity)
        {
            if (entity == null)
                return null;

            var resi = new ResidentContract
            {
                Name = entity.Name,
                PersonalCode = entity.PersonalCode,
                Surname = entity.Surname,
                DormitoryId = entity.Dormitory.ID,
                RoomId = entity.Room.ID,
                Username = entity.Username,
                Password = entity.Password
            };
            return resi;
        }

        public static ResidentInternal ToInternal(this ResidentContract entity)
        {
            if (entity == null)
                return null;
            DormitoryInternal resiDormitory = new DormitoryInternal();
            RoomInternal resiRoom = new RoomInternal();
            using (MainDbModelContainer1 db = new MainDbModelContainer1())
            {
                resiDormitory = db.DormitorySet.Find(entity.DormitoryId);
                resiRoom = db.RoomSet.Find(entity.RoomId);
            }

            var resi = new ResidentInternal
            {
                Name = entity.Name,
                PersonalCode = entity.PersonalCode,
                Surname = entity.Surname,
                Dormitory = resiDormitory,
                Room = resiRoom,
                Username = entity.Username,
                Password = entity.Password
            };
            return resi;
        }
        #endregion

        #region Visit
        public static VisitContract ToContract(this VisitInternal entity)
        {
            if (entity == null)
                return null;

            var visit = new VisitContract
            {
                ID = entity.ID,
                VisitRegDateTime = entity.VisitRegDateTime,
                IsOver = entity.IsOver,
                VisitEndDateTime = entity.VisitEndDateTime,
                ResidentId = entity.Resident.PersonalCode,
                GuardId = entity.Guard.PersonalCode,
                DormitoryId = entity.Dormitory.ID,
                GuestId = entity.Guest.PersonalCode,
                IsConfirmed = entity.IsConfirmed
            };
            return visit;
        }

        public static VisitInternal ToInternal(this VisitContract entity)
        {
            if (entity == null)
                return null;
            ResidentInternal visitResident = new ResidentInternal();
            GuardInternal visitGuard = new GuardInternal();
            DormitoryInternal visitDormitory = new DormitoryInternal();
            GuestInternal visitGuest = new GuestInternal();
            using (MainDbModelContainer1 db = new MainDbModelContainer1())
            {
                visitResident = db.ResidentSet.Find(entity.ResidentId);
                visitGuard = db.GuardSet.Find(entity.GuardId);
                visitDormitory = db.DormitorySet.Find(entity.DormitoryId);
                visitGuest = db.GuestSet.Find(entity.GuestId);
            }
            
            var visit = new VisitInternal
            {
                ID = entity.ID,
                VisitRegDateTime = entity.VisitRegDateTime,
                IsOver = entity.IsOver,
                VisitEndDateTime = entity.VisitEndDateTime,
                ResidentPersonalCode = visitResident.PersonalCode,
                GuardPersonalCode = visitGuard.PersonalCode,
                DormitoryID = visitDormitory.ID,
                Guest_PersonalCode = visitGuest.PersonalCode,
                IsConfirmed = entity.IsConfirmed,
                Resident = visitResident,
                Guard = visitGuard,
                Dormitory = visitDormitory,
                Guest = visitGuest
            };
            return visit;
        }
        #endregion
    }
}