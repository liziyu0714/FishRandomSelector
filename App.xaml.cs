using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FishRandomSelector
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public bool IsConfigFirstUse = false;
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            FishRandomSelector.core.Info.Names.SavePeople();
        }
    }
}
