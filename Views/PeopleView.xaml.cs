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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FishRandomSelector.core.Info;

namespace FishRandomSelector.core.Views
{
    /// <summary>
    /// PeopleView.xaml 的交互逻辑
    /// </summary>
    public partial class PeopleView : Page
    {
        int nowID = 0;
        public PeopleView()
        {
            InitializeComponent();
        }

        private void LoadPeople(object sender, RoutedEventArgs e)
        {
            ShowPersonGrid.DataContext = Names.GetPerson(0);
        }

        private void Prewperson(object sender, RoutedEventArgs e)
        {
            if (nowID > 0) nowID--;
            ShowPersonGrid.DataContext = Names.GetPerson(nowID);
        }
        private void Nextperson(object sender, RoutedEventArgs e)
        {
            if (nowID < Names.size - 1 ) nowID++;
            ShowPersonGrid.DataContext = Names.GetPerson(nowID);
        }
    }
}
