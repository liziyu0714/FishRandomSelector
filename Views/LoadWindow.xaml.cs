using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace FishRandomSelector.Views
{
    /// <summary>
    /// LoadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoadWindow : Window
    {
        Thread LoadAndChangeText = null;
        MainWindow main = new MainWindow();
        public LoadWindow()
        {
            InitializeComponent();
            
        }
        private void LoadWindow_OtherThread()
        {
            #region 检测是否是第一次使用
            if(!System.IO.File.Exists("FishRandomSelector.xml"))
            {
                this.Dispatcher.Invoke(() =>
                {
                    FirstUse firstUse = new FirstUse();
                    Application.Current.MainWindow = firstUse;
                    Application.Current.MainWindow.Show();
                    this.Close();
                });
            }
            #endregion
            LoadInfo.Dispatcher.Invoke(() => { LoadInfo.Text = "正在验证应用可用性..."; });
            #region 验证应用可用性
            Tools.Result result = Tools.Verify.Verify_Online();
            if(result.Can_use)
                Check1.Dispatcher.Invoke(() => { Check2.IsChecked = true; });
            else
                Check1.Dispatcher.Invoke(() => { Check2.IsChecked = false; Check2.Background = Brushes.Red;Check2.Content = "!"; });
            #endregion
            LoadInfo.Dispatcher.Invoke(() => { LoadInfo.Text = "验证完成"; });      
            Thread.Sleep(500);
            //TODO:编写加载名单的逻辑
            Thread.Sleep(1000);
            Check1.Dispatcher.Invoke(() => { Check3.IsChecked = true; }); 
            Thread.Sleep(1000);
            //TODO:编写其他加载项
            Check1.Dispatcher.Invoke(() => { Check4.IsChecked = true; }); 
            this.Dispatcher.Invoke(() =>
            {
                Application.Current.MainWindow = main;
                Application.Current.MainWindow.Show();
                this.Close();
            });
            
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadAndChangeText = new Thread(LoadWindow_OtherThread);
            LoadAndChangeText.Start();
            
            
        }
    }
}
