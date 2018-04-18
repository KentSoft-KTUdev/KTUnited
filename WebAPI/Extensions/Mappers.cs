using DormitoryContract = DataContract.Objects.Dormitory;
using DormitoryInternal = WebAPI.DataModels.Dormitory;

namespace WebAPI.Extensions
{
    public static class Mappers
    {
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
    }
}