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
        //YOU CAN INPUT DATA HERE
        private static string destinationDirectory = "arhiv";
        private static string extractionDirectory = "data";
        private static List<string> fileFormats = new List<string> { "mp4", "jpg" };
        private static DirectoryInfo projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent;
        
        
        public List<DriveInfo>? GetRemovableDevice()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            IEnumerable<DriveInfo> removableDrives = drives.Where(d => d.DriveType == DriveType.Removable || d.DriveType == DriveType.CDRom);
            return removableDrives.ToList();
        }

        public int SelectRemovableDevice(List<DriveInfo> drives)
        {
            int choice = 0;

            if (drives.Count > 0)
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
            string driveRoot = drive.RootDirectory.Name;
            string dataDirectory = Path.Combine(driveRoot, extractionDirectory);

            if (Directory.Exists(dataDirectory))
            {
                FileHandler fileHandler = new();
                List<FileDTO> matchingFiles = fileHandler.GetFilesByFormats(dataDirectory, fileFormats);
                
                if (matchingFiles.Count > 0)
                {
                    DataBaseHandler dataBaseHandler = new();

                    Parallel.ForEach(matchingFiles, file =>
                    {
                        string sourcePath = Path.Combine(dataDirectory, file.Name);
                        string archivPath = Path.Combine(projectDirectory.ToString(), destinationDirectory);
                        string destinationPath = Path.Combine(archivPath, file.Name);

                        if (!Directory.Exists(destinationPath)) { 
                            Directory.CreateDirectory(archivPath); 
                        }

                        try
                        {
                            File.Move(sourcePath, destinationPath);
                            Console.WriteLine($"move {file.Name}");
                        }
                        catch (Exception e) {
                            Console.WriteLine($"{e}");
                        }
                        
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