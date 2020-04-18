using Newtonsoft.Json;
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

namespace GorniyPriutPanel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Frame mainFrame;
       
        public MainWindow()
        {
            InitializeComponent();

            mainFrame = FrameMain;
        }

        public static void SetContent(string content)
        {
            mainFrame.Source = new Uri(content + ".xaml", UriKind.Relative);
        }
    }
}
