using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Terradue.ServiceModel.Syndication;

namespace Talamelli.Lorenzo._5L.Posters.Models
{
    public class Provider
    {
        public List<string> LUrl = new List<string>();

        public void Fonti()
        {
            LUrl.Add("http://ilmattino.it/rss/home.xml");
            LUrl.Add("http://www.ilmessaggero.it/rss/home.xml");
        }

        public XmlReader EstrttoreNotizie(string Url)
        {
                XDocument doc = XDocument.Parse(DownloadString(Url));

                try
                {
                    //elimina nodi con le date
                    doc.Descendants("pubDate").Remove();
                    doc.Descendants("lastBuildDate").Remove();
                }
                catch { }

                string xmlData = doc.ToString();
                XmlReader reader = XmlReader.Create(new StringReader(xmlData));
               
                return reader;
        }

        //scarica la pagina web FeedRss passato
        private string DownloadString(string address)
        {
            string text;
            using (var client = new WebClient())
            {
                text = client.DownloadString(address);
            }
            return text;
        }

    }
}