using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GUI
{
    /// <summary>
    /// Interaction logic for PrefWindow.xaml
    /// </summary>
    public partial class PrefWindow : Window
    {
        Molka molka;
        Storyboard storyboard;

        public string Temp { get; set; }

        public PrefWindow()
        {
            InitializeComponent();
            molka = new Molka();
            InfoBox.Text = molka.GetDefaultOutputPath();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                InfoBox.Text = dialog.SelectedPath;
                if (dialog.SelectedPath == "")
                {
                    Temp = molka.GetDefaultOutputPath();
                    InfoBox.Text = Temp;
                }
            }
        }
    }
}
