using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Talamelli.Lorenzo._5L.Posters
{
    public class UserToFonte:Fonte
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string UserId { get; set; }

        public UserToFonte()
        { }

        public UserToFonte(int ID,string us ,string url, string titol, string usid)
            :base(url,titol)
        {
            this.ID = ID;
            User = us;
            UserId = usid;
        }
    }

    public class Fonte
    {
        private int v1;
        private string v2;
        private string v3;

        public string URL { get; set; }
        public string Titolo { get; set; }
        public bool IsChecked { get; set; }

        public Fonte()
        { }

        public Fonte(string url, string titolo)
        {
            URL = url;
            Titolo = titolo;
            IsChecked = false;
        }

        public Fonte(int v1, string v2, string v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }
}