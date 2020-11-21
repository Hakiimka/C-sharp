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
using Microsoft.Win32;


namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Parsing parsing;
        List<string> result;
        List<string> urls;
        private void TextBox_KeyDown(object sender,KeyEventArgs e)
        {
            parsing = new Parsing();
            string name = NameSound.Text;
            name = DeleteWrongSymbols.DeleteSymbols(name);
            if (e.Key == Key.Enter)
            {
                ListName.Items.Clear();
                if (artist.IsChecked == true)
                {
                    parsing.getSite("artists/",name);
                }
                else
                {
                    parsing.getSite("tracks/", name);
                }
                result = parsing.generationList();
                foreach (string x in result)
                {
                    ListName.Items.Add(x);
                }
            }

            //MessageBox.Show(parsing.getSite(NameSound.Text));
        }
        
     
    private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> url = parsing.urlnames;
            string name = ListName.SelectedItem.ToString();
            name = DeleteWrongSymbols.DeleteSymbols(name);
            Downloader downloader = new Downloader(ProgressBar);
            downloader.Download(url[ListName.SelectedIndex], "C:\\1\\"+name+".mp3");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Downloader downloader = new Downloader(ProgressBar);
            urls = parsing.urlnames;
            List<string> names = new List<string>();
            for(int i=0;i<urls.Count;i++)
            {
                names.Add(pathBox.Text +"\\"+ DeleteWrongSymbols.DeleteSymbols(ListName.Items[i].ToString()) + ".mp3");
            }
            downloader.DownloadAll(urls, names);

        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog browserDialog = new System.Windows.Forms.FolderBrowserDialog();
            browserDialog.ShowDialog();
            pathBox.Text = browserDialog.SelectedPath;
        }
    }
}
