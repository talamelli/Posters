using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Talamelli.Lorenzo._5L.Posters.Controllers;

namespace Talamelli.Lorenzo._5L.Posters
{
    public class Fonti : List<Fonte>
    {
        public bool IsChecked { get; set; }

        private dbProvider p = new dbProvider();
        private string errore { get; set; }

        public Fonti()// crea una lista di fonti con tutte quelle pre impostate sul db (tabella GeneralFonti)
        {
            
            try
            {
                DataTable font = p.Getdata("select * from GeneralSorgenti");

                foreach (DataRow row in font.Rows)
                {
                    Add(new Fonte(
                             row["Titolo"].ToString().TrimEnd(),
                             row["Link"].ToString().Trim()));
                }

            }
            catch (Exception Err) { errore = Err.Message; }
        }

        public Fonti(string UserId)
        {
            if (UserId != null)
            {
                FController f = new FController();
                foreach (UserToFonte item in f.GetL(UserId))
                {
                    Add(new Fonte(item.URL, item.Titolo));
                } 
                    
            }
            else
            {
               AddRange(new Fonti());
            }
        }
    }
}