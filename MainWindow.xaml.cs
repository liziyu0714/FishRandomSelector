using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        string[] SelectedHistory = new string[100];
        int nowS = 0;
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
                var taskbarItemInfo = new TaskbarItemInfo();
                taskbarItemInfo.ProgressState = TaskbarItemProgressState.Indeterminate;
                this.TaskbarItemInfo = taskbarItemInfo;
                SelectButton.Cursor = Cursors.Wait;
                SelectButton.IsEnabled = false;
                ButtonCanUse = false;
            }
            else
            {
                var taskbarItemInfo = new TaskbarItemInfo();
                taskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
                this.TaskbarItemInfo = taskbarItemInfo;
                SelectButton.Cursor = Cursors.Hand;
                SelectButton.IsEnabled = true;
                ButtonCanUse = true;
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            //throw new Exception("Hello!");
            if (!app.HavePeople)
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
            if (!MainFlipper.IsFlipped)
            {
                if (Settings.UISettings.UseAnimation)
                {
                    Thread ChangeTextThread = new Thread(ChangeText);
                    ChangeTextThread.Start();
                    MainText.Text = FishRandomSelector.core.Info.Names.GetPersonByValue();
                }
                else
                {
                    MainText.Text = FishRandomSelector.core.Info.Names.GetPersonByValue();
                }
            }
            else
            {
                if (Settings.UISettings.UseAnimation)
                {
                    Thread ChangeTextThread = new Thread(ChangeTextMorePeople);
                    ChangeTextThread.Start();
                }
                else
                {
                    MorePeopleNameList.ItemsSource = core.Info.Names.GetPeopleList((int)Math.Ceiling(PeopleCount.Value));
                    MorePeopleNameList.DisplayMemberPath = "Name";
                }
            }
            //SelectedHistory[SelectedHistory.Length] = MainText.Text;
            //nowS = SelectedHistory.Length - 1;

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
        private void ChangeTextMorePeople()
        {
            ObservableCollection<string> PeopleNamesList = new ObservableCollection<string>();
            int Peoplenum = 0;
            this.Dispatcher.Invoke(() => { Peoplenum = (int)Math.Ceiling(PeopleCount.Value); });
            this.Dispatcher.Invoke(() => { MorePeopleNameList.ItemsSource = PeopleNamesList; });
            this.Dispatcher.Invoke(ChangeSelectButtonState);
            Random timesRandom = new Random();
            int Time = timesRandom.Next(Settings.UISettings.MinChangeTextTimes, Settings.UISettings.MaxChangeTextTimes);
            for (int i = 0; i < Peoplenum; i++)
            {
                for (int j = 0; j < Time; j++)
                {
                    Thread.Sleep(Settings.UISettings.TimeBetweenChangeText);
                    this.Dispatcher.Invoke(() => { MorePeopleText.Text = core.Info.Names.GetARandomName(); });
                }
                this.Dispatcher.Invoke(() => { MorePeopleText.Text = core.Info.Names.GetPersonByValue(); });
                this.Dispatcher.Invoke(() => { PeopleNamesList.Add((string)MorePeopleText.Text.Clone()); });
                Thread.Sleep(200);
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
            switch (Menu.SelectedIndex)
            {
                case 0:
                    LeftAreaHost.Source = new Uri("Views/SettingPage.xaml", UriKind.Relative);
                    MaterialDesignThemes.Wpf.DrawerHost.CloseDrawerCommand.Execute(null, Menu);
                    break;
                case 1:
                    LeftAreaHost.Source = new Uri("Views/PeopleView.xaml", UriKind.Relative);
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

        private void MainFlipper_IsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            PeopleCount.Maximum = FishRandomSelector.core.Info.Names.size + 1;
        }

        private void PreSelectedPerson(object sender, RoutedEventArgs e)
        {
            //MainText.Text = SelectedHistory[nowS - 1];
            //nowS -= 1;

        }

        private void NextSelectedPerson(object sender, RoutedEventArgs e)
        {
            //MainText.Text = SelectedHistory[nowS + 1];
            //nowS += 1;
        }

        private void Addp(object sender, RoutedEventArgs e)
        {
            foreach(core.Info.person p in core.Info.Names.GetAllPeople())
            {
                if (p.Name == MainText.Text) 
                {
                    core.Info.Names.AddPersonValue(p.ID, 10);
                }
            }
        }

        private void Subp(object sender, RoutedEventArgs e)
        {
            foreach (core.Info.person p in core.Info.Names.GetAllPeople())
            {
                if (p.Name == MainText.Text) 
                {
                    core.Info.Names.AddPersonValue(p.ID, -10);
                }
            }
        }
    }
}
