using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uni_tech_test.Model;

namespace uni_tech_test
{
    public class RemovableDeviceHandler
    {
        private static string destinationDirectory = "arhiv";
        private static string extractionDirectory = "data";
        private static List<string> fileFormats = new List<string> { "mp4", "jpg" };
        private static DirectoryInfo projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent;

        /*private readonly int _mode;
        public RemovableDeviceHandler(int mode)
        {
            _mode = mode;
            ModeHandler(_mode);
        }*/

       /* public void ModeHandler(int mode)
        {
            switch (mode)
            {
                case 1:
                    List<DriveInfo> list = GetRemovableDevice();
                    int index = SelectRemovableDevice(list);
                    Console.WriteLine($"{list[index].RootDirectory}");
                    TransferFile(list[index]);
                    break;
                case 2:
                    Console.WriteLine("exiting");
                    break;

            }
        }*/

        public List<DriveInfo> GetRemovableDevice()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            IEnumerable<DriveInfo> removableDrives = drives.Where(d => d.DriveType == DriveType.Removable);
            return removableDrives.ToList();
        }
        public int SelectRemovableDevice(List<DriveInfo> drives)
        {
            int choice = 0;
            int count = drives.Count;

            if (count > 0)
            {
                int i = 1;
                foreach (DriveInfo drive in drives.Where(d => d.DriveType == DriveType.Removable))
                {
                    Console.WriteLine($"Drive [{i}] {drive.Name} ");
                    i++;
                }

                //Console.WriteLine($"Drive [{i}] - Input your own drive");

                Console.Write("Enter the number of the USB Drive: ");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Not available drives");
            }

            return choice - 1;
        }

        public void TransferFile(DriveInfo drive, DataBaseContext dataBase)
        {
            Console.WriteLine($"Handling USB Drive: {drive.Name}");

            string driveRoot = drive.RootDirectory.Name;
            string dataDirectory = Path.Combine(driveRoot, extractionDirectory);

            if (Directory.Exists(dataDirectory))
            {
                FileHandler fileHandler = new();
                Console.WriteLine("Data directory found");
                List<FileDTO> matchingFiles = fileHandler.GetFilesByFormats(dataDirectory, fileFormats);
                Console.WriteLine($"projectDirectory {projectDirectory}");
                if (matchingFiles.Count > 0)
                {
                    Console.WriteLine("move to archiv");

                    DataBaseHandler dataBaseHandler = new();

                    Parallel.ForEach(matchingFiles, file =>
                    {
                        string sourcePath = Path.Combine(dataDirectory, file.Name);
                        string archivPath = Path.Combine(projectDirectory.ToString(), destinationDirectory);
                        string destinationPath = Path.Combine(archivPath, file.Name);

                        if (!Directory.Exists(destinationPath)) { 
                            Directory.CreateDirectory(archivPath); 
                        }

                        File.Move(sourcePath, destinationPath);
                        Console.WriteLine($"move {file.Name}");

                        // Запись данных в базу данных
                        dataBaseHandler.SaveFile(file, dataBase, destinationPath);
                    });
                }
            }
            else
            {
                Console.WriteLine("Data directory not found");
            }
        }
    }
}