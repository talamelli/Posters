using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Talamelli.Lorenzo._5L.Posters.Controllers;
using Terradue.ServiceModel.Syndication;

namespace Talamelli.Lorenzo._5L.Posters
{
    public class Provider
    {
        //lista completa di tutte le fonti del db
        public Fonti LUrl { get; set; }

        public Provider()
        {
            LUrl = new Fonti();
        }

        public Provider(string UserId)
        {
            LUrl = new Fonti(UserId);
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
    public class dbProvider
    {
         
        string NomeServer = @"Server=mssql2.gear.host;Database=posters;";
        string NomeFileDb = @"";
        string tipoSicurezza = @"User Id=posters;Password=Yk7K6w4-1wq!;";

        SqlConnection cn { get; set; }

        // La stringa di connessione
        public dbProvider()
        {
            string stringaConnesione = NomeServer + NomeFileDb + tipoSicurezza;
            cn = new SqlConnection(stringaConnesione);
        }

        public DataTable Getdata(string query)
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);

            return tbl;
        }
        public int Insert(string query)
        {
            int retVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Connection.Open();
                retVal = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch { }
            return retVal;

        }
    }
}
 