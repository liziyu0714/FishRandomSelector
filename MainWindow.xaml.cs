using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace FishRandomSelector
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsLeftAreaOpen = false;
        private bool ButtonCanUse = true;
        App app = (App)Application.Current;

        public bool IsLeftAreaOpen1 { get => IsLeftAreaOpen; set => IsLeftAreaOpen = value; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            //MainWindowVerify();
            //让窗口到最上层
            this.Topmost = true;
            this.Topmost = false;
        }
        /*private void MainWindowVerify()
        {
            VerifyDialog.IsOpen = true;
            Tools.Result result = Tools.Verify.Verify_Online();
            if (result.Can_use)
            {

                VerifyDialog.IsOpen = false;
            }
            else
            {
                VerifyDialog.IsOpen = false;
                VerifyfailedDialog.IsOpen = true;
            }
        }*/

        private void VerifyfailedButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenLeftAreaButton_Click(object sender, RoutedEventArgs e)
        {
            GridLength full = new GridLength(this.Width - 15);
            GridLength none = new GridLength(0);
            if (IsLeftAreaOpen)
            {
                LeftArea.Width = none;
                IsLeftAreaOpen = false;
            }
            else
            {
                LeftArea.Width = full;
                IsLeftAreaOpen = true;
            }
        }
        private void ChangeSelectButtonState()
        {
            if (ButtonCanUse)
            {
                SelectButton.Cursor = Cursors.Wait;
                SelectButton.IsEnabled = false;
                ButtonCanUse = false;
            }
            else
            {
                SelectButton.Cursor = Cursors.Hand;
                SelectButton.IsEnabled = true;
                ButtonCanUse = true;
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if(!app.HavePeople)
            {
                MainText.Text = "无名单";
                return;
            }
            GridLength none = new GridLength(0);
            if (Settings.UISettings.CloseLeftAreaWhenClick)
            {
                LeftArea.Width = none;
                IsLeftAreaOpen = false;
            }
            if (Settings.UISettings.UseAnimation)
            {
                Thread ChangeTextThread = new Thread(ChangeText);
                var taskbarinfo = new TaskbarItemInfo();
                taskbarinfo.ProgressValue = 0.5;
                taskbarinfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Error;
                ChangeTextThread.Start();
                MainText.Text = FishRandomSelector.core.Info.Names.GetPersonByValue();
            }
            else
            {
                MainText.Text = FishRandomSelector.core.Info.Names.GetPersonByValue();
            }
            
        }
        private void ChangeText()
        {
            this.Dispatcher.Invoke(ChangeSelectButtonState);
            Random timesRandom = new Random();
            int Time = timesRandom.Next(Settings.UISettings.MinChangeTextTimes, Settings.UISettings.MaxChangeTextTimes);
            for (int i = 0; i < Time; i++)
            {
                Thread.Sleep(Settings.UISettings.TimeBetweenChangeText);
                this.Dispatcher.Invoke(() => { MainText.Text = core.Info.Names.GetARandomName(); });
            }
            this.Dispatcher.Invoke(ChangeSelectButtonState);
        }
        private void ChangeUseAnimation(object sender, RoutedEventArgs e)
        {
            if ((bool)IsUsingAnimation.IsChecked)
            {
                Settings.UISettings.UseAnimation = true;
            }
            else
            {
                Settings.UISettings.UseAnimation = false;
            }
        }

        private void ClearIsSelected(object sender, RoutedEventArgs e)
        {
            FishRandomSelector.core.Info.Names.ClearIsSelected();

        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(Menu.SelectedIndex)
            {
                case 0:
                    LeftAreaHost.Source = new Uri("Views/SettingPage.xaml",UriKind.Relative);
                    MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, Menu);
                    break;
                case 1:
                    LeftAreaHost.Source = new Uri("Views/PeopleView.xaml",UriKind.Relative);
                    MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, Menu);
                    break;
                case 2:
                    LeftAreaHost.Source = new Uri("Views/Inoutput.xaml", UriKind.Relative);
                    MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, Menu);
                    break;
                case 3:
                    LeftAreaHost.Source = new Uri("Views/InfoView.xaml", UriKind.Relative);
                    MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, Menu);
                    break;
            }
        }
    }
}
