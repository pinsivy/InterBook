using InterBook2._0.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace InterBook2._0.BLL
{
    /// <summary>
    /// Description résumée de IBWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class IBWS : System.Web.Services.WebService
    {

        InterBookEntities _db;

        [WebMethod]
        public void save()
        {
            _db.SaveChanges();
        }

        [WebMethod]
        public Util GetUtilByIdu(int idu)
        {
            _db = new InterBookEntities();
            var u = (from m in _db.Util where m.IdU == idu select m).FirstOrDefault();
            return u;
        }

        [WebMethod]
        public List<Util> GetUtilsByVilleProfession(string Ville, string Profession)
        {
            _db = new InterBookEntities();
            var us = _db.SearchByVilleProfession(Ville, Profession);
            List<Util> lu = null;
            if (us != null)
            {
                lu = new List<Util>();
                foreach (Util u in us)
                {
                    lu.Add(u);
                }
            }
            return lu;
        }
    }
}
