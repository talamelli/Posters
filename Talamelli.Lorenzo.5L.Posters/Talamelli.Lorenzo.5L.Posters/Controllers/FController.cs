using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Talamelli.Lorenzo._5L.Posters;

namespace Talamelli.Lorenzo._5L.Posters.Controllers
{
    public class FController : Controller
    {
        
        #region dbController action
        dbProvider p = new dbProvider();
        List<UserToFonte> Fonti = new List<UserToFonte>();
        string errore = "";

        public FController()
        {
            try
            {
                DataTable font = p.Getdata("select * from Fonti");

                Fonti = (from DataRow row in font.Rows
                         select new UserToFonte
                         (
                             Convert.ToInt32(row["Id"]),
                             row["User"].ToString().Trim(),
                             row["LinkFonte"].ToString().Trim(),
                             row["TitoloGiornale"].ToString().TrimEnd(),
                             row["UserId"].ToString().Trim()
                         )).ToList();
            }
            catch (Exception Err) { errore = Err.Message; }
        }

        // GET api/F
        //[AllowAnonymous]
        public IEnumerable<UserToFonte> GetL()
        {
            return Fonti;
        }

        public IEnumerable<UserToFonte> GetL(string UserID)
        {
            //lista di fonti di un determinato utente
            return Fonti.Where(item => item.UserId == UserID).ToList();

        }

        // GET: api/F/5
        public UserToFonte Get(string RicTitolo, string UserID)
        {

            foreach (UserToFonte item in GetL(UserID))
            {
                if (item.Titolo == RicTitolo.TrimEnd())
                    return item;
            }

            return new UserToFonte();
        }

        // POST: api/F
        public int Post(UserToFonte value)
        {
            string query = "insert into Fonti([User], LinkFonte, TitoloGiornale, UserId) values('" + value.User + "', '" + value.URL.Trim() + "', '" + value.Titolo.TrimEnd() + "', '" + value.UserId + "' )";
            return p.Insert(query);
        }

        // PUT: api/F/5
        public void Put(int id, UserToFonte value)
        {
            string query = String.Format("update Fonti set TitoloGiornale='{0}', LinkFonte='{1}' where id={2}",
               value.Titolo, value.URL, value.ID);

            p.Insert(query);
        }

        // DELETE: api/F/5
        public void Delete(int id)
        {
            string query = "delete from Fonti where ID=" + id;
            p.Insert(query);
        }
        #endregion

        //con bottone
        public ActionResult Aggiungi(string Titolo)
        {
            var Find = new Fonti().Find(m => m.Titolo == Titolo);
            Post(new UserToFonte { Titolo = Titolo, URL = Find.URL, UserId = User.Identity.GetUserId().ToString(), User = User.Identity.GetUserName().ToString() });
            return RedirectToAction("ChangeFonti");
        }

        //con bottone
        public ActionResult Rimuovi(string Titolo)
        {
            var Find = Get(Titolo, User.Identity.GetUserId().ToString());
            Delete(Find.ID);
            return RedirectToAction("ChangeFonti");
        }

        //utilizza checkBox
        public ActionResult UpdateValue(FormCollection form)
        {
            Fonti f = new Fonti();
          
            foreach (Fonte item in f)
            {
                
                var Find = Get(item.Titolo, User.Identity.GetUserId().ToString());
                 
                if (Convert.ToBoolean(form[item.Titolo].Contains("true")))//fonte selezionata
                {
                    if (Find.Titolo == null)//non esiste nel db
                    {
                        //aggiunta della fonte
                        Post(new UserToFonte { Titolo = item.Titolo, URL = item.URL, UserId = User.Identity.GetUserId().ToString(), User = User.Identity.GetUserName().ToString() });
                    }
                }
                else
                {
                    if (Find.Titolo != null)//esiste nel db
                    {
                        //rimozione della fonte
                        Delete(Find.ID);
                    }
                }
            }
            return RedirectToAction("ChangeFonti");

        }
     
        //crea una lista delle fonti selezionate da mandare alla view
        public ActionResult ChangeFonti()
        {
            Fonti f = new Fonti();

            List<UserToFonte> compareItem = new List<UserToFonte>();
            try {
                //tutte le fonti selezionate da quell' utente
                compareItem = GetL(User.Identity.GetUserId().ToString()).ToList();
            }
            catch { }

            foreach (UserToFonte item in compareItem)// spunta le caselle delle fonti preselezionate dall'utente
            {
                f.Find(trova => trova.Titolo == item.Titolo).IsChecked = true;
            }
            return View(f);
        }
    }
}
