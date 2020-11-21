using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp8
{
    class Parsing
    {
        private WebResponse response;
        private WebRequest request;
        private string site;
        private string adress = "https://zvooq.pro/";
        private List<string> name = new List<string>();
        private List<string> url = new List<string>();

        public string getSite(string filter,string nameSound)
        {
            request = WebRequest.Create(adress + filter+nameSound);
            response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            site = reader.ReadToEnd();
            response.Close();
            return site;
        }
        public List<string> urlnames
        {
            get { return url; }
        }

        public List<string> generationList()
        {
            name.Clear();
            int index = 0;
            int urlindex = 0;
            int count = 0;
            while (index != -1)
            {
                index = site.IndexOf("data-title");
                if (index != -1)
                {
                    site = site.Remove(0, index + 12);
                    if(count>=10)
                    name.Add(site.Remove(site.IndexOf("\"")));

                    urlindex = site.IndexOf("data-url");
                    site = site.Remove(0, urlindex + 10);
                    if (count >= 10)
                    {
                        url.Add(site.Remove(site.IndexOf("\"")));
                    }
                }
                count++;
            }
            return name;
        }
    }
}
