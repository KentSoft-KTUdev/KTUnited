using WebAPI.DataModels;
using DormitoryContract = DataContract.Objects.Dormitory;
using DormitoryInternal = WebAPI.DataModels.Dormitory;

using AdministratorContract = DataContract.Objects.Administrator;
using AdministratorInternal = WebAPI.DataModels.Administrator;

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
                DormitoryId = entity.Dormitory.ID
            };
            return admin;
        }

        public static AdministratorInternal ToInternal(this AdministratorContract entity)
        {
            if (entity == null)
                return null;

            MainDbModelContainer1 db = new MainDbModelContainer1();
            DormitoryInternal adminDormitory = db.DormitorySet.Find(entity.DormitoryId);

            var admin = new AdministratorInternal
            {
                Name = entity.Name,
                PersonalCode = entity.PersonalCode,
                Surname = entity.Surname,
                Dormitory = adminDormitory
            };
            return admin;
        }

        #endregion
    }
}