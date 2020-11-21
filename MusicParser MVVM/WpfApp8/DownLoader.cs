using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp8
{
    class DownLoader
    { 
        ProgressBar progress;
        List<string> names;
        List<string> Urls;
        int index = 0;
        WebClient webClient = new WebClient();
        public DownLoader(ProgressBar pb)
        {
            progress = pb;
        }
        void pb(object sender, DownloadProgressChangedEventArgs e)
        {
            progress.Maximum = e.TotalBytesToReceive;
            progress.Value = e.BytesReceived;
        }
        void EndDownload(object sender, AsyncCompletedEventArgs e)
        {
            index++;
           
            if(index<names.Count)
                webClient.DownloadFileAsync(new Uri(Urls[index]), names[index]);
        }
        public void Download(string url, string path)
        {
            WebClient webClient;
            webClient = new WebClient();
            webClient.DownloadFileAsync(new Uri(url), path);
            webClient.DownloadProgressChanged +=
                new DownloadProgressChangedEventHandler(pb);
        }
        public void DownloadAll(List<string> url, List<string> path)
        {
            Urls = url;
            names = path;
            index = 0;
            
            webClient.DownloadProgressChanged += 
                new DownloadProgressChangedEventHandler(pb);

            webClient.DownloadFileCompleted +=
                new AsyncCompletedEventHandler(EndDownload);

            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(EndDownload);
        }
    }
}
