using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uni_tech_test
{
    public class FileDTO
    {
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public long Size { get; set; }
        public string FileType { get; set; }
    }

    public class FileHandler
    {
        public List<FileDTO> GetFilesByFormats(string path, List<string> fileFormats)
        {
            List<FileDTO> matchingFiles = new List<FileDTO>();

            foreach (string fileFormat in fileFormats)
            {
                string[] files = Directory.GetFiles(path, $"*.{fileFormat}", SearchOption.TopDirectoryOnly);

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    FileDTO fileDetails = new FileDTO
                    {
                        Name = fileInfo.Name,
                        SourcePath = fileInfo.DirectoryName,
                        Size = fileInfo.Length/1000,
                        FileType = fileFormat
                    };

                    matchingFiles.Add(fileDetails);
                }
            }

            return matchingFiles;
        }

    }
}
