using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MaterialDesignThemes.Wpf;

namespace FishRandomSelector.core.Views
{
    /// <summary>
    /// PeopleView.xaml 的交互逻辑
    /// </summary>
    public partial class PeopleView : Page
    {
        int nowID = 0;
        private bool CanUseNameList = true;
        public PeopleView()
        {
            InitializeComponent();
        }

        private void LoadPeople(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowPersonGrid.DataContext = Names.GetPerson(0);
                NoNameListMessage.IsActive = false;
                NameList.ItemsSource = Names.GetAllPeople();
            }
            catch (Exception ex)
            {
                prewBtn.IsEnabled = nextBtn.IsEnabled = RefreshButton.IsEnabled = IsEditMode.IsEnabled = false;
                App app = (App)Application.Current;
                app.HavePeople = false;
                textMessage.Text = "暂无名单可供编辑，错误信息:" + ex.Message;
            }
        }

        private void Prewperson(object sender, RoutedEventArgs e)
        {
            if (nowID > 0) nowID--;
            ShowPersonGrid.DataContext = Names.GetPerson(nowID);
        }
        private void Nextperson(object sender, RoutedEventArgs e)
        {
            if (nowID < Names.size) nowID++;
            ShowPersonGrid.DataContext = Names.GetPerson(nowID);
        }

        private void ChangeMode(object sender, RoutedEventArgs e)
        {
            if ((bool)IsEditMode.IsChecked)
            {
                NameEdit.IsReadOnly = false;
                ValueEdit.IsReadOnly = false;
                AddButton.IsEnabled = true;
                DelButton.IsEnabled = true;
            }
            else
            {
                NameEdit.IsReadOnly = true;
                ValueEdit.IsReadOnly = true;
                if (Settings.UISettings.AlwaysSaveWhenEndEit)
                    Names.SavePeople();
                AddButton.IsEnabled = false;
                DelButton.IsEnabled = false;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Names.AddPerson("[请输入名称]", 0);
            ShowPersonGrid.DataContext = Names.GetPerson(Names.size);
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            int PersonToDelete = nowID;
            if (nowID != 0)
                Prewperson(null, null);
            else
                Nextperson(null, null);
            Names.DeletePerson(PersonToDelete);
            NameList.IsEnabled = false;
            System.Threading.Thread thread = new System.Threading.Thread(SendMessage);
            thread.Start();
        }
        private void SendMessage()
        {
            this.NoNameListMessage.Dispatcher.Invoke(() =>
            {
                textMessage.Text = "序号的更改将于下一次启动或点击刷新按钮时生效,此外列表也会被禁用";
                NoNameListMessage.IsActive = true;
            });
            System.Threading.Thread.Sleep(2000);
            this.NoNameListMessage.Dispatcher.Invoke(() => { NoNameListMessage.IsActive = false; });
        }

        private void jumptoButton_Click(object sender, RoutedEventArgs e)
        {
            JumpToDialog.IsOpen = true;
        }

        private void NumJumpTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            int num = 0;
            try
            {
                num = int.Parse(NumJumpTo.Text);
                if (num > Names.size) throw new Exception("超出范围");
                errorMessage.Text = "";
            }
            catch (Exception ex)
            {
                errorMessage.Text = ex.Message;
            }
        }

        private void JumpToFinishButton_Click(object sender, RoutedEventArgs e)
        {
            if (errorMessage.Text == "")
            {
                nowID = int.Parse(NumJumpTo.Text);
                ShowPersonGrid.DataContext = Names.GetPerson(nowID);
                JumpToDialog.IsOpen = false;
            }
            else
            {
                errorMessage.Text = "请先改正此字段";
            }
        }

        private void NameList_Selected(object sender, RoutedEventArgs e)
        {
            person p = (person)NameList.SelectedItem;
            if (p != null)
                nowID = p.ID;
            ShowPersonGrid.DataContext = Names.GetPerson(nowID);
        }

        private void NameList_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CanUseNameList) return;
            else NameEdit.Focus();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Names.SavePeople();
            Names.DeleteAll();
            Names.ReadPeople();
            NameList.ItemsSource = Names.GetAllPeople();
            NameList.IsEnabled = true;
        }
    }
}
