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
            try
            { ShowPersonGrid.DataContext = Names.GetPerson(0); NoNameListMessage.IsActive = false; }
            catch (Exception ex) { prewBtn.IsEnabled = nextBtn.IsEnabled = false; textMessage.Text = "暂无名单可供编辑" + ex.Message; }
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
                if(Settings.UISettings.AlwaysSaveWhenEndEit)
                    Names.SavePeople();
                AddButton.IsEnabled = false;
                DelButton.IsEnabled = false;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Names.AddPerson("[请输入名称并更改值]", 0);
            ShowPersonGrid.DataContext = Names.GetPerson(Names.size - 1);
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            int PersonToDelete = nowID;
            if (nowID != 0)
                Prewperson(null, null);
            else
                Nextperson(null, null);
            Names.DeletePerson(PersonToDelete);
            System.Threading.Thread thread = new System.Threading.Thread(SendMessage);
            thread.Start();
        }
        private void SendMessage()
        {
            this.NoNameListMessage.Dispatcher.Invoke(() =>
            {
                textMessage.Text = "序号的更改将于下一次启动时生效";
                NoNameListMessage.IsActive = true;
            });
            System.Threading.Thread.Sleep(2000);
            this.NoNameListMessage.Dispatcher.Invoke(() => { NoNameListMessage.IsActive = false; });
        }

        private void jumptoButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
