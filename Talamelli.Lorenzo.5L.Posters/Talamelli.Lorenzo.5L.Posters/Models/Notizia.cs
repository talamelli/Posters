using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Terradue.ServiceModel.Syndication;

namespace Talamelli.Lorenzo._5L.Posters
{
    public class Notizia
    {
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public string SitoProvenienza { get; set; }//link

        public Notizia()
        {

        }

        public List<Notizia> GeneratoreNotizie(string UserId)
        {
            Provider GetNotizie = new Provider(UserId);

            List<Notizia> giornale = new List<Notizia>();

            foreach (Fonte Url in GetNotizie.LUrl)
            {
                SyndicationFeed feed = SyndicationFeed.Load(GetNotizie.EstrttoreNotizie(Url.URL));

                //riempimento lista di notizie (Descrizione e Titolo)
                foreach (SyndicationItem itemus in feed.Items)
                {
                    giornale.Add(new Notizia { Titolo = itemus.Title.Text, Descrizione = itemus.Summary.Text, SitoProvenienza = Url.Titolo });
                }

            }
            return giornale;
        }
    }
}
