using FishRandomSelector.core.Info;
using FishRandomSelector.Views;
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
        public bool IsReset = false;
        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen s = new SplashScreen(@"Resource\FishRandomSelector.png");
            s.Show(false);
            s.Close(TimeSpan.FromSeconds(1.5));
            base.OnStartup(e);
            if (!System.IO.File.Exists("FishRandomSelector.xml"))
            {
                FirstUse firstUse = new FirstUse();
                firstUse.ShowDialog();
            }
            if (!System.IO.File.Exists("FishRandomSelectorNameList.xml"))
                this.HavePeople = false;
            else
                Names.ReadPeople();
            MainWindow main = new MainWindow();
            Application.Current.MainWindow = main;
            main.Show();
            if (System.IO.File.Exists("FishRandomSelectorNameList.xml"))
                Names.ReadPeople();
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (!IsReset)
            {
                FishRandomSelector.core.Info.Names.SavePeople();
                FishRandomSelector.Settings.UISettings.SaveConfig();
            }

        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (FishRandomSelector.Settings.UISettings.UseSystemDefaultErrorDialog)
            {
                MessageBox.Show("出现了一些奇怪的东西(未经处理的异常):" + e.Exception.Message, "出现了一点“小”问题", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                core.Views.ExceptionShower exception = new core.Views.ExceptionShower(e.Exception);
                exception.ShowDialog();
            }
            this.Shutdown();
        }
    }
}
