using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uni_tech_test.Model
{
    public class CopyFile
    {
        public int Id { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public DateTime DateAndTime { get; set; }
        public long Size { get; set; }
        public string FileType { get; set; }
    }
}
