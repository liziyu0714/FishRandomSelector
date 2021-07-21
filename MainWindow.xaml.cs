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

namespace FishRandomSelector
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsLeftAreaOpen = false;
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
            GridLength full = new GridLength(this.ActualWidth);
            GridLength none = new GridLength(0);
            if (IsLeftAreaOpen)
            {
                LeftArea.Width = none;
                IsLeftAreaOpen = false;
            }
            else
            {
                LeftArea.Width =full ;
                IsLeftAreaOpen = true;
            }
        }
        private void ChangeSelectButtonState ()
        {
            if(SelectButton.IsEnabled)
            {
                SelectButton.Cursor = Cursors.Wait;
                SelectButton.IsEnabled = false;
            }
            else
            {
                SelectButton.Cursor = Cursors.Hand;
                SelectButton.IsEnabled = true;
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectButtonState();
        }
    }
}
