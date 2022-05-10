using System;
namespace XForms.Models
{
    public partial class File
    {
        public string Path { get; set; }
        public string Extension { get; set; }
        public byte[] FileByte { get; set; }
        public System.IO.Stream Stream { get; set; }
        public Guid InstanceId;
    }
}
