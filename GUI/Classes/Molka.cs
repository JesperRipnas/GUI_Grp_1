using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.IO;

namespace GUI
{
    class Molka
    {
        // Create processes for molk & unmolk
        private Process molk = new Process();
        private Process unMolk = new Process();
        private string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

        // For now, set default output folder to desktop
        private string defaultOutputPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        /// <summary>
        /// 
        /// </summary>
        public Molka()
        {
            // Set common attributes
            molk.StartInfo.CreateNoWindow = false;
            molk.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            unMolk.StartInfo.CreateNoWindow = true;

            molk.StartInfo.UseShellExecute = false;
            unMolk.StartInfo.UseShellExecute = false;

            // Set filename
            molk.StartInfo.FileName = $@"{projectDirectory}\Resources\Data\molk.exe";
            unMolk.StartInfo.FileName = $@"{projectDirectory}\Resources\Data\unmolk.exe";
        }

        /// <summary>
        /// Used to molk together files into an *.molk file
        /// </summary>
        /// <param name="archiveNamePath"></param>
        /// <param name="filesToMolk"></param>
        public void Molk(string filesToMolk, string archiveNamePath)
        {
            try
            {
                molk.StartInfo.Arguments = $"{archiveNamePath} {filesToMolk}";
                molk.Start();
                molk.WaitForExit();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                throw;
            }
        }

        public void UnMolk(string fileToUnMolk, string outputPath = null)
        {
            // Check if we got a different output path
            if (outputPath == null) outputPath = defaultOutputPath;

            // Validate that the file type is .molk
            // We should split this and check each element in case a multifile string is passed NIY
            if (fileToUnMolk.ToLower().Contains(".molk")) 
            {
                MessageBox.Show("Filepath contains .molk");
                try
                {
                    molk.StartInfo.Arguments = $"{fileToUnMolk} -d {outputPath}";
                    molk.Start();
                    molk.WaitForExit();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Filepath doesn't contain .molk");
            }
        }
    }
}
