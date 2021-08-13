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
using FishRandomSelector.Info;

namespace FishRandomSelector.core.Views
{
    /// <summary>
    /// InfoView.xaml 的交互逻辑
    /// </summary>
    public partial class InfoView : Page
    {
        public InfoView()
        {
            InitializeComponent();
            LICENSE.MaxWidth /= 2;
            EULA.MaxWidth /= 2;
            LICENSE.Text = FishRandomSelector.Info.Info.LICENSE;
            EULA.Text = FishRandomSelector.Info.Info.EULA;
            Info1.Text += FishRandomSelector.Info.Info.Productname;
            Info2.Text += FishRandomSelector.Info.Info.Version;
        }
    }
}
