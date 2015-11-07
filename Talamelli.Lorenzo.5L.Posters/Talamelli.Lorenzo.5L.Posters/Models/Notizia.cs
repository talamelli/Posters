using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
                XDocument doc = XDocument.Parse(GetNotizie.DownloadString(Url.URL));

               giornale.AddRange((from descendant in doc.Descendants("item")
                    select new Notizia()
                    {
                        
                        Descrizione = getDescr(descendant.Element("description").Value) ,
                        Titolo = Encoder(descendant.Element("title").Value),
                        SitoProvenienza = Url.Titolo,
                      
                    }).ToList());

            }
            return giornale;
        }

    public string getDescr(string str)
    {
            //prende testo comprose tra a> e </p se disponibile altrimenti restituisce non disponibile se non c'è testo o la stringa originale
        if(Regex.Match(str, @"(?<=a>)(.*)(?=</p>)").ToString() != String.Empty)
            {
                return Encoder(Regex.Match(str, @"(?<=a>)(.*)(?=</p>)").ToString());
            }
            else
            {
                if (!str.Contains("</a></p>"))
                {

                    return Encoder(Regex.Replace(str, "<.*?>", string.Empty));

                }
                else
                {
                    return "Non disponibile";
                }
            }
    }
        //consente di visualizzare gli accenti
        public string Encoder(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            str = Encoding.UTF8.GetString(bytes);
            return str;
        }
    }
}
