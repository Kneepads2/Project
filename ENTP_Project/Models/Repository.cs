using ENTP_Project.Models;

namespace Dylan_Tran___Assignment_1.Models
{
    public static class Repository
    {//not used rn
        private static List<RegistrationModel> register = new();
        public static IEnumerable<RegistrationModel> Register => register;
        public static void AddRequest(RegistrationModel requestForm)
        {
            Console.WriteLine(requestForm.ToString());
            //requests.Add(requestForm);
        }

        //public static IEnumerable<Equipment> GetAllEquipment() => equipmentList;

        /*public static IEnumerable<Equipment> GetAvailableEquipment()
        {
            return equipmentList.Where(e => e.IsAvailable);
        }
        */

    }
}
