using System;
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

        public Molka()
        {
            // Set common attributes
            molk.StartInfo.CreateNoWindow = true;
            molk.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            unMolk.StartInfo.CreateNoWindow = true;
            unMolk.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            molk.StartInfo.UseShellExecute = false;
            unMolk.StartInfo.UseShellExecute = false;

            // Set filename
            molk.StartInfo.FileName = $@"{projectDirectory}\Resources\Data\molk.exe";
            unMolk.StartInfo.FileName = $@"{projectDirectory}\Resources\Data\unmolk.exe";
        }

        /// <summary>
        /// Used to molk (pack) together files into an *.molk file
        /// </summary>
        /// <param name="archiveNamePath"></param>
        /// <param name="filesToMolk"></param>
        public void Molk(string filesToMolk, string path)
        {
            try
            {
                molk.StartInfo.Arguments = $"\"{path}\" {filesToMolk}";
                molk.Start();
                molk.WaitForExit();
                MessageBox.Show(SuccsessMessage.MolkSuccessMessage, Headers.Molkinator, MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow.molkFiles.Clear();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                throw;
            }
        }

        /// <summary>
        /// Used to unmolk (unpack) files
        /// </summary>
        /// <param name="fileToUnMolk"></param>
        /// <param name="outputPath"></param>
        public void UnMolk(string fileToUnMolk, string outputPath)
        {
            // Validate that the file type is .molk
            if (fileToUnMolk.ToLower().Contains(".molk"))
            {
                try
                {
                    unMolk.StartInfo.Arguments = $" -j -b {fileToUnMolk} -d \"{outputPath}\"";
                    unMolk.Start();
                    unMolk.WaitForExit();
                    MessageBox.Show(SuccsessMessage.UnmolkSuccessMessage, Headers.Molkinator, MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow.unMolkFiles.Clear();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                    throw;
                }
            }
            else
            {
                MessageBox.Show(GeneralError.noMolkFilePath, Headers.Molkinator);
            }
        }
        public string GetDefaultOutputPath()
        {
            return defaultOutputPath;
        }
        public void SetDefaultOutputPath(string output)
        {
            defaultOutputPath = output;
        }
    }
}
