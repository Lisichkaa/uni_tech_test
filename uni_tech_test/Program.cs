using uni_tech_test.Model;
using SQLitePCL;

namespace uni_tech_test
{
    class Program
    {       
        static void Main(string[] args)
        {
            UsersMenu usersMenu = new();
            int mode = usersMenu.ShowMenu();

            DataBaseContext dataBaseContext = new DataBaseContext();
            RemovableDeviceHandler handler = new RemovableDeviceHandler();

            if (mode == 1)
            {
                List<DriveInfo> drives = handler.GetRemovableDevice();
                int index = handler.SelectRemovableDevice(drives);
                //handler.TransferFile(drives[index], dataBaseContext);
            }
        }
    }
}