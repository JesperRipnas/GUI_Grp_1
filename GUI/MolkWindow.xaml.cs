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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MolkWindow.xaml
    /// </summary>
    public partial class MolkWindow : Window
    {
        public MolkWindow()
        {
            InitializeComponent();
            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.Source = MainWindow.molkFiles;
        }

        private void Btn_AddFile(object sender, RoutedEventArgs e)
        {
            MainWindow.OpenExplorerMolkFiles();
        }
        public void RefreshDataGrid()
        {
            dtGrid.Items.Refresh();
        }
    }
}