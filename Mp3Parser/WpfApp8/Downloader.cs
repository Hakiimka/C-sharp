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
     class Downloader
    {
        ProgressBar progress;
        List<string> names;
        List<string> urls;
        WebClient webclient = new WebClient();
        int index;
        public Downloader(ProgressBar pb)
        {
            progress = pb;
            
        }
          void pb(object sender,DownloadProgressChangedEventArgs e)
        {
            progress.Maximum = e.TotalBytesToReceive;
            progress.Value = e.BytesReceived;
        }
        
        public void DownloadAll(List<string> url,List<string> path)
        {
            urls = url;
            names = path;
            index = 0;
            webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(pb);
            webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(endDownload);
            webclient.DownloadFileAsync(new Uri(url[0]), path[0]);

        }
        void endDownload(object sender, AsyncCompletedEventArgs e)
        {
            index++;
            if(index<names.Count)
            {
                webclient.DownloadFileAsync(new Uri(urls[index]), names[index]);
            }
        }
        public  void Download(string url,string path)
        {
            webclient.DownloadFileAsync(new Uri(url), path);
            webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(pb);
        }
    }
}
