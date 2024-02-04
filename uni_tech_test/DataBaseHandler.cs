using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uni_tech_test.Model;

namespace uni_tech_test
{
    public class DataBaseHandler
    {
        private static readonly object dbContextLock = new object();
        public void SaveFile(FileDTO file, DataBaseContext dataBase, string destinationPath)
        {
            CopyFile copyFile = new CopyFile()
            {
                SourcePath = file.SourcePath,

                DestinationPath = destinationPath,

                DateAndTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time")),

                Size = file.Size,

                FileType = file.FileType,

                //Name = file.Name
            };

            lock (dbContextLock)
            {
                dataBase.Add(copyFile);
                dataBase.SaveChanges();
            }
        }
    }
}
