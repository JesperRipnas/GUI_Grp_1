using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace GUI
{
    public class File
    {
        public int fileIndex { get; set; }
        public string fileName { get; set; }
        public string fileSize { get; set; }
        public string filePath { get; set; }
        public string fileExtension { get; set; }
        public string fileCreatedDate { get; set; }
    }
}