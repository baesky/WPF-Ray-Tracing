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

namespace WPFRayTracing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FinalOutput.Width = ((App)Application.Current).MyWorld.VP.HRes;
            FinalOutput.Height = ((App)Application.Current).MyWorld.VP.VRes;
            FinalOutput.Source = ViewPlane.BackBuffer;

        }

        private void Render_Button_Click(object sender, RoutedEventArgs e)
        {
            
            ((App)Application.Current).MyWorld.RenderScene();
        }
    }
}
