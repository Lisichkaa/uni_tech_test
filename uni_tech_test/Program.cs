using uni_tech_test.Model;
using SQLitePCL;

namespace uni_tech_test
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                UsersMenu usersMenu = new();
                int mode = usersMenu.ShowMenu();

                DataBaseContext dataBaseContext = new DataBaseContext();
                RemovableDeviceHandler handler = new RemovableDeviceHandler();

                if (mode == 1)
                {
                    List<DriveInfo> drives = handler.GetRemovableDevice();
                    int index = handler.SelectRemovableDevice(drives);
                    if (index >= 0)
                    {
                        handler.TransferFile(drives[index], dataBaseContext);
                    }
                } 
                else if (mode == 2) {
                    return; 
                }
            }
        }
    }
}