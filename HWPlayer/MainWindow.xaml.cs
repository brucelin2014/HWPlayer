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
using System.Configuration;
using System.Reflection;

namespace HWPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ConfigObj configObj = new ConfigObj();
            ConfigMgr configMgr = new ConfigMgr();
            if (configMgr.LoadConfig(ref configObj))
            {
                this.WindowStyle = WindowStyle.None;
                this.Width = configObj.win_width;
                this.Height = configObj.win_height;
                this.Left = configObj.win_left;
                this.Top = configObj.win_top;

                web_view.Navigate(configObj.open_url);
            }
        }

        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            SuppressScriptErrors((WebBrowser)sender, true);
        }
        public void SuppressScriptErrors(WebBrowser wb, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;

            object objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;

            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
        }

    }

}
