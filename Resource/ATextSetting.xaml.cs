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

namespace FishRandomSelector.core.Resource
{
    /// <summary>
    /// ATextSetting.xaml 的交互逻辑
    /// </summary>
    public partial class ATextSetting : UserControl
    {
        public static DependencyProperty SettingNameProperty;
        public static DependencyProperty HelpTextProperty;
        public static DependencyProperty SettingValueProperty;
        public string SettingName
        {
            get { return (string)GetValue(SettingNameProperty); }
            set { SetValue(SettingNameProperty, value); }
        }
        public string HelpText
        {
            get { return (string)GetValue(HelpTextProperty); }
            set { SetValue(HelpTextProperty, value); }
        }
        public string SettingValue
        {
            get { return (string)GetValue(SettingValueProperty); }
            set { SetValue(SettingValueProperty, value); }
        }
        public ATextSetting()
        {
            InitializeComponent();
        }
        static ATextSetting()
        {
            SettingNameProperty = DependencyProperty.Register(
                "SettingName",
                typeof(string),
                typeof(ATextSetting),
                new PropertyMetadata(new PropertyChangedCallback(OnSettingNameChanged)));
            HelpTextProperty = DependencyProperty.Register(
                "HelpText",
                typeof(string),
                typeof(ATextSetting),
                new PropertyMetadata(new PropertyChangedCallback(OnHelpTextChanged)));
            SettingValueProperty = DependencyProperty.Register(
                "SettingValue",
                typeof(string),
                typeof(ATextSetting),
                new PropertyMetadata(new PropertyChangedCallback(OnHelpTextChanged)));
        }
        private static void OnSettingNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        private static void OnHelpTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void SettingValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            SettingValue = SettingValue_Text.Text;
        }
    }
}
