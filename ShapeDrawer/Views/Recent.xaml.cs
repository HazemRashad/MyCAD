using ShapeDrawer.Models;
using ShapeDrawer.ViewModels;
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

namespace ShapeDrawer.Views
{
    /// <summary>
    /// Interaction logic for Recent.xaml
    /// </summary>
    public partial class Recent : Window
    {
        public Recent(int userid)
        {
            
            InitializeComponent();

            DataContext = new RecentViewModel(userid); // Pass the UserId

        }
    }
}
