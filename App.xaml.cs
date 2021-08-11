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
        public bool HavePeople = true;
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            FishRandomSelector.core.Info.Names.SavePeople();
            FishRandomSelector.Settings.UISettings.SaveConfig();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if(FishRandomSelector.Settings.UISettings.UseSystemDefaultErrorDialog)
            {
                MessageBox.Show("出现了一些奇怪的东西(未经处理的异常):" + e.Exception.Message, "出现了一点“小”问题", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
