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
    /// Логика взаимодействия для News.xaml
    /// </summary>
    public partial class News : Page
    {
        ObservableCollection<NewForm> items;
        public News()
        {
            InitializeComponent();
            if (Request.GetInstance().HasConnection())
                GetNews();
        }

        public async void AddPost(string title, string date, string text)
        {

            var day = date.Split('.').First();
            var month = date.Split('.')[1];
            var year = date.Split('.').Last();


            var values = new Dictionary<string, string>
            {

                { "title", title },
                { "date", string.Join("-",new string[]{ year,month,day})},
                { "text", text},

            };

            StatusBar.DisableControls();

            string t = await Request.GetInstance().Post("home/new", values);

            StatusBar.EnableControls();


            Console.WriteLine(t);
        }

        private async void GetNews()
        {
            StatusBar.DisableControls();

            string result = await Request.GetInstance().Get("home/newsjson", new Dictionary<string, string>() { });

            items = JsonConvert.DeserializeObject<ObservableCollection<NewForm>>(result);

            news_list.ItemsSource = items;

            StatusBar.EnableControls();
        }

      
        private void AddNew(object sender, RoutedEventArgs e)
        {
            AddPost(post_form_title.Text, post_from_date.Text, post_form_text.Text);
        }

        private void ChangeNews_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshItems(object sender, RoutedEventArgs e)
        {
            items?.Clear();
            GetNews();
        }
    }
}
