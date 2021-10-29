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
    /// Interaction logic for MolkPathPickerWindow.xaml
    /// </summary>
    public partial class MolkPathPickerWindow : Window
    {
        private string archivename { get; set; }
        private string output { get; set; }
        public MolkPathPickerWindow()
        {
            InitializeComponent();
            output = MainWindow.defaultOutString;
            directoryPath.Text = output;
        }

        private void Btn_ClearArchiveName(object sender, RoutedEventArgs e)
        {
            archiveName.Text = "";
        }

        private void Btn_Browse(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                output = dialog.SelectedPath;

                if(output == "" && output == null)
                {
                    output = MainWindow.defaultOutString;
                }
                directoryPath.Text = output;
            }
        }
        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            // Check if user has entered a archive name or not before going forward
            if(archiveName.Text != "")
            {
                // Check so no file with the same archive name already exists on output path to avoid errors
                if (!File.Exists(output + "\\" + archiveName.Text + ".molk"))
                {
                    archivename = archiveName.Text;
                    string path = output + $"\\{archivename}.molk";
                    Molka molka = new Molka();
                    
                    // call for Molk method
                    molka.Molk(MainWindow.finalString, path);
                    Close();
                }
                else
                {
                    MessageBox.Show(GeneralError.fileAlreadyExistsPleaseChange, "Error: Archive name", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show(GeneralError.pickArchiveName, "Error: Archive name", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        Storyboard storyboard;

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            Color background = ((SolidColorBrush)btn.BorderBrush).Color;
            storyboard = new Storyboard();
            storyboard.Children.Add(SetAnimButton(background, btn.Name));
            storyboard.Begin(this);
        }
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            storyboard = new Storyboard();
            storyboard.Children.Add(SetAnimButton(Color.FromRgb(255, 255, 255), btn.Name));
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
    }
}
