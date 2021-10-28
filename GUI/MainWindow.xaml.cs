using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //WIP Maybe working, maybe not
        public static ObservableCollection<MolkFile> molkFiles = new ObservableCollection<MolkFile>();
        public static ObservableCollection<MolkFile> unMolkFiles = new ObservableCollection<MolkFile>();

        //
        public static string finalString { get; set; }
        // Declare Molka object
        Molka molka;
        public static string defaultOutString { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            molka = new Molka();
            defaultOutString = molka.GetDefaultOutputPath();
        }

        private void Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }
        private void ButtonPref_Click(object sender, RoutedEventArgs e)
        {
            PrefWindow win2 = new PrefWindow();
            win2.ShowDialog();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonMolk_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.molk.com/");
        }

        Storyboard storyboard;

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            Color background = ((SolidColorBrush)btn.BorderBrush).Color;
            storyboard = new Storyboard();
            storyboard.Children.Add(SetAnimButton(background, btn.Name));
            storyboard.Children.Add(SetAnimCirkle(background));
            storyboard.Begin(this);
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            storyboard = new Storyboard();
            storyboard.Children.Add(SetAnimButton(Color.FromRgb(113, 110, 110), btn.Name));
            storyboard.Children.Add(SetAnimCirkle(Color.FromArgb(150, 67, 67, 67)));
            storyboard.Begin(this);
        }

        public ColorAnimation SetAnimButton(Color Color, string objName)
        {
            ColorAnimation anim = new ColorAnimation();
            anim.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            anim.To = Color;
            Storyboard.SetTargetName(anim, objName);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(Button.Background).(SolidColorBrush.Color)"));
            return anim;
        }

        public ColorAnimation SetAnimCirkle(Color Color)
        {
            ColorAnimation anim = new ColorAnimation();
            anim.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            anim.To = Color;
            Storyboard.SetTargetName(anim, "ColorCirkle");
            Storyboard.SetTargetProperty(anim, new PropertyPath(GradientStop.ColorProperty));
            return anim;
        }
        private void btn_unzip(object sender, RoutedEventArgs e)
        {
            UnMolkWindow umw = new UnMolkWindow();
            umw.ShowDialog();
        }
        private void btn_zip(object sender, RoutedEventArgs e)
        {
            MolkWindow mw = new MolkWindow();
            mw.ShowDialog();
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow.Text = HelpMessage.Help;
            ShowHelp();
        }

        public void ShowHelp()
        {
            if (HelpWindow.Visibility != Visibility.Visible)
            {
                HelpWindow.Visibility = Visibility.Visible;
            }
            else if (HelpWindow.Visibility == Visibility.Visible)
            {
                HelpWindow.Visibility = Visibility.Collapsed;
            }
        }
        public static void OpenExplorerMolkFiles()
        {
            OpenFileDialog openExplorer = new OpenFileDialog();
            openExplorer.Filter = "All files (*.*)|*.*";
            openExplorer.Multiselect = true;
            openExplorer.InitialDirectory = @"c:\";
            openExplorer.ShowDialog();

            try
            {
                if (openExplorer.FileNames != null && openExplorer.FileNames.Length != 0)
                {
                    foreach (string file in openExplorer.FileNames)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(file);

                            // Check if file already exists in list
                            if(molkFiles.Any(x => x.filePath == fi.FullName))
                            {
                                MessageBox.Show(GeneralError.fileAlreadyExist + "\n" + "File: " + fi.Name);
                            }
                            else
                            {
                                // Add file as a MolkFile to molkFiles list
                                molkFiles.Add(new MolkFile
                                {
                                    fileIndex = molkFiles.Count,
                                    fileName = $"{fi.Name}",
                                    fileSize = $"{fi.Length} bytes",
                                    fileCreatedDate = $"{fi.CreationTime}",
                                    fileExtension = $"{fi.Extension}",
                                    filePath = $"\"{fi.FullName}\""
                                });
                                finalString = string.Join(" ", molkFiles.Select(x => x.filePath));
                                Debug.WriteLine(finalString);
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString());
                            throw;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                throw;
            }
        }

        public static void OpenExplorerUnMolkFiles()
        {
            OpenFileDialog openExplorer = new OpenFileDialog();
            openExplorer.Filter = "Mölk files (*.molk)|*.molk";
            openExplorer.Multiselect = true;
            openExplorer.InitialDirectory = @"c:\";
            openExplorer.ShowDialog();

            try
            {
                if (openExplorer.FileNames != null && openExplorer.FileNames.Length != 0)
                {
                    foreach (string file in openExplorer.FileNames)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(file);

                            // Check if file already exists in list
                            if (unMolkFiles.Any(x => x.filePath == fi.FullName))
                            {
                                MessageBox.Show(GeneralError.fileAlreadyExist + "\n" + "File: " + fi.Name);
                            }
                            else
                            {
                                // Add file as a MolkFile to molkFiles list
                                unMolkFiles.Add(new MolkFile
                                {
                                    fileIndex = unMolkFiles.Count,
                                    fileName = $"{fi.Name}",
                                    fileSize = $"{fi.Length} bytes",
                                    fileCreatedDate = $"{fi.CreationTime}",
                                    fileExtension = $"{fi.Extension}",
                                    filePath = $"\"{fi.FullName}\""
                                });
                                finalString = string.Join(" ", unMolkFiles.Select(x => x.filePath));
                                Debug.WriteLine(finalString);
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString());
                            throw;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                throw;
            }
        }
    }
}