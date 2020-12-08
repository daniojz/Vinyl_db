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

namespace Vinyl_db.View
{
    /// <summary>
    /// Lógica de interacción para EditAlbumWindow.xaml
    /// </summary>
    public partial class EditAlbumWindow : Window
    {
        public ViewModel.EditAlbumWindowModel dataContext;

        public EditAlbumWindow(Vinyl_db.ViewModel.MainWindowModel MainWindowModel)
        {
            InitializeComponent();

            dataContext = new ViewModel.EditAlbumWindowModel(MainWindowModel);
            DataContext = dataContext;

        }
    }
}
