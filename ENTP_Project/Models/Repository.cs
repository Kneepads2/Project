using ENTP_Project.Models;

namespace Dylan_Tran___Assignment_1.Models
{
    public static class Repository
    {
        private static List<Registration> register = new();
        public static IEnumerable<Registration> Register => register;
        public static void AddRequest(Registration requestForm)
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
