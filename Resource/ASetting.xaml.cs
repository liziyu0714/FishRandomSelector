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
    /// ASetting.xaml 的交互逻辑
    /// </summary>
    public partial class ASetting : UserControl
    {
        public static DependencyProperty SettingNameProperty;
        public static DependencyProperty HelpTextProperty;
        public static DependencyProperty IsDefineProperty;
        public static readonly RoutedEvent CheckChangeEvent;
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
        public bool IsDefine
        {
            get { return (bool)GetValue(IsDefineProperty); }
            set { SetValue(IsDefineProperty, value); }
        }
        public event RoutedPropertyChangedEventHandler<bool> CheckChanged
        {
            add { AddHandler(CheckChangeEvent, value); }
            remove { RemoveHandler(CheckChangeEvent, value); }
        }
        public ASetting()
        {
            InitializeComponent();
        }
        static ASetting()
        {
            SettingNameProperty = DependencyProperty.Register(
                "SettingName",
                typeof(string),
                typeof(ASetting),
                new PropertyMetadata(new PropertyChangedCallback(OnSettingNameChanged)));
            HelpTextProperty = DependencyProperty.Register(
                "HelpText",
                typeof(string),
                typeof(ASetting),
                new PropertyMetadata(new PropertyChangedCallback(OnHelpTextChanged)));
            IsDefineProperty = DependencyProperty.Register(
                "IsDefine",
                typeof(bool),
                typeof(ASetting),
                new PropertyMetadata(new PropertyChangedCallback(OnIsDefineChanged)));
            CheckChangeEvent = EventManager.RegisterRoutedEvent("CheckChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<bool>), typeof(ASetting));

        }
        private static void OnSettingNameChanged(DependencyObject sender , DependencyPropertyChangedEventArgs e)
        {

        }
        private static void OnHelpTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }
        private static void OnIsDefineChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
        private static void OnNameColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)togBtn.IsChecked)
            {
                SetValue(IsDefineProperty, true);
                RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(false, true);
                args.RoutedEvent = ASetting.CheckChangeEvent;
                this.RaiseEvent(args);
            }
            else
            {
                SetValue(IsDefineProperty, false);
                RoutedPropertyChangedEventArgs<bool> args = new RoutedPropertyChangedEventArgs<bool>(true, false);
                args.RoutedEvent = ASetting.CheckChangeEvent;
                this.RaiseEvent(args);
            }
        }
    }
}
