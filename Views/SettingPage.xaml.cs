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
using FishRandomSelector.core.Resource;

namespace FishRandomSelector.core.Views
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
            OpenLeftArea.IsDefine = Settings.UISettings.CloseLeftAreaWhenClick;
            SaveFast.IsDefine = Settings.UISettings.AlwaysSaveWhenEndEit;
            ErrorDialog.IsDefine = Settings.UISettings.UseSystemDefaultErrorDialog;
            TimeBetween.SettingValue = Settings.UISettings.TimeBetweenChangeText.ToString();
            MaxChangeTime.SettingValue = Settings.UISettings.MaxChangeTextTimes.ToString();
            MinChangeTime.SettingValue = Settings.UISettings.MinChangeTextTimes.ToString();

        }

        private void ASetting_CheckChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            ASetting p = (ASetting)sender;
            bool define = p.IsDefine;
            switch (p.Name)
            {
                case "OpenLeftArea":
                    Settings.UISettings.CloseLeftAreaWhenClick = define;break;
                case "SaveFast":
                    Settings.UISettings.AlwaysSaveWhenEndEit = define; break;
                case "ErrorDialog":
                    Settings.UISettings.UseSystemDefaultErrorDialog = define; break;
                default: return;
            }
        }
        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings.UISettings.TimeBetweenChangeText = int.Parse(TimeBetween.SettingValue);
                Settings.UISettings.MaxChangeTextTimes = int.Parse(MaxChangeTime.SettingValue);
                Settings.UISettings.MinChangeTextTimes = int.Parse(MinChangeTime.SettingValue);
            }
            catch(Exception ex)
            {
                MessageBox.Show("设置的值有问题:" + ex.Message);
            }
        }
    }
}
